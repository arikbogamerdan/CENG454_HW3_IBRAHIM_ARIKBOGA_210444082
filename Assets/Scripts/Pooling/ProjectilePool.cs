using UnityEngine;
using CoreBreach.Combat;

namespace CoreBreach.Pooling
{
    public class ProjectilePool : MonoBehaviour
    {
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private int prewarmCount = 16;
        [SerializeField] private Transform poolParent;

        private ObjectPool<Projectile> pool;

        public int CountAvailable => pool?.CountAvailable ?? 0;

        private void Awake()
        {
            if (poolParent == null) poolParent = transform;
            pool = new ObjectPool<Projectile>(projectilePrefab, poolParent, prewarmCount);
        }

        public Projectile Get() => pool.Get();
        public void Release(Projectile projectile) => pool.Release(projectile);
    }
}