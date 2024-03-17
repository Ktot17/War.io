using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace War.io.PickUp
{
    public class PickUpSpawner : BaseSpawner
    {
        [SerializeField] private PickUpItem pickUpPrefab;
        [SerializeField] private int maxCount = 2;

        private int _currentCount;

        protected void Update()
        {
            if (_currentCount < maxCount)
            {
                CurrentSpawnTimerSeconds += Time.deltaTime;

                if (CurrentSpawnTimerSeconds > CurrentSpawnIntervalSeconds)
                {
                    CurrentSpawnTimerSeconds = 0f;
                    CurrentSpawnIntervalSeconds = Random.Range(minSpawnIntervalSeconds, maxSpawnIntervalSeconds);
                    _currentCount++;

                    var randomPointInsideRange = Random.insideUnitCircle * range;
                    var randomPosition = new Vector3(randomPointInsideRange.x, 0f, randomPointInsideRange.y)
                                         + transform.position;

                    var pickUp = Instantiate(pickUpPrefab, randomPosition, Quaternion.identity, transform);
                    pickUp.OnPickedUp += OnItemPickedUp;
                }
            }
        }

        private void OnItemPickedUp(PickUpItem pickedUpItem)
        {
            _currentCount--;
            pickedUpItem.OnPickedUp -= OnItemPickedUp;
        }
        
        protected void OnDrawGizmos()
        {
            var cashedColor = Handles.color;
            Handles.color = Color.green;
            Handles.DrawWireDisc(transform.position, Vector3.up, range);
            Handles.color = cashedColor;
        }
    }
}