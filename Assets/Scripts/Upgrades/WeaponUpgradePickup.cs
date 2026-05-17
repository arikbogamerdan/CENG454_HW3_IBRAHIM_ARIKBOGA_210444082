using UnityEngine;
using CoreBreach.Player;

namespace CoreBreach.Upgrades
{
    public enum WeaponUpgradeKind
    {
        DamageBoost = 0,
        RapidFire   = 1,
        SplitShot   = 2
    }

    [RequireComponent(typeof(Collider2D))]
    public class WeaponUpgradePickup : MonoBehaviour
    {
        [Header("Upgrade Type")]
        [SerializeField] private WeaponUpgradeKind kind = WeaponUpgradeKind.DamageBoost;

        [Header("Parameters (only the relevant one is used)")]
        [SerializeField] private int   damageBonus        = 15;
        [SerializeField] private float cooldownMultiplier = 0.5f;   // 0.5 = twice as fast
        [SerializeField] private float splitSpreadDegrees = 15f;

        private bool consumed;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (consumed) return;
            if (!other.TryGetComponent<PlayerWeaponController>(out var controller)) return;

            IWeapon decorated = Decorate(controller.CurrentWeapon);
            controller.WrapWeapon(decorated);

            consumed = true;
            Destroy(gameObject);
        }

        private IWeapon Decorate(IWeapon inner)
        {
            switch (kind)
            {
                case WeaponUpgradeKind.RapidFire:
                    return new RapidFireWeaponDecorator(inner, cooldownMultiplier);
                case WeaponUpgradeKind.SplitShot:
                    return new SplitShotWeaponDecorator(inner, splitSpreadDegrees);
                case WeaponUpgradeKind.DamageBoost:
                default:
                    return new DamageBoostWeaponDecorator(inner, damageBonus);
            }
        }
    }
}