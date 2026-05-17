using System;
using UnityEngine;

namespace CoreBreach.Upgrades
{
    public class RapidFireWeaponDecorator : WeaponDecoratorBase
    {
        private readonly float cooldownMultiplier;

        public RapidFireWeaponDecorator(IWeapon inner, float cooldownMultiplier) : base(inner)
        {
            this.cooldownMultiplier = Mathf.Max(0.01f, cooldownMultiplier);
        }

        public override float CooldownSeconds => Inner.CooldownSeconds * cooldownMultiplier;

        public override void Fire(Vector2 origin, Vector2 direction, Action<ShotRequest> spawn)
        {
            Inner.Fire(origin, direction, spawn);
        }
    }
}