using UnityEngine;

// Namespace organizes the code in such a way that there will not be name collisions for method or class names
// from other sources of code such as Unity's API, code entered by the user or via code obtained from asset packs.
namespace MovementHandler
{
    public class MovementBackend
    {
        /// <summary>
        /// Move function for Physics based movement in 2D. Set the vertical to 0 if you want to employ the jump mechanic.
        /// </summary>
        /// <param name="rb">Rigidbody2D component</param>
        /// <param name="horizontal">Left and Right movement. Input.GetAxis("Horizontal")</param>
        /// <param name="vertical">Up and Down movement. Input.GetAxis("Vertical") or pass 0</param>
        /// <param name="speed">Speed value on how fast the character should move.</param>
        public void Move(Rigidbody2D rb, float horizontal, float vertical, float speed)
        {
            rb.AddForce(new Vector2(horizontal, vertical) * speed, ForceMode2D.Force);
        }

        /// <summary>
        /// Move function for 3D Physics based movement left, right, up, down and/ or forward, backwards
        /// Note, do not put the movement in vertical if you want to move forwards and backwards, put in depth.
        /// </summary>
        /// <param name="transform">The player's transform component.</param>
        /// <param name="rb">Rigidbody component</param>
        /// <param name="horizontal">Left and Right movement. Input.GetAxis("Horizontal")</param>
        /// <param name="vertical">Up and Down movement. Input.GetAxis("Vertical")</param>
        /// <param name="depth">Forward and Backwards movement. Input.GetAxis("Vertical")</param>
        /// <param name="speed">Speed value on how fast the character should move.</param>
        /// <param name="frameTime">Pass Time.fixedDeltaTime for physics based movement.</param>
        public void Move(Transform transform, Rigidbody rb, float horizontal, float vertical, float depth, float speed, float frameTime)
        {
            // Create a vector to house the three movement variables we need.
            Vector3 movement = new Vector3(horizontal, vertical, depth);
            // normalize the movement and add the speed and frames
            Vector3 fullMovement = movement.normalized * speed * frameTime;
            // move the physics body based off the transform's position and add the movement to it.
            rb.MovePosition(transform.position + fullMovement);
        }

        /// <summary>
        /// 3D Physics based movement with rotation.
        /// </summary>
        /// <param name="transform">The player's transform component.</param>
        /// <param name="rb">Rigidbody component</param>
        /// <param name="horizontal">Left and Right movement. Input.GetAxis("Horizontal")</param>
        /// <param name="vertical">Up and Down movement. Input.GetAxis("Vertical")</param>
        /// <param name="depth">Forward and Backwards movement. Input.GetAxis("Vertical")</param>
        /// <param name="speed">Speed value on how fast the character should move</param>
        /// <param name="rotationX">Rotation on the Y axis. Input.GetAxis("Mouse X")</param>
        /// <param name="rotationY">Rotation on the X axis. Input.GetAxis("Mouse Y")</param>
        /// <param name="frameTime">Pass Time.fixedDeltaTime for physics based movement.</param>
        public void Move(Transform transform, Rigidbody rb, float horizontal, float vertical, float depth, float speed, float rotationX, float rotationY, float frameTime)
        {
            // Create a vector to house the three movement variables we need.
            Vector3 movement = new Vector3(horizontal, vertical, depth);
            // normalize the movement and add the speed and frames
            Vector3 fullMovement = movement.normalized * speed * frameTime;
            // move the physics body based off the transform's position and add the movement to it.
            rb.MovePosition(transform.position + fullMovement);
            // Create a Vector to house the rotation variables
            Vector3 rotation = new Vector3(rotationX, rotationY, 0);
            // Create a quaternion to convert and store rotation delta values
            Quaternion deltaRotation = Quaternion.Euler(rotation * speed);
            // Pass the rigidbody's rotation and multiply it by the deltaRotation for smooth rotation.
            rb.MoveRotation(rb.rotation * deltaRotation);
        }

        /// <summary>
        /// 2D Physics based jumping. 
        /// </summary>
        /// <param name="rb">RigidBody2D component</param>
        /// <param name="jumpForce">Vector2 for how high the player can jump (0, 10).</param>
        public void Jump(Rigidbody2D rb, Vector2 jumpForce)
        {
            // check if the space key is pressed.
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Adds an upwards impulse for the character to move in an upward direction
                // and then fall back down due to gravity.
                rb.AddForce(jumpForce, ForceMode2D.Impulse);
            }
        }

        /// <summary>
        /// 2D physics based jump where upwards input needs specifying.
        /// </summary>
        /// <param name="rb">RigidBody2D component</param>
        /// <param name="jumpForce">Vector2 for how high the player can jump (0, 10).</param>
        /// <param name="vertical">Polls input for jump</param>
        public void Jump(Rigidbody2D rb, Vector2 jumpForce, float vertical)
        {
            // check if the space key is pressed.
            if (vertical > 0)
            {
                // Adds an upwards impulse for the character to move in an upward direction
                // and then fall back down due to gravity.
                rb.AddForce(jumpForce, ForceMode2D.Impulse);
            }
        }

        /// <summary>
        /// 3D Physics based jump mechanic.
        /// </summary>
        /// <param name="rb">RigidBody component</param>
        /// <param name="IsGrounded"> Boolean to check if on the ground or not.</param>
        /// <param name="jumpForce"> Float value for how high the character will jump.</param>
        public void Jump(Rigidbody rb, bool IsGrounded, float jumpForce)
        {
            // check if the space key is pressed and the boolean evaluates to true.
            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded == true)
            {
                // if it does, then we want to modify the upwards velocity by the jumpforce we want.
                rb.velocity = new Vector3(0f, jumpForce, 0f);
                // since the object leaves the ground, we set the boolean to be false as to not allow for more jumping during
                // this time frame.
                IsGrounded = false;
            }
        }

        /// <summary>
        /// 3D Physics based jump mechanic where jump needs to be specified.
        /// </summary>
        /// <param name="rb">RigidBody component</param>
        /// <param name="IsGrounded"> Boolean to check if on the ground or not.</param>
        /// <param name="vertical"> Float value for polling user input.</param>
        /// <param name="jumpForce"> Float value for how high the character will jump.</param>
        public void Jump(Rigidbody rb, bool IsGrounded, float vertical, float jumpForce)
        {
            // check if the space key is pressed and the boolean evaluates to true.
            if (vertical > 0 && IsGrounded == true)
            {
                // if it does, then we want to modify the upwards velocity by the jumpforce we want.
                rb.velocity = new Vector3(0f, jumpForce, 0f);
                // since the object leaves the ground, we set the boolean to be false as to not allow for more jumping during
                // this time frame.
                IsGrounded = false;
            }
        }
    }
}
