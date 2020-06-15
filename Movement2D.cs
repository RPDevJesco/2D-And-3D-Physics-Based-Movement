using UnityEngine;
using MovementHandler;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement2D : MonoBehaviour
{
    // We can set the speed as we need it to be.
    public float Speed;
    // How high we want the object to jump
    public Vector2 jumpForce;
    // check to see if the object is in the air or on the ground
    public bool IsGrounded = false;
    // check to see if the object can bounce or not.
    public bool canBounce = false;
    // Horizontal Input.
    private float horizontal;
    // Jump input
    private float vertical;
    // Rigidbody component for physics
    [SerializeField] private Rigidbody2D rb;
    // Grab a reference to the movement backend
    private readonly MovementBackend _movementBackend = new MovementBackend();

    // Function that calls the backend's move function and fills in the data.
    private void Move()
    {
        _movementBackend.Move(rb,
            horizontal,
            0,
            Speed);
    }

    // Function that calls the backend's jump function and fills in the data.
    private void Jump()
    {
        if (IsGrounded != true) return;
        _movementBackend.Jump(rb, jumpForce, vertical);
    }

    // Function that calls the backend's bounce function and fills in the data.
    private void Bounce()
    {
        _movementBackend.Bounce(rb, jumpForce, canBounce);
    }

    // Physics update call. Only affects the Move and Jump
    private void FixedUpdate()
    {
        Bounce();
        Move();
        Jump();
    }

    // We use regular update to poll for input.
    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Jump");
    }
    
    // Here we check for collision starting to occur and if it is with the wall or the floor.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Wall"))
        {
            IsGrounded = true;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            canBounce = true;
        }
    }

    // We check to see if collisions are still occuring with either the floor or the wall, just the wall,
    // and just the floor.
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor") && collision.gameObject.CompareTag("Wall"))
        {
            IsGrounded = true;
        }

        if (!collision.gameObject.CompareTag("Floor") && collision.gameObject.CompareTag("Wall"))
        {
            IsGrounded = true;
        }

        if (collision.gameObject.CompareTag("Floor") && !collision.gameObject.CompareTag("Wall"))
        {
            IsGrounded = true;
        }
    }
    
    // If collision no longer occurs, then isgrounded is set to false.
    private void OnCollisionExit2D(Collision2D collision)
    {
        IsGrounded = false;
        canBounce = false;
    }
}
