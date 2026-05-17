using System;
using UnityEngine;

namespace CoreBreach.Upgrades
{
    public abstract class WeaponDecoratorBase : IWeapon
    {
        protected readonly IWeapon Inner;

        protected WeaponDecoratorBase(IWeapon inner)
        {
            Inner = inner;
        }

        public virtual float CooldownSeconds => Inner.CooldownSeconds;

        public abstract void Fire(Vector2 origin, Vector2 direction, Action<ShotRequest> spawn);
    }
}