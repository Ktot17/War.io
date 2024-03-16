using UnityEngine;
using War.io.Enemy.States;

namespace War.io.Enemy
{
    public class EnemyAIController : MonoBehaviour
    {
        [SerializeField] private float viewRadius = 20f;
        
        private EnemyTarget _target;
        private EnemyStateMachine _stateMachine;
        
        protected void Awake()
        {
            var player = FindObjectOfType<PlayerCharacter>();
            
            var enemyDirectionController = GetComponent<EnemyDirectionController>();
            
            var navMesher = new NavMesher(transform);

            _target = new EnemyTarget(transform, player, viewRadius);
            
            _stateMachine = new EnemyStateMachine(enemyDirectionController, navMesher, _target);
        }

        protected void Update()
        {
            _target.FindClosest();
            _stateMachine.Update();
        }
    }
}