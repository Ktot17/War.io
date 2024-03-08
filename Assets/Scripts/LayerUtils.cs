using UnityEngine;

namespace War.io
{
    public static class LayerUtils
    {
        public const string BulletLayerName = "Bullet";
        public const string EnemyLayerName = "Enemy";
        public const string PlayerLayerName = "Player";
        public const string PickUpLayerName = "PickUp";

        public static readonly int BulletLayer = LayerMask.NameToLayer(BulletLayerName);
        public static readonly int PickUpLayer = LayerMask.NameToLayer(PickUpLayerName);

        public static readonly int CharacterMask = LayerMask.GetMask(EnemyLayerName, PlayerLayerName);

        public static bool IsBullet(GameObject other) => other.layer == BulletLayer;
        public static bool IsPickUp(GameObject other) => other.layer == PickUpLayer;
    }
}