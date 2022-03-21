using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AK.Attacks
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] GameObject missilePrefab = null;
        [SerializeField] Transform muzzle = null;

        public void Shoot()
        {
            GameObject missileInstance = Instantiate(missilePrefab, muzzle.position, Quaternion.identity);
            Missile missile = missileInstance.GetComponent<Missile>();
            missile.SetDirection(transform.localScale.x);
        }
    }
}