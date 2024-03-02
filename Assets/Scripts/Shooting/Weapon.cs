using UnityEngine;

namespace War.io.Shooting
{
    public class Weapon : MonoBehaviour
    {
        [field: SerializeField] public Bullet BulletPrefab { get; private set; }

        [field: SerializeField] public float ShootRadius { get; private set; } = 5f;

        [field: SerializeField] public float ShootFrequencySec { get; private set; } = 1f;

        [SerializeField] private float bulletMaxFlyDistance = 10f;

        [SerializeField] private float bulletFlySpeed = 10f;

        [SerializeField] private Transform bulletSpawnPosition;

        [SerializeField] private float damage = 2f;

        public void Shoot(Vector3 targetPoint)
        {
            var position = bulletSpawnPosition.position;
            var bullet = Instantiate(BulletPrefab, position, Quaternion.identity);

            var target = targetPoint - position;
            target.y = 0;
            target.Normalize();
            
            bullet.Initialise(target, bulletFlySpeed, bulletMaxFlyDistance, damage);
        }
    }
}