using System;
using UnityEngine;

namespace AK.UnitsStats
{
    public class Stats : MonoBehaviour
    {
        public event Action OnHealthChange;

        [Range(0, 10)] [SerializeField] int health = 3;

        public bool IsUnitDeath { get => health <= 0; }
        public int GetCurrentHealth { get => health; }

        public void ModifyHealth(int amount)
        {
            health += amount;

            if (OnHealthChange != null) { OnHealthChange(); }
        }
    }
}