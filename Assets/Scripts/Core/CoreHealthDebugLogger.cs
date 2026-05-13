using UnityEngine;

namespace CoreBreach.Core
{

    public class CoreHealthDebugLogger : MonoBehaviour
    {
        [SerializeField] private CoreHealth coreHealth;

        private void OnEnable()
        {
            if (coreHealth == null) return;
            coreHealth.OnHealthChanged += HandleHealthChanged;
            coreHealth.OnCoreDestroyed += HandleCoreDestroyed;
        }

        private void OnDisable()
        {
            if (coreHealth == null) return;
            coreHealth.OnHealthChanged -= HandleHealthChanged;
            coreHealth.OnCoreDestroyed -= HandleCoreDestroyed;
        }

        private void HandleHealthChanged(int current, int max)
        {
            Debug.Log($"[CoreHealthDebugLogger] Health changed: {current}/{max}");
        }

        private void HandleCoreDestroyed()
        {
            Debug.Log("[CoreHealthDebugLogger] Core destroyed!");
        }
    }
}