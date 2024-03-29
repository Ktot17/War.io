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
        [SerializeField] private Animator animator;
        [SerializeField] private GameObject deathAnim;
        
        private IMovementDirectionSource _movementDirectionSource;
        private ISprintingSource _sprintingSource;
        
        private CharacterMovementController _characterMovementController;
        private ShootingController _shootingController;

        private float _currentHealth;
        private Weapon _currentWeapon;
    
        protected void Awake()
        {
            _movementDirectionSource = GetComponent<IMovementDirectionSource>();
            _sprintingSource = GetComponent<ISprintingSource>();
            
            _characterMovementController = GetComponent<CharacterMovementController>();
            _shootingController = GetComponent<ShootingController>();

            _currentHealth = health;
            _currentWeapon = baseWeaponPrefab;
        }

        protected void Start()
        {
            SetWeapon(_currentWeapon);
        }

        protected void Update()
        {
            var direction = _movementDirectionSource.MovementDirection.normalized;
            var lookDirection = direction;
            if (_shootingController.HasTarget)
                lookDirection = (_shootingController.TargetPosition - transform.position).normalized;
            _characterMovementController.MovementDirection = direction;
            _characterMovementController.LookDirection = lookDirection;

            _characterMovementController.SetSprint(_sprintingSource.IsSprinting);
            
            animator.SetBool("IsSprinting", _sprintingSource.IsSprinting);
            animator.SetBool("IsMoving", direction != Vector3.zero);
            animator.SetBool("IsShooting", _shootingController.HasTarget);
            animator.SetBool("HasBaseWeapon", HasBaseWeapon());
            animator.SetBool("IsBackwards", 
                Mathf.Abs(Mathf.Sign(direction.x) - Mathf.Sign(lookDirection.x)) > Mathf.Epsilon ||
                Mathf.Abs(Mathf.Sign(direction.z) - Mathf.Sign(lookDirection.z)) > Mathf.Epsilon);

            if (_currentHealth <= 0f)
            {
                Instantiate(deathAnim, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        protected void OnTriggerEnter(Collider other)
        {
            var otherGameObject = other.gameObject;
            if (LayerUtils.IsBullet(otherGameObject))
            {
                animator.SetTrigger("Hit");
                var bullet = otherGameObject.GetComponent<Bullet>();
                
                _currentHealth -= bullet.Damage;
                
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
            _currentWeapon = weapon;
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

        public float GetHealthPercent()
        {
            return _currentHealth / health * 100f;
        }

        public bool HasBaseWeapon()
        {
            return _currentWeapon == baseWeaponPrefab;
        }
    }
}
