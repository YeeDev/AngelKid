using UnityEngine;
using AK.MovementStates;

namespace AK.Movements
{
    [RequireComponent(typeof(Climber))]
    public class Mover : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 5f;
        [SerializeField] float jumpForce = 15f;

        float initialGravity;
        Rigidbody2D rb;
        Climber climber;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            climber = GetComponent<Climber>();

            initialGravity = rb.gravityScale;
        }

        public void SetGravity(bool setToInital, float gravityScale = 0) { rb.gravityScale = setToInital ? initialGravity : gravityScale; }
        public void StopRigidbody() { rb.velocity = Vector2.zero; }

        public void Move(float xAxis, float yAxis) { rb.velocity = CalculateDirectionalSpeed(xAxis, yAxis); }

        public void Jump() { rb.velocity = new Vector2(rb.velocity.x, jumpForce); }

        public void HaltJump()
        {
            if (rb.velocity.y < 0) { return; }

            Vector2 haltSpeed = rb.velocity;
            haltSpeed.y *= 0.5f;
            rb.velocity = haltSpeed;
        }

        private Vector2 CalculateDirectionalSpeed(float xAxis, float yAxis)
        {
            Vector2 directionalSpeed = Vector2.zero;
            directionalSpeed.x = xAxis * moveSpeed;
            directionalSpeed.y = climber.GetIsClimbing ? yAxis * climber.ClimbSpeed : rb.velocity.y;

            return directionalSpeed;
        }
    }
}