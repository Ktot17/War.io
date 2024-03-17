using UnityEngine;

namespace War.io
{
    public abstract class BaseSpawner : MonoBehaviour
    {
        [SerializeField] protected float range = 2f;
        [SerializeField] protected float minSpawnIntervalSeconds = 2f;
        [SerializeField] protected float maxSpawnIntervalSeconds = 10f;
        
        protected float CurrentSpawnIntervalSeconds;
        protected float CurrentSpawnTimerSeconds;
        
        protected void Awake()
        {
            CurrentSpawnIntervalSeconds = Random.Range(minSpawnIntervalSeconds, maxSpawnIntervalSeconds);
        }
    }
}