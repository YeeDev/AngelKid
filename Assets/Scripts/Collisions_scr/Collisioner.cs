using UnityEngine;
using AK.UnitsStats;
using AK.Animations;

namespace AK.Collisions
{
    [RequireComponent(typeof(Stats))]
    public class Collisioner : MonoBehaviour
    {
        [SerializeField] string damagerTag = "Damager";
        [SerializeField] Collider2D groundCollider = null;

        Stats stats;

        public float GetColliderMinYBound { get => groundCollider.bounds.min.y; }

        public bool IsTouchingLayer(LayerMask layer) { return groundCollider.IsTouchingLayers(layer); }

        public void InitializeCollisioner(Stats stats, Animater animater)
        {
            this.stats = stats;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.CompareTag(damagerTag))
            {
                //TODO Grab Damage from another class.
                stats.ModifyHealth(-1);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(damagerTag) && !transform.CompareTag("Player"))
            {
                //TODO Grab Damage from another class.
                stats.ModifyHealth(-1);
            }
        }
    }
}