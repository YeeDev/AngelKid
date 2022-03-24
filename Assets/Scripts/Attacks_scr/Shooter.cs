using UnityEngine;

namespace AK.Attacks
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] GameObject missilePrefab = null;
        [SerializeField] Transform muzzle = null;
        [SerializeField] float fireRate = 1f;

        float fireRateTimer;

        private void Awake() { fireRateTimer = fireRate; }

        public void AddToTimer() { fireRateTimer += Time.deltaTime; }

        public void Shoot()
        {
            if (fireRateTimer < fireRate) { return; }

            fireRateTimer = 0;
            GameObject missileInstance = Instantiate(missilePrefab, muzzle.position, Quaternion.identity);
            Missile missile = missileInstance.GetComponent<Missile>();
            missile.SetDirection(transform.localScale.x);
        }
    }
}