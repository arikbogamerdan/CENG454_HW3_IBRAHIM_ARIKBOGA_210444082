using System;
using UnityEngine;
using CoreBreach.Combat;

namespace CoreBreach.Enemies
{
    public class EnemyHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private int maxHealth = 50;

        private int currentHealth;
        public int MaxHealth => maxHealth;
        public int CurrentHealth => currentHealth;
        public bool IsAlive => currentHealth > 0;
        public event Action<EnemyHealth> OnEnemyDied;

        private void Awake()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(int amount)
        {
            if (!IsAlive || amount <= 0) return;

            currentHealth = Mathf.Max(0, currentHealth - amount);

            if (currentHealth == 0)
            {
                OnEnemyDied?.Invoke(this);
                Destroy(gameObject);
            }
        }
    }
}