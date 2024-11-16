using System.Collections;
using Code.Behaviours.Abstraction;
using Code.Behaviours.Visuals.Abstraction;
using UnityEngine;

namespace Code.Behaviours.Visuals.Concrete
{
    public class RunBehaviourVisual : IRunBehaviourVisual
    {
        private static readonly int _runAnimationTrigger = Animator.StringToHash("Running");

        public RunBehaviourVisual(Animator animator, SpriteRenderer spriteRenderer, IRunBehaviour runBehaviour,
            MonoBehaviour coroutineStarter)
        {
            coroutineStarter.StartCoroutine(UpdateVisualCoroutine(animator, spriteRenderer, runBehaviour));
        }

        private static IEnumerator UpdateVisualCoroutine(Animator animator, SpriteRenderer spriteRenderer,
            IRunBehaviour runBehaviour)
        {
            var waitForEndOfFrame = new WaitForEndOfFrame();
            
            while (true)
            {
                if (runBehaviour.HorizontalVelocity != 0)
                    animator.SetTrigger(_runAnimationTrigger);
                else
                    animator.ResetTrigger(_runAnimationTrigger);

                if (runBehaviour.HorizontalVelocity != 0)
                    spriteRenderer.flipX = runBehaviour.HorizontalVelocity < 0;

                yield return waitForEndOfFrame;
            }
        }
    }
}
