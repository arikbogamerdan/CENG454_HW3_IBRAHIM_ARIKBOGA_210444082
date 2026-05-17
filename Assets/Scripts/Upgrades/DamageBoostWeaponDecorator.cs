using System;
using UnityEngine;

namespace CoreBreach.Upgrades
{
    public class DamageBoostWeaponDecorator : WeaponDecoratorBase
    {
        private readonly int damageBonus;

        public DamageBoostWeaponDecorator(IWeapon inner, int damageBonus) : base(inner)
        {
            this.damageBonus = damageBonus;
        }

        public override void Fire(Vector2 origin, Vector2 direction, Action<ShotRequest> spawn)
        {
            Inner.Fire(origin, direction, shot =>
            {
                spawn(new ShotRequest(shot.Origin, shot.Direction,
                                      shot.Damage + damageBonus, shot.Speed));
            });
        }
    }
}