using UnityEngine;
using AK.Missiles;
using AK.Animations;

namespace AK.Attacks
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] GameObject missilePrefab = null;
        [SerializeField] Transform muzzle = null;
        [SerializeField] float fireRate = 1f;

        float fireRateTimer;
        Animater animater;

        private void Awake()
        {
            fireRateTimer = fireRate;
            animater = GetComponent<Animater>();
        }

        public void AddToTimer() { fireRateTimer += Time.deltaTime; }

        public void Shoot(float direction)
        {
            if (fireRateTimer < fireRate) { return; }

            fireRateTimer = 0;
            GameObject missileInstance = Instantiate(missilePrefab, muzzle.position, Quaternion.identity);
            Missile missile = missileInstance.GetComponent<Missile>();
            missile.SetDirection(direction);
            animater.SetShootLayerWeight();
        }
    }
}