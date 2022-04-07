using System;
using UnityEngine;
using AK.UnitsStats;
using AK.Animations;

namespace AK.Collisions
{
    [RequireComponent(typeof(Stats))]
    public class Collisioner : MonoBehaviour
    {
        enum CollisionsStates { Enter, Stay, Exit };

        public event Action OnGrabItem;

        [SerializeField] Collider2D groundCollider = null;
        [SerializeField] Collider2D ladderCheckerCollier = null;
        [SerializeField] Transform itemSoundTransform = null;

        bool isInvincible;
        Transform pusher;
        Stats stats;
        Animater animater;
        AudioSource itemSoundPlayer;

        //Used in Controller StopClimb()
        public float GetColliderMinYBound { get => groundCollider.bounds.min.y; }
        //Used in Controller TakeDamage()
        public Transform GetPusher { get => pusher; }

        public void Awake()
        {
            stats = GetComponent<Stats>();
            animater = GetComponent<Animater>();
            itemSoundPlayer = itemSoundTransform.GetComponent<AudioSource>();
        }

        //Used in Controller ReadEnterDoorInput() and CheckGroundedState()
        public bool IsTouchingGround(LayerMask layer) { return groundCollider.IsTouchingLayers(layer); }
        //Used in Controller StartClimb()
        public bool IsOnLadder(LayerMask layer) { return ladderCheckerCollier.IsTouchingLayers(layer); }

        //Called in Animations
        private void MakeVulnerable() { isInvincible = false; }

        private void OnTriggerEnter2D(Collider2D other) { ActionOnCollisionType(other.transform, CollisionsStates.Enter); }
        private void OnTriggerStay2D(Collider2D other) { ActionOnCollisionType(other.transform, CollisionsStates.Stay); }
        private void OnTriggerExit2D(Collider2D other) { ActionOnCollisionType(other.transform, CollisionsStates.Exit); }

        private void ActionOnCollisionType(Transform other, CollisionsStates state)
        {
            if (other.CompareTag("Damager") && state == CollisionsStates.Stay) { TakeDamage(other.transform); }
            else if (other.CompareTag("Sign") && state != CollisionsStates.Stay) { ReadSign(other.transform, state == CollisionsStates.Enter); }
            else if (other.CompareTag("Gem") && state == CollisionsStates.Enter) { GrabItem(other.gameObject); }
        }

        private void TakeDamage(Transform damager)
        {
            if (isInvincible) { return; }

            isInvincible = true;
            pusher = damager.transform;
            stats.ModifyHealth(damager.GetComponentInParent<DamagerStats>().GetDamageDealt);
            animater.TriggerTakeDamage(stats.GetCurrentHealth);
        }

        //Used locally and on Missile OnTriggerEnter2D
        public void GrabItem(GameObject item)
        {
            if (OnGrabItem != null)
            {
                OnGrabItem();
                itemSoundTransform.position = item.transform.position;
                itemSoundPlayer.Play();
                Destroy(item);
            }
        }

        private void ReadSign(Transform sign, bool isReading) { sign.GetComponent<Animator>().SetBool("Reading", isReading); }
    }
}