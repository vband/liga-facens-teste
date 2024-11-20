using Code.ActorControllers.Concrete;
using Code.Actors.Abstraction;
using Code.Behaviours.Abstraction;
using Code.Behaviours.Concrete;
using Code.Behaviours.Visuals.Abstraction;
using Code.Behaviours.Visuals.Concrete;
using Code.Services.Abstraction;
using Code.Services.ServiceLocator;
using Code.Utils;
using UnityEngine;

namespace Code.Actors.Concrete
{
    public class PlayerActor : KillableActor
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private CollisionObserver _groundTriggerObserver;
        [SerializeField] private float _horizontalSpeed;
        [SerializeField] private float _jumpVelocity;
        [SerializeField] private float _jumpMaxDuration;
        
        private IRunBehaviour _runBehaviour;
        private IRunBehaviourVisual _runBehaviourVisual;
        private IJumpBehaviour _jumpBehaviour;
        private IGroundCheckBehaviour _groundCheckBehaviour;
        private IJumpBehaviourVisual _jumpBehaviourVisual;
        
        protected override void InitBehaviours()
        {
            base.InitBehaviours();
            
            var tickService = ServiceLocator.Get<ITickService>();
            
            _runBehaviour = new RunBehaviour(_rigidbody2D, _horizontalSpeed);
            TryAddBehaviour(_runBehaviour);
            
            _jumpBehaviour = new JumpBehaviour(_rigidbody2D, this, _jumpVelocity, _jumpMaxDuration);
            TryAddBehaviour(_jumpBehaviour);
            
            _groundCheckBehaviour = new GroundCheckBehaviour();
            TryAddBehaviour(_groundCheckBehaviour);
            
            _runBehaviourVisual = new RunBehaviourVisual(_animator, _spriteRenderer, _runBehaviour, tickService);
            _jumpBehaviourVisual = new JumpBehaviourVisual(_animator, _jumpBehaviour, tickService);

            _groundTriggerObserver.OnTriggerEnter += OnGroundTriggerEnter;
            _groundTriggerObserver.OnTriggerExit += OnGroundTriggerExit;

            _groundCheckBehaviour.OnGroundedStateChanged += OnGroundedStateChanged;
        }

        protected override void BindController()
        {
            var tickService = ServiceLocator.Get<ITickService>();
            
            _controller = new InputActorController(this, tickService);
            _controller.SetEnabled(true);
        }

        private void OnGroundTriggerEnter(GameObject go)
            => _groundCheckBehaviour.OnNewContact(go);

        private void OnGroundTriggerExit(GameObject go)
            => _groundCheckBehaviour.OnLoseContact(go);

        private void OnGroundedStateChanged(bool grounded)
            => _jumpBehaviour.SetGrounded(grounded);

        protected override void DisposeBehaviours()
        {
            base.DisposeBehaviours();
            
            _runBehaviourVisual.Dispose();
            _jumpBehaviourVisual.Dispose();
            
            _groundTriggerObserver.OnTriggerEnter -= OnGroundTriggerEnter;
            _groundTriggerObserver.OnTriggerExit -= OnGroundTriggerExit;
            
            _groundCheckBehaviour.OnGroundedStateChanged += OnGroundedStateChanged;
        }
    }
}