using System;
using UnityEngine;

namespace AK.UnitsStats
{
    public class Stats : MonoBehaviour
    {
        public event Action OnHealthChange;

        [Range(0, 10)] [SerializeField] int health = 3;

        //Used in Contronller Update()
        public bool IsUnitDeath { get => health <= 0; }
        //Used in Collisioner OnTriggerEnter2D and HealthBarUpdater UpdateHealthBar()
        public int GetCurrentHealth { get => health; }

        //Called in Collisioner OnTriggerEnter2D
        public void ModifyHealth(int amount)
        {
            health += amount;

            if (OnHealthChange != null) { OnHealthChange(); }
        }
    }
}