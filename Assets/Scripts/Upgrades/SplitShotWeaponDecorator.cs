using System;
using UnityEngine;

namespace CoreBreach.Upgrades
{
    public class SplitShotWeaponDecorator : WeaponDecoratorBase
    {
        private readonly float spreadDegrees;

        public SplitShotWeaponDecorator(IWeapon inner, float spreadDegrees = 15f) : base(inner)
        {
            this.spreadDegrees = spreadDegrees;
        }

        public override void Fire(Vector2 origin, Vector2 direction, Action<ShotRequest> spawn)
        {
            Inner.Fire(origin, Rotate(direction, -spreadDegrees), spawn);
            Inner.Fire(origin, direction,                          spawn);
            Inner.Fire(origin, Rotate(direction, +spreadDegrees), spawn);
        }

        private static Vector2 Rotate(Vector2 v, float degrees)
        {
            float rad = degrees * Mathf.Deg2Rad;
            float cos = Mathf.Cos(rad);
            float sin = Mathf.Sin(rad);
            return new Vector2(v.x * cos - v.y * sin,
                               v.x * sin + v.y * cos);
        }
    }
}