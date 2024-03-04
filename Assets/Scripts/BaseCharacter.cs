using UnityEngine;
using War.io.Movement;
using War.io.Shooting;

namespace War.io
{
    [RequireComponent(typeof(CharacterMovementController),
        typeof(ShootingController))]
    public class BaseCharacter : MonoBehaviour
    {
        [SerializeField] private Weapon baseWeaponPrefab;
        [SerializeField] private Transform hand;
        [SerializeField] private float health = 2f;
        
        private IMovementDirectionSource _movementDirectionSource;
        private ISprintingSource _sprintingSource;
        
        private CharacterMovementController _characterMovementController;
        private ShootingController _shootingController;
    
        protected void Awake()
        {
            _movementDirectionSource = GetComponent<IMovementDirectionSource>();
            _sprintingSource = GetComponent<ISprintingSource>();
            
            _characterMovementController = GetComponent<CharacterMovementController>();
            _shootingController = GetComponent<ShootingController>();
        }

        protected void Start()
        {
            _shootingController.SetWeapon(baseWeaponPrefab, hand);
        }

        protected void Update()
        {
            var direction = _movementDirectionSource.MovementDirection;
            var lookDirection = direction;
            if (_shootingController.HasTarget)
                lookDirection = (_shootingController.TargetPosition - transform.position).normalized;
            _characterMovementController.MovementDirection = direction;
            _characterMovementController.LookDirection = lookDirection;

            _characterMovementController.SetSprint(_sprintingSource.IsSprinting);
            
            if (health <= 0f)
                Destroy(gameObject);
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (LayerUtils.IsBullet(other.gameObject))
            {
                var otherGameObject = other.gameObject;
                var bullet = otherGameObject.GetComponent<Bullet>();
                
                health -= bullet.Damage;
                
                Destroy(otherGameObject);
            }
        }
    }
}
