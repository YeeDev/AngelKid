using UnityEngine;
using AK.Animations;

namespace AK.Movements
{
    [RequireComponent(typeof(Animater))]
    public class Mover : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 5f;
        [SerializeField] float jumpForce = 15f;
        [SerializeField] float climbingSpeed = 2;
        [SerializeField] float pushForce = 10;

        float initialGravity;
        Animater animater;
        Rigidbody2D rb;

        public float GetYRigidbodySpeed { get => rb.velocity.y; }

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animater = GetComponent<Animater>();

            initialGravity = rb.gravityScale;
        }

        public void SetGravity(bool setToInital, float gravityScale = 0)
        {
            rb.gravityScale = setToInital ? initialGravity : gravityScale;
        }

        public void StopRigidbody(bool keepFallSpeed = false)
        {
            rb.velocity = keepFallSpeed ? new Vector2(0, rb.velocity.y) : Vector2.zero;
        }

        public void Move(float xAxis, float yAxis, bool isClimbing)
        {
            rb.velocity = CalculateDirectionalSpeed(xAxis, yAxis, isClimbing);

            animater.CheckIfFlip(isClimbing ? 0 : xAxis);

            animater.SetWalkBool(Mathf.Abs(xAxis) > Mathf.Epsilon);
            animater.SetClimbing(isClimbing);
            animater.SetClimbSpeed(yAxis);
        }

        public void Jump(bool isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            if (isGrounded) { animater.PlayJumpDustEffect(); }
        }

        public void HaltJump()
        {
            if (rb.velocity.y < 0) { return; }

            Vector2 haltSpeed = rb.velocity;
            haltSpeed.y *= 0.5f;
            rb.velocity = haltSpeed;
        }

        public void PushInDirection(Vector3 pusherPosition)
        {
            StopRigidbody();
            Vector3 pushDirection = (transform.position - pusherPosition).normalized;
            rb.AddForce(pushDirection * pushForce);
            SetGravity(false, 0);
        }

        private Vector2 CalculateDirectionalSpeed(float xAxis, float yAxis, bool isClimbing)
        {
            Vector2 directionalSpeed = Vector2.zero;
            directionalSpeed.x = isClimbing ? 0 : xAxis * moveSpeed;
            directionalSpeed.y = isClimbing ? yAxis * climbingSpeed : rb.velocity.y;

            return directionalSpeed;
        }
    }
}