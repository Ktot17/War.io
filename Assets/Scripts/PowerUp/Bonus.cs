using UnityEngine;

namespace War.io.PowerUp
{
    public abstract class Bonus : MonoBehaviour
    {
        protected BaseCharacter Character;
        
        public virtual void ActivateBonus(BaseCharacter character)
        {
            Character = character;
        }

        protected virtual void DeactivateBonus()
        {
            
        }
    }
}