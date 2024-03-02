using UnityEngine;
using War.io.Movement;
using War.io.Shooting;

namespace War.io
{
    [RequireComponent(typeof(CharacterMovementController), 
        typeof(PlayerMovementDirectionController),
        typeof(ShootingController))]
    public class PlayerCharacter : MonoBehaviour
    {
        [SerializeField] private Weapon baseWeaponPrefab;
        [SerializeField] private Transform hand;
        
        private IMovementDirectionSource _movementDirectionSource;
        
        private CharacterMovementController _characterMovementController;
        private ShootingController _shootingController;
    
        protected void Awake()
        {
            _movementDirectionSource = GetComponent<IMovementDirectionSource>();
            
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
            _characterMovementController.Direction = direction;

            _characterMovementController.Sprint = Input.GetKey(KeyCode.Space);
        }
    }
}
