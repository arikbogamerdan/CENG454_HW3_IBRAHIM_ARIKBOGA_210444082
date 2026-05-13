using System;
using UnityEngine;

namespace CoreBreach.Upgrades
{
    public interface IWeapon
    {
        float CooldownSeconds { get; }
        void Fire(Vector2 origin, Vector2 direction, Action<ShotRequest> spawn);
    }
}