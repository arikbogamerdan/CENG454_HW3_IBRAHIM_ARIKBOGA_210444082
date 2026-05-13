using UnityEngine;

namespace CoreBreach.Upgrades
{
    public readonly struct ShotRequest
    {
        public readonly Vector2 Origin;
        public readonly Vector2 Direction;
        public readonly int Damage;
        public readonly float Speed;

        public ShotRequest(Vector2 origin, Vector2 direction, int damage, float speed)
        {
            Origin = origin;
            Direction = direction;
            Damage = damage;
            Speed = speed;
        }
    }
}