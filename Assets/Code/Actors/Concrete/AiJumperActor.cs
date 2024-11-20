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
    public class AiJumperActor : BounceableActor
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private CollisionObserver _groundTriggerObserver;
        [SerializeField] private CollisionObserver _killTriggerObserver;
        [SerializeField] private float _jumpVelocity;
        [SerializeField] private float _jumpMaxDuration;
        [SerializeField] private float _jumpTimeInterval;
        [SerializeField] private float _initialWaitTime;
        
        private IJumpBehaviour _jumpBehaviour;
        private IGroundCheckBehaviour _groundCheckBehaviour;
        private IJumpBehaviourVisual _jumpBehaviourVisual;
        private IKillBehaviour _killBehaviour;
        
        protected override void InitBehaviours()
        {
            base.InitBehaviours();
            
            var tickService = ServiceLocator.Get<ITickService>();
            
            _jumpBehaviour = new JumpBehaviour(_rigidbody2D, this, _jumpVelocity, _jumpMaxDuration);
            TryAddBehaviour(_jumpBehaviour);
            
            _groundCheckBehaviour = new GroundCheckBehaviour();
            TryAddBehaviour(_groundCheckBehaviour);
            
            _killBehaviour = new KillBehaviour();
            TryAddBehaviour(_killBehaviour);
            
            _jumpBehaviourVisual = new JumpBehaviourVisual(_animator, _jumpBehaviour, tickService);

            _groundTriggerObserver.OnTriggerEnter += OnGroundTriggerEnter;
            _groundTriggerObserver.OnTriggerExit += OnGroundTriggerExit;

            _groundCheckBehaviour.OnGroundedStateChanged += OnGroundedStateChanged;

            _killTriggerObserver.OnTriggerEnter += OnKillTriggerEnter;
        }

        protected override void BindController()
        {
            var tickService = ServiceLocator.Get<ITickService>();
            
            _controller = new AiJumperActorController(this, tickService, _jumpTimeInterval, _initialWaitTime);
            _controller.SetEnabled(true);
        }

        private void OnGroundTriggerEnter(GameObject go)
            => _groundCheckBehaviour.OnNewContact(go);

        private void OnGroundTriggerExit(GameObject go)
            => _groundCheckBehaviour.OnLoseContact(go);

        private void OnGroundedStateChanged(bool grounded)
            => _jumpBehaviour.SetGrounded(grounded);

        private void OnKillTriggerEnter(GameObject go)
        {
            var actor = go.GetComponentInParent<BaseActor>();

            if (actor == null)
                return;

            if (!actor.TryGetBehaviour<IKillableBehaviour>(out var killableBehaviour))
                return;
            
            _killBehaviour.Kill(killableBehaviour);
        }

        protected override void DisposeBehaviours()
        {
            _jumpBehaviourVisual.Dispose();
            
            _groundTriggerObserver.OnTriggerEnter -= OnGroundTriggerEnter;
            _groundTriggerObserver.OnTriggerExit -= OnGroundTriggerExit;
            
            _groundCheckBehaviour.OnGroundedStateChanged += OnGroundedStateChanged;

            _killTriggerObserver.OnTriggerEnter -= OnKillTriggerEnter;
        }
    }
}