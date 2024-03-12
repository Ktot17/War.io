using UnityEngine;
using UnityEngine.Serialization;

namespace War.io.Movement
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMovementController : MonoBehaviour
    {
        private static readonly float SqrEpsilon = Mathf.Epsilon * Mathf.Epsilon;
        
        [SerializeField] private float speed = 5f;
        [FormerlySerializedAs("speedCoefficient")] [SerializeField] private float sprintCoefficient = 3f;
        [SerializeField] private float maxRadiansDelta = 10f;
        private float _bonusSpeed;
        private float _currentSpeed;
        
        public Vector3 LookDirection { get; set; }
        
        public Vector3 MovementDirection { get; set; }

        private CharacterController _characterController;

        protected void Awake()
        {
            _characterController = GetComponent<CharacterController>();

            _bonusSpeed = speed;
        }

        protected void Update()
        {
            Translate();
            
            if (maxRadiansDelta > 0f && LookDirection != Vector3.zero)
                Rotate();
        }

        private void Translate()
        {
            var delta = MovementDirection * (_currentSpeed * Time.deltaTime);
            _characterController.Move(delta);
        }

        private void Rotate()
        {
            var currentLookDirection = transform.rotation * Vector3.forward;
            var sqrMagnitude = (currentLookDirection - LookDirection).sqrMagnitude;

            if (sqrMagnitude <= SqrEpsilon) return;
            var newRotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(LookDirection, Vector3.up),
                maxRadiansDelta * Time.deltaTime);

            transform.rotation = newRotation;
        }

        public void SetSprint(bool isSprinting)
        {
            if (isSprinting)
                _currentSpeed = _bonusSpeed * sprintCoefficient;
            else
                _currentSpeed = _bonusSpeed;
        }

        public void MultiplySpeed(float bonus)
        {
            _bonusSpeed *= bonus;
        }

        public void ResetSpeed()
        {
            _bonusSpeed = speed;
        }
    }
}