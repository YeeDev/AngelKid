using UnityEngine;

namespace AK.UnitsStats
{
    public class DamagerStats : MonoBehaviour
    {
        [Range(1, 100)][SerializeField] int damageDealt = 1;

        //Used by Collisioner OnTriggerEnter2D()
        public int GetDamageDealt { get => -damageDealt; }
    }
}