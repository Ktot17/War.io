using UnityEngine;

namespace War.io.PowerUp
{
    public class SpeedBonus : Bonus
    {
        [SerializeField] private float durationSeconds = 10f;
        [SerializeField] private float coefficient = 2f;
        
        public override void ActivateBonus(BaseCharacter character)
        {
            base.ActivateBonus(character);
            character.BonusSpeed(coefficient);
            CancelInvoke(nameof(DeactivateBonus));
            Invoke(nameof(DeactivateBonus), durationSeconds);
        }

        protected override void DeactivateBonus()
        {
            Character.BonusSpeed(1f);
            base.DeactivateBonus();
        }
    }
}