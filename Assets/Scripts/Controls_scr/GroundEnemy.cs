using UnityEngine;
using AK.Collisions;
using AK.UnitsStats;
using AK.Animations;

namespace AK.Controls
{
    [RequireComponent(typeof(Animater))]
    [RequireComponent(typeof(Collisioner))]
    public class GroundEnemy : MonoBehaviour
    {
        [SerializeField] bool ignoresFalls = false;
        [Range(-1, 1)] [SerializeField] float startingDirection = 1f;
        [Range(0, 10)] [SerializeField] float moveSpeed = 0f;
        [SerializeField] Collider2D checkCollider = null;

        bool stopMoving;
        Stats stats;
        Animater animater;
        Rigidbody2D rb;

        private void Awake()
        {
            stats = GetComponent<Stats>();
            rb = GetComponent<Rigidbody2D>();
            animater = GetComponent<Animater>();
        }

        private void Update() { MoveBehaviour(); }

        private void DestroyEnemy() { Destroy(gameObject); }

        private void StopMoving() { stopMoving = true; }
        private void RestoreMovement() { stopMoving = false; }

        private void MoveBehaviour()
        {
            if ((ignoresFalls && !checkCollider.IsTouchingLayers(LayerMask.GetMask("Jumpable"))) || stopMoving)
            {
                rb.velocity = Vector2.zero;
                return;
            }

            rb.velocity = new Vector2(startingDirection * moveSpeed, 0);
            animater.CheckIfFlip(rb.velocity.x);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Ground") && !ignoresFalls)
            {
                FlipMovingDirection();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision) { if (collision.CompareTag("Wall")) { FlipMovingDirection(); } }

        private void FlipMovingDirection() { startingDirection *= -1; }
    }
}