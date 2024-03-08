using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace War.io.PickUp
{
    public class PickUpSpawner : MonoBehaviour
    {
        [SerializeField] private PickUpItem pickUpPrefab;
        [SerializeField] private float range = 2f;
        [SerializeField] private int maxCount = 2;
        [SerializeField] private float minSpawnIntervalSeconds = 2f;
        [SerializeField] private float maxSpawnIntervalSeconds = 10f;

        private float _currentSpawnIntervalSeconds;
        private float _currentSpawnTimerSeconds;
        private int _currentCount;

        protected void Awake()
        {
            _currentSpawnIntervalSeconds = Random.Range(minSpawnIntervalSeconds, maxSpawnIntervalSeconds);
        }

        protected void Update()
        {
            if (_currentCount < maxCount)
            {
                _currentSpawnTimerSeconds += Time.deltaTime;

                if (_currentSpawnTimerSeconds > _currentSpawnIntervalSeconds)
                {
                    _currentSpawnTimerSeconds = 0f;
                    _currentSpawnIntervalSeconds = Random.Range(minSpawnIntervalSeconds, maxSpawnIntervalSeconds);
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