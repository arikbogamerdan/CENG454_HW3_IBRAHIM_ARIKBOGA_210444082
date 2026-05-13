using System;
using UnityEngine;
using CoreBreach.Combat;

namespace CoreBreach.Core
{

    public class CoreHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private int maxHealth = 100;
        private int currentHealth;
        public int MaxHealth => maxHealth;
        public int CurrentHealth => currentHealth;
        public bool IsAlive => currentHealth > 0;
        public event Action<int, int> OnHealthChanged; 
        public event Action OnCoreDestroyed;

        private void Awake()
        {
            currentHealth = maxHealth;
        }

        private void Start()
        {
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
        }

        public void TakeDamage(int amount)
        {
            if (!IsAlive || amount <= 0) return;

            currentHealth = Mathf.Max(0, currentHealth - amount);
            OnHealthChanged?.Invoke(currentHealth, maxHealth);

            if (currentHealth == 0)
            {
                OnCoreDestroyed?.Invoke();
            }
        }

        [ContextMenu("Debug: Take 10 Damage")]
        private void DebugTake10Damage() => TakeDamage(10);
    }
}