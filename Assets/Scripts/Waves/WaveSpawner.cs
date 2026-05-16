using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoreBreach.Enemies;

namespace CoreBreach.Waves
{
    public class WaveSpawner : MonoBehaviour
    {
        [SerializeField] private WaveDefinition[] waves;
        [SerializeField] private Transform[]     spawnPoints;
        [SerializeField] private Transform       target;
        [SerializeField] private float           initialDelay = 1.5f;

        public event Action<int> OnWaveStarted;
        public event Action<int> OnWaveCompleted;
        public event Action       OnAllWavesCompleted;
        public int CurrentWaveIndex { get; private set; } = -1;
        public int TotalWaves => waves != null ? waves.Length : 0;

        private int aliveEnemiesInWave;
        private readonly List<EnemyHealth> subscribedEnemies = new List<EnemyHealth>();

        private void Start()
        {
            if (waves == null || waves.Length == 0)
            {
                Debug.LogWarning("[WaveSpawner] No waves assigned.");
                return;
            }
            StartCoroutine(RunWaves());
        }

        private IEnumerator RunWaves()
        {
            yield return new WaitForSeconds(initialDelay);

            for (int i = 0; i < waves.Length; i++)
            {
                CurrentWaveIndex = i;
                OnWaveStarted?.Invoke(i);
                yield return StartCoroutine(RunWave(waves[i]));
                OnWaveCompleted?.Invoke(i);
            }

            OnAllWavesCompleted?.Invoke();
        }

        private IEnumerator RunWave(WaveDefinition wave)
        {
            aliveEnemiesInWave = 0;

            foreach (var spec in wave.enemies)
            {
                for (int j = 0; j < spec.count; j++)
                {
                    SpawnEnemy(spec);
                    yield return new WaitForSeconds(spec.spawnInterval);
                }
            }

            while (aliveEnemiesInWave > 0)
            {
                yield return null;
            }

            yield return new WaitForSeconds(wave.postWaveDelay);
        }

        private void SpawnEnemy(WaveDefinition.EnemySpec spec)
        {
            if (spec.enemyPrefab == null) return;
            if (spawnPoints == null || spawnPoints.Length == 0) return;

            Transform sp = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
            GameObject go = Instantiate(spec.enemyPrefab, sp.position, Quaternion.identity);

            var movement = go.GetComponent<EnemyMovement>();
            if (movement != null)
            {
                movement.SetTarget(target);
                movement.SetStrategy(BuildStrategy(spec));
            }

            var health = go.GetComponent<EnemyHealth>();
            if (health != null)
            {
                aliveEnemiesInWave++;
                subscribedEnemies.Add(health);
                health.OnEnemyDied += HandleEnemyDied;
            }
        }

        private void HandleEnemyDied(EnemyHealth enemy)
        {
            enemy.OnEnemyDied -= HandleEnemyDied;
            subscribedEnemies.Remove(enemy);
            aliveEnemiesInWave--;
        }

        private IMovementStrategy BuildStrategy(WaveDefinition.EnemySpec spec)
        {
            switch (spec.movementKind)
            {
                case MovementKind.ZigZag:
                    return new ZigZagCoreChaseStrategy(spec.movementSpeed);
                case MovementKind.Direct:
                default:
                    return new DirectCoreChaseStrategy(spec.movementSpeed);
            }
        }

        private void OnDestroy()
        {
            for (int i = 0; i < subscribedEnemies.Count; i++)
            {
                if (subscribedEnemies[i] != null)
                    subscribedEnemies[i].OnEnemyDied -= HandleEnemyDied;
            }
            subscribedEnemies.Clear();
        }
    }
}