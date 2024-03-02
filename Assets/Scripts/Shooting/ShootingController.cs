using UnityEngine;

namespace War.io.Shooting
{
    public class ShootingController : MonoBehaviour
    {
        public bool HasTarget => _target != null;

        public Vector3 TargetPosition => _target.transform.position;
        
        private Weapon _weapon;
        
        private readonly Collider[] _colliders = new Collider[2];
        private float _nextShotTimerSec;
        private GameObject _target;

        protected void Update()
        {
            _target = GetTarget();
            
            _nextShotTimerSec -= Time.deltaTime;

            if (_nextShotTimerSec < 0)
            {
                if (HasTarget)
                    _weapon.Shoot(TargetPosition);

                _nextShotTimerSec = _weapon.ShootFrequencySec;
            }
        }

        public void SetWeapon(Weapon weaponPrefab, Transform hand)
        {
            _weapon = Instantiate(weaponPrefab, hand);
            var weaponTransform = _weapon.transform;
            weaponTransform.localPosition = Vector3.zero;
            weaponTransform.localRotation = Quaternion.identity;
        }

        private GameObject GetTarget()
        {
            GameObject target = null;

            var position = _weapon.transform.position;
            var radius = _weapon.ShootRadius;
            var mask = LayerUtils.CharacterMask;

            var size = Physics.OverlapSphereNonAlloc(position, radius, _colliders, mask);

            if (size > 0)
            {
                for (var i = 0; i < size; ++i)
                {
                    if (_colliders[i].gameObject != gameObject && _colliders[i].gameObject.layer != gameObject.layer)
                    {
                        target = _colliders[i].gameObject;
                        break;
                    }
                }
            }

            return target;
        }
    }
}