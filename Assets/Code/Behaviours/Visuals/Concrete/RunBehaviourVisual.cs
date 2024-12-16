using Code.Behaviours.Abstraction;
using Code.Behaviours.Visuals.Abstraction;
using Code.Services.Abstraction;
using UnityEngine;

namespace Code.Behaviours.Visuals.Concrete
{
    public class RunBehaviourVisual : IRunBehaviourVisual
    {
        private static readonly int _runAnimationTrigger = Animator.StringToHash("Running");

        private readonly Animator _animator;
        private readonly SpriteRenderer _spriteRenderer;
        private readonly IRunBehaviour _runBehaviour;
        private readonly ITickService _tickService;

        public RunBehaviourVisual(Animator animator, SpriteRenderer spriteRenderer, IRunBehaviour runBehaviour,
            ITickService tickService)
        {
            _animator = animator;
            _spriteRenderer = spriteRenderer;
            _runBehaviour = runBehaviour;
            _tickService = tickService;

            _tickService.OnTick += OnTick;
        }

        private void OnTick()
        {
            if (_runBehaviour.HorizontalVelocity != 0)
                _animator.SetTrigger(_runAnimationTrigger);
            else
                _animator.ResetTrigger(_runAnimationTrigger);

            if (_runBehaviour.HorizontalVelocity != 0)
                _spriteRenderer.flipX = _runBehaviour.HorizontalVelocity < 0;
        }

        public void Dispose()
        {
            _tickService.OnTick -= OnTick;
        }
    }
}
