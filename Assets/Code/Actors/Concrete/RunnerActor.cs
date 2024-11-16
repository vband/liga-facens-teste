using Code.Actors.Abstraction;
using Code.Behaviours.Abstraction;
using Code.Behaviours.Concrete;
using Code.Behaviours.Visuals.Abstraction;
using Code.Behaviours.Visuals.Concrete;
using Code.Services.Abstraction;
using Code.Services.ServiceLocator;
using UnityEngine;

namespace Code.Actors.Concrete
{
    public abstract class RunnerActor : Rigidbody2DActor, IRunnerActor
    {
        public float HorizontalPos => transform.position.x;

        [SerializeField] protected Animator _animator;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private float _horizontalSpeed;
        
        private IRunBehaviour _runBehaviour;
        private IRunBehaviourVisual _runBehaviourVisual;
        
        protected override void InitBehaviours()
        {
            var tickService = ServiceLocator.Get<ITickService>();
            
            _runBehaviour = new Rigidbody2DRunBehaviour(_rigidbody2D, _horizontalSpeed);
            _runBehaviourVisual = new RunBehaviourVisual(_animator, _spriteRenderer, _runBehaviour, tickService);
        }

        public void UpdateMovement(float axis)
            => _runBehaviour.UpdateMovement(axis);

        public void SnapHorizontalPos(float targetHorizontalPos)
            => transform.position = new Vector3(targetHorizontalPos, transform.position.y, transform.position.z);

        protected override void DisposeBehaviours()
        {
            _runBehaviourVisual.Dispose();
        }
    }
}