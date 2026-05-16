using System;
using UnityEngine;
using CoreBreach.Enemies;

namespace CoreBreach.Waves
{
    [CreateAssetMenu(fileName = "Wave", menuName = "CoreBreach/Wave Definition", order = 1)]
    public class WaveDefinition : ScriptableObject
    {
        [Serializable]
        public struct EnemySpec
        {
            public GameObject enemyPrefab;
            [Min(1)]   public int    count;
            [Min(0.05f)] public float spawnInterval;
            [Min(0.5f)]  public float movementSpeed;
            public MovementKind movementKind;
        }

        [Tooltip("Spawn bursts that make up this wave.")]
        public EnemySpec[] enemies;

        [Tooltip("Pause between this wave completing and the next wave starting.")]
        [Min(0f)] public float postWaveDelay = 2f;
    }
}