using UnityEngine;

public class LevelGeneratorScript : MonoBehaviour
{

    public GameObject platformPrefab;

    public int numberOfPlatforms = 10; // Platforms per batch
    public float levelWidth = 3.0f; // Horizontal range for spawning
    public float minY = 1.0f; // Minimum vertical distance
    public float maxY = 2.0f; // Maximum vertical distance
    public float spawnThreshold = 10.0f; // Distance above which platforms spawn

    private float highestPlatformY = 0f; // Tracks the highest Y position
    private Transform playerTransform; // Player's transform
    private Camera mainCamera;

    private void Start()
    {
        // Find player and camera
        playerTransform = GameObject.FindWithTag("PlayerTag").transform;
        mainCamera = Camera.main;

        // Spawn initial platforms
        SpawnPlatformBatch(0f, numberOfPlatforms);
    }

    private void Update()
    {
        // Spawn new platforms if the player approaches the highest platform
        if (playerTransform.position.y + spawnThreshold > highestPlatformY)
        {
            SpawnPlatformBatch(highestPlatformY, numberOfPlatforms);
        }

        // Destroy platforms below the camera
        DestroyBelowCamera();
    }

    private void SpawnPlatformBatch(float startY, int platformCount)
    {
        if (platformPrefab == null)
        {
            Debug.LogError("Platform Prefab is missing! Assign it in the Inspector.");
            return; // Exit early if prefab is missing
        }

        float spawnY = startY;

        for (int i = 0; i < platformCount; i++)
        {
            float spawnX = Random.Range(-levelWidth, levelWidth);
            spawnY += Random.Range(minY, maxY);

            Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);
            Instantiate(platformPrefab, spawnPosition, Quaternion.identity);

            // Update the highest platform Y position
            if (spawnY > highestPlatformY)
            {
                highestPlatformY = spawnY;
            }
        }
    }

    private void DestroyBelowCamera()
    {
        float cameraBottomY = mainCamera.transform.position.y - mainCamera.orthographicSize;

        // Find all platforms in the scene
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");

        foreach (GameObject platform in platforms)
        {
            if (platform.transform.position.y < cameraBottomY)
            {
                Destroy(platform);
            }
        }
    }
}