using Code.ActorControllers.Concrete;
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
    public class AiJumperActor : BounceableActor, IJumperActor
    {
        [SerializeField] protected Animator _animator;
        [SerializeField] private float _jumpVelocity;
        [SerializeField] private float _jumpMaxDuration;
        [SerializeField] private float _jumpTimeInterval;
        [SerializeField] private float _initialWaitTime;
        
        private IJumpBehaviour _jumpBehaviour;
        private IGroundCheckBehaviour _groundCheckBehaviour;
        private IJumpBehaviourVisual _jumpBehaviourVisual;
        
        protected override void InitBehaviours()
        {
            base.InitBehaviours();
            
            var tickService = ServiceLocator.Get<ITickService>();
            
            _jumpBehaviour = new JumpBehaviour(_rigidbody2D, this, _jumpVelocity, _jumpMaxDuration);
            _groundCheckBehaviour = new GroundCheckBehaviour();
            _jumpBehaviourVisual = new JumpBehaviourVisual(_animator, _jumpBehaviour, tickService);
        }
        
        protected override void BindController()
        {
            var tickService = ServiceLocator.Get<ITickService>();
            
            _controller = new AiJumperActorController(this, tickService, _jumpTimeInterval, _initialWaitTime);
            _controller.SetEnabled(true);
        }
        
        public void UpdateJump(bool jumping)
            => _jumpBehaviour.UpdateJump(jumping, _groundCheckBehaviour.IsGrounded);

        private void OnTriggerEnter2D(Collider2D other)
            => _groundCheckBehaviour.OnNewContact(other.gameObject);

        private void OnTriggerExit2D(Collider2D other)
            => _groundCheckBehaviour.OnLoseContact(other.gameObject);

        protected override void DisposeBehaviours()
        {
            _jumpBehaviourVisual.Dispose();
        }
    }
}