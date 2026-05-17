using CoreBreach.Waves;
using TMPro;
using UnityEngine;

namespace CoreBreach.UI
{
    public class WaveHUD : MonoBehaviour
    {
        [SerializeField] private WaveSpawner waveSpawner;
        [SerializeField] private TMP_Text    waveLabel;

        [Header("Text Templates")]
        [SerializeField] private string startedFormat   = "WAVE {0} / {1}";
        [SerializeField] private string completedFormat = "WAVE {0} CLEARED";
        [SerializeField] private string allClearedText  = "ALL WAVES CLEARED";

        private void OnEnable()
        {
            if (waveSpawner == null) return;
            waveSpawner.OnWaveStarted        += HandleWaveStarted;
            waveSpawner.OnWaveCompleted      += HandleWaveCompleted;
            waveSpawner.OnAllWavesCompleted  += HandleAllWavesCompleted;
        }

        private void OnDisable()
        {
            if (waveSpawner == null) return;
            waveSpawner.OnWaveStarted        -= HandleWaveStarted;
            waveSpawner.OnWaveCompleted      -= HandleWaveCompleted;
            waveSpawner.OnAllWavesCompleted  -= HandleAllWavesCompleted;
        }

        private void HandleWaveStarted(int waveIndex)
        {
            SetText(string.Format(startedFormat, waveIndex + 1, waveSpawner.TotalWaves));
        }

        private void HandleWaveCompleted(int waveIndex)
        {
            SetText(string.Format(completedFormat, waveIndex + 1));
        }

        private void HandleAllWavesCompleted()
        {
            SetText(allClearedText);
        }

        private void SetText(string s)
        {
            if (waveLabel != null) waveLabel.text = s;
        }
    }
}