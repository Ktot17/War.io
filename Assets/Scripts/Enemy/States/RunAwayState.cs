﻿using UnityEngine;
using War.io.FSM;

namespace War.io.Enemy.States
{
    public class RunAwayState : BaseState
    {
        private readonly EnemyTarget _target;
        private readonly EnemyDirectionController _enemyDirectionController;
        private readonly EnemySprintingController _enemySprintingController;

        private Vector3 _currentPoint;
        
        public RunAwayState(EnemyTarget target, EnemyDirectionController enemyDirectionController,
            EnemySprintingController enemySprintingController)
        {
            _target = target;
            _enemyDirectionController = enemyDirectionController;
            _enemySprintingController = enemySprintingController;
        }
        
        public override void Execute()
        {
            var targetPosition = _target.Closest.transform.position;
            targetPosition.x = -targetPosition.x;
            targetPosition.z = -targetPosition.z;

            if (_currentPoint != targetPosition)
            {
                _currentPoint = targetPosition;
                _enemyDirectionController.UpdateMovementDirection(targetPosition);
                _enemySprintingController.IsSprinting = true;
            }
        }
    }
}