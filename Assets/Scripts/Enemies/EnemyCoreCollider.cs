using UnityEngine;
using CoreBreach.Combat;

namespace CoreBreach.Enemies
{
    [RequireComponent(typeof(Collider2D))]
    public class EnemyCoreCollider : MonoBehaviour
    {
        [SerializeField] private int contactDamage = 10;

        private bool hasHit;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (hasHit) return;
            if (!other.TryGetComponent<IDamageable>(out var target)) return;
            if (!target.IsAlive) return;

            hasHit = true;
            target.TakeDamage(contactDamage);
            Destroy(gameObject);
        }
    }
}