using UnityEngine;
using Random = UnityEngine.Random;

namespace War.io.Enemy
{
    [RequireComponent(typeof(EnemyDirectionController),
        typeof(EnemySprintingController),
        typeof(EnemyAIController))]
    public class EnemyCharacter : BaseCharacter
    {
        [field: SerializeField]
        public float RunAwayHealthPercent { get; private set; } = 30f;
        [field: SerializeField]
        public float RunAwayPercent { get; private set; } = 70f;

        public bool DecidesToRun() => Random.Range(0, 100) <= RunAwayPercent;
    }
}