using UnityEngine;

public class LevelGeneratorScript : MonoBehaviour
{

    public GameObject platformPrefab;

    public int numberOfPlatforms = 10; // Number of platforms to spawn per batch
    public float levelWidth = 3.0f; // Horizontal range for platform spawn
    public float minY = 1.0f; // Minimum vertical distance between platforms
    public float maxY = 2.0f; // Maximum vertical distance between platforms
    public float spawnThreshold = 10.0f; // Distance from the highest platform at which new platforms are spawned

    private float highestPlatformY = 0f; // Tracks the highest platform's Y position
    private Transform playerTransform; // Reference to the player's position
    private Camera mainCamera;

    private void Start()
    {
        // Find the player and main camera
        playerTransform = GameObject.FindWithTag("PlayerTag").transform;
        mainCamera = Camera.main;

        // Spawn the initial platforms
        SpawnPlatformBatch(0f, numberOfPlatforms);
    }

    private void Update()
    {
        // Check if platforms need to be spawned ahead
        if (playerTransform.position.y + spawnThreshold > highestPlatformY)
        {
            SpawnPlatformBatch(highestPlatformY, numberOfPlatforms);
        }

        // Destroy platforms that fall below the camera
        DestroyBelowCamera();
    }

    private void SpawnPlatformBatch(float startY, int platformCount)
    {
        float spawnY = startY;

        for (int i = 0; i < platformCount; i++)
        {
            float spawnX = Random.Range(-levelWidth, levelWidth);
            spawnY += Random.Range(minY, maxY);

            Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);
            Instantiate(platformPrefab, spawnPosition, Quaternion.identity);

            highestPlatformY = Mathf.Max(highestPlatformY, spawnY);
        }
    }

    private void DestroyBelowCamera()
    {
        float cameraBottomY = mainCamera.transform.position.y - mainCamera.orthographicSize;

        // Find all platforms in the scene
        foreach (GameObject platform in GameObject.FindGameObjectsWithTag("Platform"))
        {
            if (platform.transform.position.y < cameraBottomY)
            {
                Destroy(platform);
            }
        }
    }
}