using UnityEngine;
using AK.Missiles;
using AK.Animations;
using AK.Collisions;

namespace AK.Attacks
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] GameObject missilePrefab = null;
        [SerializeField] Transform muzzle = null;

        Animater animater;
        SoundPlayer soundPlayer;
        Collisioner collisioner;

        private void Awake()
        {
            animater = GetComponent<Animater>();
            collisioner = GetComponent<Collisioner>();
            soundPlayer = GetComponent<SoundPlayer>();
        }

        //Called in Animation AngelKid_Shoot
        private void Shoot()
        {
            soundPlayer.PlayShootClip();
            GameObject missileInstance = Instantiate(missilePrefab, muzzle.position, Quaternion.identity);
            Missile missile = missileInstance.GetComponent<Missile>();
            missile.InitializeArrow(collisioner, animater.GetLookingDirection);
        }
    }
}