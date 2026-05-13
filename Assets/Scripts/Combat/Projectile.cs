using UnityEngine;
using CoreBreach.Pooling;

namespace CoreBreach.Combat
{

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class Projectile : MonoBehaviour, IPoolable
    {
        [SerializeField] private float defaultSpeed = 12f;
        [SerializeField] private float lifetime = 3f;

        private Rigidbody2D rb;
        private float remainingLifetime;
        private int damage;
        private ProjectilePool owningPool;
        private bool hasHit; 

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        public void Launch(Vector2 origin, Vector2 direction, int damage,
                           float speedOverride, ProjectilePool pool)
        {
            transform.position = origin;
            this.damage = damage;
            this.owningPool = pool;
            this.hasHit = false;

            float speed = speedOverride > 0f ? speedOverride : defaultSpeed;
            rb.linearVelocity = direction.normalized * speed;
            remainingLifetime = lifetime;
        }

        private void Update()
        {
            remainingLifetime -= Time.deltaTime;
            if (remainingLifetime <= 0f)
            {
                ReturnToPool();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (hasHit) return;
            if (!other.TryGetComponent<IDamageable>(out var target)) return;
            if (!target.IsAlive) return;

            hasHit = true;
            target.TakeDamage(damage);
            ReturnToPool();
        }

        private void ReturnToPool()
        {
            if (owningPool != null) owningPool.Release(this);
            else gameObject.SetActive(false);
        }


        public void OnSpawned()
        {
            hasHit = false;
            remainingLifetime = lifetime;
        }

        public void OnDespawned()
        {
            if (rb != null) rb.linearVelocity = Vector2.zero;
            damage = 0;
            owningPool = null;
            hasHit = false;
        }
    }
}