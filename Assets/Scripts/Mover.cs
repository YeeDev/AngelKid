using UnityEngine;

namespace AK.Movements
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 5f;
        [SerializeField] float jumpForce = 15f;
        [SerializeField] float climbingSpeed = 2;

        float initialGravity;
        Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();

            initialGravity = rb.gravityScale;
        }

        public void SetGravity(bool setToInital, float gravityScale = 0) { rb.gravityScale = setToInital ? initialGravity : gravityScale; }
        public void StopRigidbody() { rb.velocity = Vector2.zero; }

        public void Move(float xAxis, float yAxis, bool isClimbing) { rb.velocity = CalculateDirectionalSpeed(xAxis, yAxis, isClimbing); }

        public void Jump() { rb.velocity = new Vector2(rb.velocity.x, jumpForce); }

        public void HaltJump()
        {
            if (rb.velocity.y < 0) { return; }

            Vector2 haltSpeed = rb.velocity;
            haltSpeed.y *= 0.5f;
            rb.velocity = haltSpeed;
        }

        private Vector2 CalculateDirectionalSpeed(float xAxis, float yAxis, bool isClimbing)
        {
            Vector2 directionalSpeed = Vector2.zero;
            directionalSpeed.x = xAxis * moveSpeed;
            directionalSpeed.y = isClimbing ? yAxis * climbingSpeed : rb.velocity.y;

            return directionalSpeed;
        }
    }
}