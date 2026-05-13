using UnityEngine;

namespace CoreBreach.Enemies
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float defaultSpeed = 2f;

        private IMovementStrategy strategy;

        private void Awake()
        {
            strategy = new DirectCoreChaseStrategy(defaultSpeed);
        }

        public void SetStrategy(IMovementStrategy newStrategy)
        {
            if (newStrategy != null) strategy = newStrategy;
        }

        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
        }

        private void FixedUpdate()
        {
            if (strategy == null || target == null) return;
            strategy.Move(transform, target, Time.fixedDeltaTime);
        }
    }
}