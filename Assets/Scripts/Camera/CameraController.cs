using System;
using UnityEngine;

namespace War.io.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Vector3 followCameraOffset = Vector3.zero;
        [SerializeField] private Vector3 rotationOffset = Vector3.zero;
        [SerializeField] private PlayerCharacter playerCharacter;

        protected void Awake()
        {
            if (playerCharacter == null)
                throw new NullReferenceException(
                    $"Follow camera can't follow null player - {nameof(playerCharacter)}");
        }

        protected void LateUpdate()
        {
            var targetRotation = rotationOffset - followCameraOffset;

            transform.position = playerCharacter.transform.position + followCameraOffset;
            transform.rotation = Quaternion.LookRotation(targetRotation, Vector3.up);
        }
    }
}