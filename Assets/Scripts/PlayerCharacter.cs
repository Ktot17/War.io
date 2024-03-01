using UnityEngine;

namespace War.io
{
    [RequireComponent(typeof(CharacterMovementController))]
    [RequireComponent(typeof(PlayerMovementDirectionController))]
    public class PlayerCharacter : MonoBehaviour
    {
        private CharacterMovementController _characterMovementController;
        private IMovementDirectionSource _movementDirectionSource;
    
        protected void Awake()
        {
            _characterMovementController = GetComponent<CharacterMovementController>();
            _movementDirectionSource = GetComponent<IMovementDirectionSource>();
        }

        protected void Update()
        {
            var direction = _movementDirectionSource.MovementDirection;
            _characterMovementController.Direction = direction;

            _characterMovementController.Sprint = Input.GetKey(KeyCode.Space);
        }
    }
}
