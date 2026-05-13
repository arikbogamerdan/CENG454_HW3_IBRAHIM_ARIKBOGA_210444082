using UnityEngine;
using CoreBreach.Pooling;

namespace CoreBreach.Combat
{
    public class ProjectileDebugSpawner : MonoBehaviour
    {
        [SerializeField] private ProjectilePool pool;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private int damage = 25;
        [SerializeField] private Camera worldCamera;

        private void Awake()
        {
            if (worldCamera == null) worldCamera = Camera.main;
        }

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Space)) return;
            Fire();
        }

        private void Fire()
        {
            if (pool == null || spawnPoint == null || worldCamera == null) return;

            Vector3 mouseWorld = worldCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dir = ((Vector2)mouseWorld - (Vector2)spawnPoint.position).normalized;
            if (dir.sqrMagnitude < 0.0001f) dir = Vector2.right;

            Projectile p = pool.Get();
            p.Launch(spawnPoint.position, dir, damage, speedOverride: 0f, pool: pool);

            Debug.Log($"[DebugSpawner] Fired. Pool available now: {pool.CountAvailable}");
        }
    }
}