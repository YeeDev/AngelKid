using System;
using UnityEngine;
using AK.UnitsStats;

namespace AK.Collisions
{
    [RequireComponent(typeof(Stats))]
    public class Collisioner : MonoBehaviour
    {
        public event Action OnGrabItem;

        [SerializeField] string damagerTag = "Damager";
        [SerializeField] Collider2D groundCollider = null;
        [SerializeField] Collider2D ladderCheckerCollier = null;

        Stats stats;

        public float GetColliderMinYBound { get => groundCollider.bounds.min.y; }

        public void Awake() { stats = GetComponent<Stats>(); }

        public bool IsTouchingGround(LayerMask layer) { return groundCollider.IsTouchingLayers(layer); }
        public bool IsOnLadder(LayerMask layer) { return ladderCheckerCollier.IsTouchingLayers(layer); }

        public void GrabItem(GameObject item)
        {
            if (OnGrabItem != null)
            {
                OnGrabItem();
                Destroy(item);
            }
        }

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

            if (transform.CompareTag("Player") && other.CompareTag("Gem")) { GrabItem(other.gameObject); }
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