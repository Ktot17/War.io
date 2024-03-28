using UnityEngine;

namespace War.io
{
    public class Death : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            Destroy(animator.gameObject, stateInfo.length);
        }
    }
}