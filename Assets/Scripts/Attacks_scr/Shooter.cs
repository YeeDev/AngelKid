using UnityEngine;
using AK.Missiles;
using AK.Animations;

namespace AK.Attacks
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] GameObject missilePrefab = null;
        [SerializeField] Transform muzzle = null;

        Animater animater;

        private void Awake() { animater = GetComponent<Animater>(); }

        public void Shoot()
        {
            GameObject missileInstance = Instantiate(missilePrefab, muzzle.position, Quaternion.identity);
            Missile missile = missileInstance.GetComponent<Missile>();
            missile.SetDirection(animater.GetLookingDirection);
        }
    }
}