using UnityEngine;
using War.io.Movement;

namespace War.io.Enemy
{
    public class EnemySprintingController : MonoBehaviour, ISprintingSource
    {
        public bool IsSprinting { get; set; }
    }
}