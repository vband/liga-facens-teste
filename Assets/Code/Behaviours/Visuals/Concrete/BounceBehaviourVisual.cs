using Code.Behaviours.Abstraction;
using Code.Behaviours.Visuals.Abstraction;
using UnityEngine;

namespace Code.Behaviours.Visuals.Concrete
{
    public class BounceBehaviourVisual : IBounceBehaviourVisual
    {
        private static readonly int _bounceAnimation = Animator.StringToHash("Bounce");

        private readonly Animator _animator;
        private readonly IBounceBehaviour _bounceBehaviour;
        
        public BounceBehaviourVisual(Animator animator, IBounceBehaviour bounceBehaviour)
        {
            _animator = animator;
            _bounceBehaviour = bounceBehaviour;
            
            _bounceBehaviour.OnBounce += OnBounce;
        }

        private void OnBounce()
        {
            _animator.Play(_bounceAnimation);
        }

        public void Dispose()
        {
            _bounceBehaviour.OnBounce -= OnBounce;
        }
    }
}