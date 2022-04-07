using UnityEngine;
using AK.Animations;
using AK.Collisions;

namespace AK.Missiles
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Missile : MonoBehaviour
    {
        [Range(-100, 100)] [SerializeField] float missileSpeed = 18f;
        [SerializeField] GameObject shootEffect = null;
        [SerializeField] Transform spawnPoint = null;

        float direction;
        Animater animater;
        Rigidbody2D rb;
        Collisioner collisioner;

        //Called in Shooter Shoot()
        public void InitializeArrow(Collisioner collisioner, float value)
        {
            this.collisioner = collisioner;
            direction = Mathf.Sign(value);
            animater.CheckIfFlip(direction);
        }

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animater = GetComponent<Animater>();
        }

        private void Update() { rb.velocity = Vector2.right * direction * missileSpeed; }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.transform.CompareTag("Gem"))
            {
                collisioner.GrabItem(collider.gameObject);
                Destroy(collider.gameObject);
            }

            Instantiate(shootEffect, spawnPoint.position, Quaternion.identity);
            Dissapear();
        }

        private void Dissapear() { Destroy(gameObject); }

        private void OnBecameInvisible() { Dissapear(); }
    }
}