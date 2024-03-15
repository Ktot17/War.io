using UnityEngine;
using War.io.Movement;
using War.io.PickUp;
using War.io.PowerUp;
using War.io.Shooting;

namespace War.io
{
    [RequireComponent(typeof(CharacterMovementController),
        typeof(ShootingController))]
    public abstract class BaseCharacter : MonoBehaviour
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
            SetWeapon(baseWeaponPrefab);
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
            var otherGameObject = other.gameObject;
            if (LayerUtils.IsBullet(otherGameObject))
            {
                var bullet = otherGameObject.GetComponent<Bullet>();
                
                health -= bullet.Damage;
                
                Destroy(otherGameObject);
            }
            else if (LayerUtils.IsPickUp(otherGameObject))
            {
                var pickUp = otherGameObject.GetComponent<PickUpItem>();
                pickUp.PickUp(this);
                
                Destroy(otherGameObject);
            }
        }

        public void SetWeapon(Weapon weapon)
        {
            _shootingController.SetWeapon(weapon, hand);
        }

        public void MultiplySpeed(float bonus)
        {
            _characterMovementController.MultiplySpeed(bonus);
        }

        public void ResetSpeed()
        {
            _characterMovementController.ResetSpeed();
        }

        public void SetBonus(Bonus bonus)
        {
            bonus.ActivateBonus(this);
        }
    }
}
