using UnityEngine;
using AK.Animations;
using AK.Collisions;

namespace AK.Missiles
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Missile : MonoBehaviour
    {
        [Range(-100, 100)] [SerializeField] float missileSpeed = 18f;
        [Range(0, 10)] [SerializeField] float timeBeforeDissapearing = 5f;

        float direction;
        Animater animater;
        Rigidbody2D rb;
        Collisioner collisioner;

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
            Invoke("Dissapear", timeBeforeDissapearing);
        }

        private void Update() { rb.velocity = Vector2.right * direction * missileSpeed; }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Gem"))
            {
                collisioner.GrabItem(collision.gameObject);
                Destroy(collision.gameObject);
            }

            Dissapear();
        }

        private void Dissapear() { Destroy(gameObject); }
    }
}