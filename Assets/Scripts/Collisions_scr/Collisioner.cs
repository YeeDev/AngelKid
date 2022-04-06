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

        //Used in Controller StopClimb()
        public float GetColliderMinYBound { get => groundCollider.bounds.min.y; }
        //Used in Controller TakeDamage()
        public Transform GetPusher { get => pusher; }

        public void Awake()
        {
            stats = GetComponent<Stats>();
            animater = GetComponent<Animater>();
        }

        //Used in Controller ReadEnterDoorInput() and CheckGroundedState()
        public bool IsTouchingGround(LayerMask layer) { return groundCollider.IsTouchingLayers(layer); }
        //Used in Controller StartClimb()
        public bool IsOnLadder(LayerMask layer) { return ladderCheckerCollier.IsTouchingLayers(layer); }

        //Used locally and on Missile OnTriggerEnter2D
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