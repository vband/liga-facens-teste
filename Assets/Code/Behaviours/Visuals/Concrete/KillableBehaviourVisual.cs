using Code.Behaviours.Abstraction;
using Code.Behaviours.Visuals.Abstraction;
using UnityEngine;

namespace Code.Behaviours.Visuals.Concrete
{
    public class KillableBehaviourVisual : IKillableBehaviourVisual
    {
        private static readonly int _dieAnimation = Animator.StringToHash("Die");

        private readonly Animator _animator;
        private readonly IKillableBehaviour _killableBehaviour;
        
        public KillableBehaviourVisual(Animator animator, IKillableBehaviour killableBehaviour)
        {
            _animator = animator;
            _killableBehaviour = killableBehaviour;
            
            _killableBehaviour.OnDied += OnDied;
        }

        private void OnDied()
            => _animator.Play(_dieAnimation);

        public void Dispose()
        {
            _killableBehaviour.OnDied -= OnDied;
        }
    }
}