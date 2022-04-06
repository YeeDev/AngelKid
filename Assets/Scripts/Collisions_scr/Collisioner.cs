using System;
using UnityEngine;
using AK.UnitsStats;
using AK.Animations;

namespace AK.Collisions
{
    [RequireComponent(typeof(Stats))]
    public class Collisioner : MonoBehaviour
    {
        public event Action OnGrabItem;

        [SerializeField] string damagerTag = "Damager";
        [SerializeField] Collider2D groundCollider = null;
        [SerializeField] Collider2D ladderCheckerCollier = null;

        bool isInvincible;
        Transform pusher;
        Stats stats;
        Animater animater;

        public float GetColliderMinYBound { get => groundCollider.bounds.min.y; }
        public Transform GetPusher { get => pusher; }

        public void Awake()
        {
            stats = GetComponent<Stats>();
            animater = GetComponent<Animater>();
        }

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

        private void MakeInvulnerable() { isInvincible = true; }
        private void MakeVulnerable() { isInvincible = false; }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(damagerTag) && !isInvincible)
            {
                pusher = other.transform;
                stats.ModifyHealth(other.GetComponentInParent<DamagerStats>().GetDamageDealt);
                animater.TriggerTakeDamage(stats.GetCurrentHealth);
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