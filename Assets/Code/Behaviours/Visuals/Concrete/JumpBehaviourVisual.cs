using System.Collections;
using Code.Behaviours.Abstraction;
using Code.Behaviours.Visuals.Abstraction;
using UnityEngine;

namespace Code.Behaviours.Visuals.Concrete
{
    public class JumpBehaviourVisual : IJumpBehaviourVisual
    {
        private static readonly int _jumpAnimationTrigger = Animator.StringToHash("Jumping");
        private static readonly int _midairAnimationTrigger = Animator.StringToHash("Midair");

        public JumpBehaviourVisual(Animator animator, IJumpBehaviour jumpBehaviour, MonoBehaviour coroutineStarter)
        {
            coroutineStarter.StartCoroutine(UpdateVisualCoroutine(animator, jumpBehaviour));
        }

        private static IEnumerator UpdateVisualCoroutine(Animator animator, IJumpBehaviour jumpBehaviour)
        {
            var waitForEndOfFrame = new WaitForEndOfFrame();

            while (true)
            {
                if (jumpBehaviour.VerticalVelocity != 0)
                    animator.SetTrigger(_midairAnimationTrigger);
                else
                    animator.ResetTrigger(_midairAnimationTrigger);
                
                if (jumpBehaviour.VerticalVelocity > 0)
                    animator.SetTrigger(_jumpAnimationTrigger);
                else
                    animator.ResetTrigger(_jumpAnimationTrigger);

                yield return waitForEndOfFrame;
            }
        }
    }
}