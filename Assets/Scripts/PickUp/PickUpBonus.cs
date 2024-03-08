using UnityEngine;
using War.io.PowerUp;

namespace War.io.PickUp
{
    public class PickUpBonus : PickUpItem
    {
        [SerializeField] private Bonus bonusPrefab;

        public override void PickUp(BaseCharacter character)
        {
            base.PickUp(character);
            character.SetBonus(bonusPrefab, true);
        }
    }
}