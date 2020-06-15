using UnityEngine;
using MovementHandler;

// Force this script to only work if a Rigidbody is attached to the object this script is attached to.
[RequireComponent(typeof(Rigidbody))]
public class Movement3D : MonoBehaviour
{
    // We can set the speed as we need it to be.
    public float Speed;
    // How high we want the object to jump
    private float jumpForce = 10f;
    // check to see if the object is in the air or on the ground
    [SerializeField] private bool IsGrounded = false;
    // Rigidbody component for physics
    [SerializeField] private Rigidbody rb;
    // Horizontal Input value
    private float horizontal;
    // Vertical Input value
    private float vertical;
    // Jump Input
    private float jumpInput;
    // Mouse move Horizontal Input
    private float rotationX;
    // Mouse move Vertical Input
    private float rotationY;
    // Frame update time value
    private float frameTime;
    // Grab a reference to the movement backend
    private readonly MovementBackend _movementBackend = new MovementBackend();

    // Function that calls the backend's move function and fills in the data.
    void Move()
    {
        _movementBackend.Move(this.transform,
            rb,
            horizontal,
            0,
            vertical,
            Speed,
            0,
            rotationY,
            frameTime);
    }

    // Function that calls the backend's jump function and fills in the data.
    void Jump()
    {
        _movementBackend.Jump(rb, IsGrounded, jumpInput, jumpForce);
    }

    // Here we check for collision starting to occur and if it is with the wall or the floor.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Wall"))
        {
            IsGrounded = true;
        }
    }

    // We check to see if collisions are still occuring with either the floor or the wall, just the wall,
    // and just the floor.
    private void OnCollisionStay(Collision collision)
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
    private void OnCollisionExit(Collision collision)
    {
        IsGrounded = false;
    }

    // Physics update call. Only affects the Move and Jump
    private void FixedUpdate()
    {
        frameTime = Time.fixedDeltaTime;
        Move();
        Jump();
    }

    // Polls the inputs from Update to pass to our horizontal and vertical input calls in physics update.
    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        jumpInput = Input.GetAxis("Jump");
        rotationY = Input.GetAxis("Mouse Y");
    }
}
