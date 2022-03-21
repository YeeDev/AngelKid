using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AK.UnitsStats
{
    public class Stats : MonoBehaviour
    {
        [Range(0, 10)] [SerializeField] int health = 3;

        public bool IsUnitDeath { get => health <= 0; }

        public void ModifyHealth(int amount) { health += amount; }
    }
}