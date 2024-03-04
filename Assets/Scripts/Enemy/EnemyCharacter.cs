using UnityEngine;
using War.io.Movement;

namespace War.io.Enemy
{
    [RequireComponent(typeof(DummyDirectionController),
        typeof(DummySprintingController))]
    public class EnemyCharacter : BaseCharacter
    {
    }
}