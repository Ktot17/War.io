using UnityEngine;

namespace War.io.PowerUp
{
    public enum BonusType
    {
        Speed
    }
    public class Bonus : MonoBehaviour
    {
        [SerializeField] private float durationSeconds = 10f;
        [field: SerializeField]
        public float Coefficient { get; private set; } = 2f;

        [field: SerializeField]
        public BonusType Type { get; private set; } = BonusType.Speed;
        
        public BaseCharacter Character { get; set; }

        private float _currentDurationSeconds;

        protected void Update()
        {
            if (Character)
            {
                _currentDurationSeconds += Time.deltaTime;

                if (_currentDurationSeconds > durationSeconds)
                {
                    _currentDurationSeconds = 0f;
                    Character.SetBonus(this, false);
                    Destroy(gameObject);
                    Character = null;
                }
            }
        }
    }
}