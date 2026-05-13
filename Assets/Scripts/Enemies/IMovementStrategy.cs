using UnityEngine;

namespace CoreBreach.Enemies
{
    public interface IMovementStrategy
    {
        void Move(Transform self, Transform target, float deltaTime);
    }
}