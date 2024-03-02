using UnityEngine;

namespace War.io.Movement
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMovementController : MonoBehaviour
    {
        private static readonly float SqrEpsilon = Mathf.Epsilon * Mathf.Epsilon;
        
        [SerializeField] private float speed = 5f;
        [SerializeField] private float speedCoefficient = 3f;
        [SerializeField] private float maxRadiansDelta = 10f;
        private float _currentSpeed;
        
        public Vector3 Direction { get; set; }
        public bool Sprint
        {
            set
            {
                if (value)
                    _currentSpeed = speed + speedCoefficient;
                else
                    _currentSpeed = speed;
            }
        }

        private CharacterController _characterController;

        protected void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        protected void Update()
        {
            Translate();
            
            if (maxRadiansDelta > 0f && Direction != Vector3.zero)
                Rotate();
        }

        private void Translate()
        {
            var delta = Direction * (_currentSpeed * Time.deltaTime);
            _characterController.Move(delta);
        }

        private void Rotate()
        {
            var currentLookDirection = transform.rotation * Vector3.forward;
            var sqrMagnitude = (currentLookDirection - Direction).sqrMagnitude;

            if (!(sqrMagnitude > SqrEpsilon)) return;
            var newRotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(Direction, Vector3.up),
                maxRadiansDelta * Time.deltaTime);

            transform.rotation = newRotation;
        }
    }
}