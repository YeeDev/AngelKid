using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AK.UnitsStats;

namespace AK.Collisions
{
    [RequireComponent(typeof(Stats))]
    public class Collisioner : MonoBehaviour
    {
        [SerializeField] string damagerTag = "Damager";

        Stats stats;
        public void SetStats(Stats value) { stats = value; }

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
            if (other.CompareTag(damagerTag))
            {
                //TODO Grab Damage from another class.
                stats.ModifyHealth(-1);
            }
        }
    }
}