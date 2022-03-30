using UnityEngine;
using AK.UnitsStats;

namespace AK.Collisions
{
    [RequireComponent(typeof(Stats))]
    public class Collisioner : MonoBehaviour
    {
        [SerializeField] string damagerTag = "Damager";
        [SerializeField] Collider2D groundCollider = null;
        [SerializeField] Collider2D ladderCheckerCollier = null;

        Stats stats;

        public float GetColliderMinYBound { get => groundCollider.bounds.min.y; }

        public void Awake() { stats = GetComponent<Stats>(); }

        public bool IsTouchingGround(LayerMask layer) { return groundCollider.IsTouchingLayers(layer); }
        public bool IsOnLadder(LayerMask layer) { return ladderCheckerCollier.IsTouchingLayers(layer); }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(damagerTag))
            {
                stats.ModifyHealth(other.GetComponent<DamagerStats>().GetDamageDealt);
            }

            if (transform.CompareTag("Player") && other.CompareTag("Sign"))
            {
                other.GetComponent<Animator>().SetBool("Reading", true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (transform.CompareTag("Player") && other.CompareTag("Sign"))
            {
                other.GetComponent<Animator>().SetBool("Reading", false);
            }
        }
    }
}