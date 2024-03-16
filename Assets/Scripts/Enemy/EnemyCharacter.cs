using UnityEngine;
using War.io.Movement;

namespace War.io.Enemy
{
    [RequireComponent(typeof(EnemyDirectionController),
        typeof(DummySprintingController),
        typeof(EnemyAIController))]
    public class EnemyCharacter : BaseCharacter
    {
    }
}