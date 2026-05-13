using UnityEngine;

namespace CoreBreach.Enemies
{
    public class DirectCoreChaseStrategy : IMovementStrategy
    {
        private readonly float speed;

        public DirectCoreChaseStrategy(float speed)
        {
            this.speed = speed;
        }

        public void Move(Transform self, Transform target, float deltaTime)
        {
            if (target == null) return;

            Vector2 toTarget = (Vector2)target.position - (Vector2)self.position;
            float distance = toTarget.magnitude;
            if (distance < 0.001f) return; 

            Vector2 step = (toTarget / distance) * speed * deltaTime;
            self.position = (Vector2)self.position + step;
        }
    }
}