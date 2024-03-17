using UnityEngine;

namespace War.io.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Vector3 followCameraOffset = Vector3.zero;
        [SerializeField] private Vector3 rotationOffset = Vector3.zero;
        [SerializeField] private PlayerCharacter playerCharacter;

        protected void LateUpdate()
        {
            if (playerCharacter)
            {
                var targetRotation = rotationOffset - followCameraOffset;

                transform.position = playerCharacter.transform.position + followCameraOffset;
                transform.rotation = Quaternion.LookRotation(targetRotation, Vector3.up);
            }
            else
            {
                playerCharacter = FindObjectOfType<PlayerCharacter>();
            }
        }
    }
}