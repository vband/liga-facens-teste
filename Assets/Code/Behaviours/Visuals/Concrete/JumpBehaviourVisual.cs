using Code.Behaviours.Abstraction;
using Code.Behaviours.Visuals.Abstraction;
using Code.Services.Abstraction;
using UnityEngine;

namespace Code.Behaviours.Visuals.Concrete
{
    public class JumpBehaviourVisual : IJumpBehaviourVisual
    {
        private static readonly int _jumpAnimationTrigger = Animator.StringToHash("Jumping");
        private static readonly int _midairAnimationTrigger = Animator.StringToHash("Midair");

        private readonly Animator _animator;
        private readonly IJumpBehaviour _jumpBehaviour;
        private readonly ITickService _tickService;

        public JumpBehaviourVisual(Animator animator, IJumpBehaviour jumpBehaviour, ITickService tickService)
        {
            _animator = animator;
            _jumpBehaviour = jumpBehaviour;
            _tickService = tickService;
            
            _tickService.OnTick += OnTick;
        }

        private void OnTick()
        {
            if (_jumpBehaviour.VerticalVelocity != 0)
                _animator.SetTrigger(_midairAnimationTrigger);
            else
                _animator.ResetTrigger(_midairAnimationTrigger);
                
            if (_jumpBehaviour.VerticalVelocity > 0)
                _animator.SetTrigger(_jumpAnimationTrigger);
            else
                _animator.ResetTrigger(_jumpAnimationTrigger);
        }

        public void Dispose()
        {
            _tickService.OnTick -= OnTick;
        }
    }
}