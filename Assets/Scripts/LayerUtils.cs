using UnityEngine;

namespace War.io
{
    public static class LayerUtils
    {
        public const string BulletLayerName = "Bullet";
        public const string EnemyLayerName = "Enemy";
        public const string PlayerLayerName = "Player";
        public const string WeaponPickUpLayerName = "WeaponPickUp";
        public const string BonusPickUpLayerName = "BonusPickUp";

        public static readonly int BulletLayer = LayerMask.NameToLayer(BulletLayerName);
        public static readonly int WeaponPickUpLayer = LayerMask.NameToLayer(WeaponPickUpLayerName);
        public static readonly int BonusPickUpLayer = LayerMask.NameToLayer(BonusPickUpLayerName);

        public static readonly int CharacterMask = LayerMask.GetMask(EnemyLayerName, PlayerLayerName);
        public static readonly int PlayerMask = LayerMask.GetMask(PlayerLayerName);
        public static readonly int PickUpsMask = LayerMask.GetMask(WeaponPickUpLayerName, BonusPickUpLayerName);

        public static bool IsBullet(GameObject other) => other.layer == BulletLayer;
        public static bool IsPickUp(GameObject other) => other.layer == WeaponPickUpLayer || 
                                                         other.layer == BonusPickUpLayer;

        public static bool IsWeaponPickUp(GameObject other) => other.layer == WeaponPickUpLayer;
    }
}