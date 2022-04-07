using UnityEngine;
using AK.Animations;
using AK.UnitsStats;

namespace AK.Controls
{
    public class PatrollingEnemy : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 2f;
        [SerializeField] Transform[] patrolPoints = null;

        int currentPoint;
        Stats stats;
        Animater animater;
        AudioSource audioSource;

        private void Awake()
        {
            animater = GetComponent<Animater>();
            stats = GetComponent<Stats>();
            audioSource = GetComponent<AudioSource>();
        }

        private void PlayDeathSound() { audioSource.Play(); }

        private void Update() { Patrol(); }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("PlayerDamager"))
            {
                stats.ModifyHealth(other.GetComponentInParent<DamagerStats>().GetDamageDealt);
                animater.TriggerTakeDamage(stats.GetCurrentHealth);
            }
        }

        private void Patrol()
        {
            float delta = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPoint].position, delta);

            if (transform.position == patrolPoints[currentPoint].position)
            {
                currentPoint = (currentPoint + 1) % patrolPoints.Length;
                animater.CheckIfFlip(patrolPoints[currentPoint].position.x > transform.position.x ? 1 : -1);
            }
        }

        private void DestroyEnemy() { Destroy(gameObject); }
    }
}