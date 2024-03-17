using System.Collections.Generic;
using War.io.FSM;

namespace War.io.Enemy.States
{
    public class EnemyStateMachine : BaseStateMachine
    {
        private const float NavMeshTurnOffDistance = 5f;

        public EnemyStateMachine(EnemyCharacter enemy, EnemyDirectionController enemyDirectionController, 
            EnemySprintingController enemySprintingController,
            NavMesher navMesher, EnemyTarget target)
        {
            var idleState = new IdleState(enemySprintingController);
            var findWayState = new FindWayState(target, navMesher, enemyDirectionController);
            var moveForwardState = new MoveForwardState(target, enemyDirectionController);
            var runAwayState = new RunAwayState(target, enemyDirectionController, 
                enemySprintingController);

            SetInitialState(idleState);

            AddState(state: idleState, transitions: new List<Transition>
            {
                new Transition(
                    findWayState,
                    () => target.DistanceToClosestFromAgent() > NavMeshTurnOffDistance),
                new Transition(
                    moveForwardState,
                    () => target.DistanceToClosestFromAgent() <= NavMeshTurnOffDistance),
                new Transition(
                    runAwayState,
                    () => enemy.GetHealthPercent() <= enemy.RunAwayHealthPercent && 
                          enemy.DecidesToRun && target.IsTargetPlayer())
            });
            
            AddState(state: findWayState, transitions: new List<Transition>
            {
                new Transition(
                    idleState,
                    () => target.Closest == null),
                new Transition(
                    moveForwardState,
                    () => target.DistanceToClosestFromAgent() <= NavMeshTurnOffDistance)
            });
            
            AddState(state: moveForwardState, transitions: new List<Transition>
            {
                new Transition(
                    idleState,
                    () => target.Closest == null),
                new Transition(
                    findWayState,
                    () => target.DistanceToClosestFromAgent() > NavMeshTurnOffDistance),
                new Transition(
                    runAwayState,
                    () => enemy.GetHealthPercent() <= enemy.RunAwayHealthPercent && 
                          enemy.DecidesToRun && target.IsTargetPlayer())
            });
            
            AddState(state: runAwayState, transitions: new List<Transition>
            {
                new Transition(
                    idleState,
                    () => !target.IsTargetPlayer())
            });
        }
    }
}