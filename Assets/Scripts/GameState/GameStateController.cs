using System;
using UnityEngine;
using CoreBreach.Core;
using CoreBreach.Waves;

namespace CoreBreach.GameState
{
    public class GameStateController : MonoBehaviour
    {
        [SerializeField] private CoreHealth   coreHealth;
        [SerializeField] private WaveSpawner  waveSpawner;
        [SerializeField] private bool         pauseTimeOnEnd = true;

        public event Action OnGameWon;
        public event Action OnGameLost;

        public bool GameEnded { get; private set; }

        private void OnEnable()
        {
            if (coreHealth   != null) coreHealth.OnCoreDestroyed         += HandleCoreDestroyed;
            if (waveSpawner  != null) waveSpawner.OnAllWavesCompleted    += HandleAllWavesCompleted;
        }

        private void OnDisable()
        {
            if (coreHealth   != null) coreHealth.OnCoreDestroyed         -= HandleCoreDestroyed;
            if (waveSpawner  != null) waveSpawner.OnAllWavesCompleted    -= HandleAllWavesCompleted;
        }

        private void HandleCoreDestroyed()
        {
            if (GameEnded) return;
            EndGame(won: false);
        }

        private void HandleAllWavesCompleted()
        {
            if (GameEnded) return;
            EndGame(won: true);
        }

        private void EndGame(bool won)
        {
            GameEnded = true;
            if (pauseTimeOnEnd) Time.timeScale = 0f;

            if (won) OnGameWon?.Invoke();
            else     OnGameLost?.Invoke();
        }
    }
}