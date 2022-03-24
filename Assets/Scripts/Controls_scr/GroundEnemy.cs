using UnityEngine;
using AK.Movements;
using AK.Collisions;
using AK.UnitsStats;

namespace AK.Controls
{
    [RequireComponent(typeof(Mover))]
    [RequireComponent(typeof(Collisioner))]
    public class GroundEnemy : MonoBehaviour
    {
        [SerializeField] bool ignoresFalls = false;
        [Range(-1, 1)] [SerializeField] float movingDirection = 1f;
        [SerializeField] Collider2D mainCollider = null;

        Mover mover;
        Stats stats;

        private void Awake()
        {
            mover = GetComponent<Mover>();
            stats = GetComponent<Stats>();
        }

        private void Update()
        {
            if (stats.IsUnitDeath) { Destroy(gameObject); }

            MoveBehaviour();
        }

        private void MoveBehaviour()
        {
            if (ignoresFalls && !mainCollider.IsTouchingLayers(LayerMask.GetMask("Jumpable")))
            {
                mover.StopRigidbody(true);
                return;
            }

            mover.Move(movingDirection, 0, false);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Ground") && !ignoresFalls)
            {
                FlipMovingDirection();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision) { if (collision.CompareTag("Wall")) { FlipMovingDirection(); } }

        private void FlipMovingDirection() { movingDirection *= -1; }
    }
}