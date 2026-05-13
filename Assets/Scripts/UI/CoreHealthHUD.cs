using CoreBreach.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CoreBreach.UI
{
 
    public class CoreHealthHUD : MonoBehaviour
    {
        [SerializeField] private CoreHealth coreHealth;
        [SerializeField] private Slider healthSlider;
        [SerializeField] private TMP_Text healthLabel;

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
            if (healthSlider != null)
            {
                healthSlider.maxValue = max;
                healthSlider.value = current;
            }
            if (healthLabel != null)
            {
                healthLabel.text = $"Core: {current} / {max}";
            }
        }

        private void HandleCoreDestroyed()
        {
            if (healthLabel != null)
            {
                healthLabel.text = "CORE DESTROYED";
            }
        }
    }
}