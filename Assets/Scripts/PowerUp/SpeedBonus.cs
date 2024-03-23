using UnityEngine;

namespace War.io.PowerUp
{
    public class SpeedBonus : Bonus
    {
        [SerializeField] private float durationSeconds = 10f;
        [SerializeField] private float coefficient = 2f;
        
        public override void ActivateBonus(BaseCharacter character)
        {
            if (Character)
            {
                CancelInvoke(nameof(DeactivateBonus));
                DeactivateBonus();
            }
            base.ActivateBonus(character);
            character.MultiplySpeed(coefficient);
            Invoke(nameof(DeactivateBonus), durationSeconds);
        }

        protected override void DeactivateBonus()
        {
            if (Character)
                Character.ResetSpeed();
            base.DeactivateBonus();
        }
    }
}