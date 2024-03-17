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
            if (_weapon)
                Destroy(_weapon.gameObject);
            
            _weapon = Instantiate(weaponPrefab, hand);
            var weaponTransform = _weapon.transform;
            weaponTransform.localPosition = Vector3.zero;
            weaponTransform.localRotation = Quaternion.identity;
        }

        private GameObject GetTarget()
        {
            var minDistance = float.MaxValue;
            
            GameObject target = null;

            var position = _weapon.transform.position;
            var radius = _weapon.ShootRadius;
            var mask = LayerUtils.CharacterMask;

            var size = Physics.OverlapSphereNonAlloc(position, radius, _colliders, mask);

            if (size > 1)
            {
                for (var i = 0; i < size; ++i)
                {
                    var go = _colliders[i].gameObject;
                    var distance = (transform.position - go.transform.position).magnitude;
                    if (go.layer != gameObject.layer && distance < minDistance)
                    {
                        target = go;
                        break;
                    }
                }
            }

            return target;
        }
    }
}