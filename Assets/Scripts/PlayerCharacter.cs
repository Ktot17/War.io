using UnityEngine;
using War.io.Movement;

namespace War.io
{
    [RequireComponent(typeof(PlayerMovementDirectionController),
        typeof(PlayerSprintingController))]
    public class PlayerCharacter : BaseCharacter
    {
    }
}
