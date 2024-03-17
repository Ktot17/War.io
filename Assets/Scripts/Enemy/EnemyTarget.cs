using UnityEngine;

namespace War.io.Enemy
{
    public class EnemyTarget
    {
        public GameObject Closest { get; private set; }

        private readonly float _viewRadius;
        private readonly EnemyCharacter _agent;
        public PlayerCharacter Player { get; set; }
        
        private readonly Collider[] _colliders = new Collider[10];

        public EnemyTarget(EnemyCharacter agent, PlayerCharacter player, float viewRadius)
        {
            _agent = agent;
            Player = player;
            _viewRadius = viewRadius;
        }

        public void FindClosest()
        {
            var minDistance = float.MaxValue;

            var count = FindAllTargets(LayerUtils.PickUpsMask | LayerUtils.PlayerMask);

            for (var i = 0; i < count; i++)
            {
                var go = _colliders[i].gameObject;
                
                if (go == _agent.gameObject) continue;
                
                if (LayerUtils.IsWeaponPickUp(go) && !_agent.HasBaseWeapon()) continue;

                var distance = DistanceFromAgentTo(go);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    Closest = go;
                }
            }

            if (Player != null && DistanceFromAgentTo(Player.gameObject) < minDistance)
                Closest = Player.gameObject;
        }

        public float DistanceToClosestFromAgent()
        {
            if (Closest)
                DistanceFromAgentTo(Closest);
            
            return 0;
        }

        private int FindAllTargets(int layerMask)
        {
            var size = Physics.OverlapSphereNonAlloc(
                _agent.transform.position,
                _viewRadius,
                _colliders,
                layerMask);

            return size;
        }

        private float DistanceFromAgentTo(GameObject go) =>
            (_agent.transform.position - go.transform.position).magnitude;

        public bool IsTargetPlayer()
        {
            if (Player)
                return Player.gameObject == Closest;
            return false;
        }
    }
}