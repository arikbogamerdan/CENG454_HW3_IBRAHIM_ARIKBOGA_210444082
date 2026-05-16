using UnityEngine;

namespace CoreBreach.Enemies
{
    public class ZigZagCoreChaseStrategy : IMovementStrategy
    {
        private readonly float forwardSpeed;
        private readonly float sideAmplitude; 
        private readonly float sideFrequency;   
        private float phase;

        public ZigZagCoreChaseStrategy(float forwardSpeed,
                                       float sideAmplitude = 1.2f,
                                       float sideFrequency = 4f)
        {
            this.forwardSpeed  = forwardSpeed;
            this.sideAmplitude = sideAmplitude;
            this.sideFrequency = sideFrequency;
            this.phase = Random.value * Mathf.PI * 2f;
        }

        public void Move(Transform self, Transform target, float deltaTime)
        {
            if (target == null) return;

            Vector2 toTarget = (Vector2)target.position - (Vector2)self.position;
            float distance = toTarget.magnitude;
            if (distance < 0.001f) return;

            Vector2 forward = toTarget / distance;
            Vector2 right = new Vector2(forward.y, -forward.x);

            phase += deltaTime * sideFrequency;
            float sideVelocity = Mathf.Cos(phase) * sideAmplitude * sideFrequency;

            Vector2 velocity = forward * forwardSpeed + right * sideVelocity;
            self.position = (Vector2)self.position + velocity * deltaTime;
        }
    }
}