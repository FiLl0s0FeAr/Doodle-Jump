using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public float movementSpeed = 10f;

    private float movement = 0f;
    private Rigidbody2D rigidbody2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal") * movementSpeed;
    }
    void FixedUpdate()
    {
        Vector2 velocity = rigidbody2.linearVelocity;
        velocity.x = movement;
        rigidbody2.linearVelocity = velocity;
    }
}
