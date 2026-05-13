using System;
using UnityEngine;

namespace CoreBreach.Upgrades
{
    public class BasicBlaster : IWeapon
    {
        private readonly int damage;
        private readonly float speed;

        public float CooldownSeconds { get; }

        public BasicBlaster(int damage, float speed, float cooldownSeconds)
        {
            this.damage = damage;
            this.speed = speed;
            CooldownSeconds = cooldownSeconds;
        }

        public void Fire(Vector2 origin, Vector2 direction, Action<ShotRequest> spawn)
        {
            spawn(new ShotRequest(origin, direction, damage, speed));
        }
    }
}