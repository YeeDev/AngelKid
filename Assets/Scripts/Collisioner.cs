using UnityEngine;
using AK.UnitsStats;
using AK.Core;

namespace AK.Collisions
{
    [RequireComponent(typeof(Stats))]
    public class Collisioner : MonoBehaviour
    {
        [SerializeField] string damagerTag = "Damager";

        Stats stats;
        LevelLoader levelLoader;

        public void SetStats(Stats value) { stats = value; }

        private void Awake() { levelLoader = GameObject.FindWithTag("GameController").GetComponent<LevelLoader>(); }

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

            if (transform.CompareTag("Player") && other.CompareTag("Exit"))
            {
                StartCoroutine(levelLoader.LoadLevel());
            }
        }
    }
}