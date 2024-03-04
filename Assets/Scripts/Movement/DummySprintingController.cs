using UnityEngine;

namespace War.io.Movement
{
    public class DummySprintingController : MonoBehaviour, ISprintingSource
    {
        public bool IsSprinting { get; private set; }
    }
}