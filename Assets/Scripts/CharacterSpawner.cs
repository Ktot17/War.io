using UnityEditor;
using UnityEngine;
using War.io.Enemy;

namespace War.io
{
    public class CharacterSpawner : BaseSpawner
    {
        [SerializeField] private EnemyCharacter enemyCharacterPrefab;
        [SerializeField] private PlayerCharacter playerCharacterPrefab;

        private static bool _isPlayerAlive;
        
        protected void Update()
        {
            CurrentSpawnTimerSeconds += Time.deltaTime;

            if (CurrentSpawnTimerSeconds > CurrentSpawnIntervalSeconds)
            {
                CurrentSpawnTimerSeconds = 0f;
                CurrentSpawnIntervalSeconds = Random.Range(minSpawnIntervalSeconds, maxSpawnIntervalSeconds);

                var randomPointInsideRange = Random.insideUnitCircle * range;
                var randomPosition = new Vector3(randomPointInsideRange.x, 0f, randomPointInsideRange.y)
                                     + transform.position;

                if (_isPlayerAlive || Random.Range(0, 1) == 1)
                    Instantiate(enemyCharacterPrefab, randomPosition, Quaternion.identity, transform);
                else
                {
                    var player = Instantiate(playerCharacterPrefab, randomPosition, Quaternion.identity, transform);
                    _isPlayerAlive = true;
                    player.OnDeath += OnPlayerDeath;
                }
            }
        }

        private void OnPlayerDeath(PlayerCharacter player)
        {
            _isPlayerAlive = false;
            player.OnDeath -= OnPlayerDeath;
        }
        
        protected void OnDrawGizmos()
        {
            var cashedColor = Handles.color;
            Handles.color = Color.blue;
            Handles.DrawWireDisc(transform.position, Vector3.up, range);
            Handles.color = cashedColor;
        }
    }
}