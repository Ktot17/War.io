using UnityEngine;
using War.io.Shooting;

namespace War.io.PickUp
{
    public class PickUpWeapon : PickUpItem
    {
        [SerializeField] private Weapon weaponPrefab;

        public override void PickUp(BaseCharacter character)
        {
            base.PickUp(character);
            character.SetWeapon(weaponPrefab);
        }
    }
}