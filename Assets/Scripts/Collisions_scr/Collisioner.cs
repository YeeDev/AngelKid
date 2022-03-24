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

        public void InitializeCollisioner(Stats stats) { this.stats = stats; }

        public bool IsTouchingLayer(LayerMask layer) { return groundCollider.IsTouchingLayers(layer); }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(damagerTag))
            {
                //TODO Grab Damage from another class.
                stats.ModifyHealth(-1);
            }
        }
    }
}