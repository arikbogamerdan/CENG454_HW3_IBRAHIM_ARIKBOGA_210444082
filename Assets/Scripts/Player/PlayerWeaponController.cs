using System;
using UnityEngine;
using CoreBreach.Combat;
using CoreBreach.Pooling;
using CoreBreach.Upgrades;

namespace CoreBreach.Player
{
    public class PlayerWeaponController : MonoBehaviour
    {
        [SerializeField] private PlayerInputReader inputReader;
        [SerializeField] private ProjectilePool pool;
        [SerializeField] private Transform muzzle;
        [SerializeField] private Camera worldCamera;

        [Header("Default Weapon (BasicBlaster)")]
        [SerializeField] private int baseDamage = 25;
        [SerializeField] private float baseProjectileSpeed = 12f;
        [SerializeField] private float baseCooldownSeconds = 0.25f;

        private IWeapon currentWeapon;
        private float cooldownRemaining;
        private Action<ShotRequest> spawnDelegate;

        public IWeapon CurrentWeapon => currentWeapon;

        private void Awake()
        {
            if (worldCamera == null) worldCamera = Camera.main;
            spawnDelegate = SpawnFromShot;

            currentWeapon = new BasicBlaster(
                damage: baseDamage,
                speed: baseProjectileSpeed,
                cooldownSeconds: baseCooldownSeconds);
        }

        private void Update()
        {
            if (cooldownRemaining > 0f)
            {
                cooldownRemaining -= Time.deltaTime;
            }

            if (inputReader == null || pool == null || muzzle == null || worldCamera == null) return;
            if (!inputReader.FireHeld) return;
            if (cooldownRemaining > 0f) return;

            FireOnce();
        }

        private void FireOnce()
        {
            Vector3 mouseWorld = worldCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dir = ((Vector2)mouseWorld - (Vector2)muzzle.position).normalized;
            if (dir.sqrMagnitude < 0.0001f) dir = Vector2.right;

            currentWeapon.Fire(muzzle.position, dir, spawnDelegate);
            cooldownRemaining = currentWeapon.CooldownSeconds;
        }

        private void SpawnFromShot(ShotRequest shot)
        {
            Projectile p = pool.Get();
            p.Launch(shot.Origin, shot.Direction, shot.Damage, shot.Speed, pool);
        }

        public void WrapWeapon(IWeapon decoratedWeapon)
        {
            if (decoratedWeapon == null) return;
            currentWeapon = decoratedWeapon;
        }
    }
}