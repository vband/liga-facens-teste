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
    public class PlayerActor : BounceableActor, IRunnerJumperActor
    {
        public float HorizontalPos => transform.position.x;

        [SerializeField] protected Animator _animator;
        [SerializeField] private SpriteRenderer _spriteRenderer;
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
            _runBehaviourVisual = new RunBehaviourVisual(_animator, _spriteRenderer, _runBehaviour, tickService);
            _jumpBehaviour = new JumpBehaviour(_rigidbody2D, this, _jumpVelocity, _jumpMaxDuration);
            _groundCheckBehaviour = new GroundCheckBehaviour();
            _jumpBehaviourVisual = new JumpBehaviourVisual(_animator, _jumpBehaviour, tickService);
        }

        protected override void BindController()
        {
            var tickService = ServiceLocator.Get<ITickService>();
            
            _controller = new InputActorController(this, tickService);
            _controller.SetEnabled(true);
        }

        public void UpdateMovement(float axis)
            => _runBehaviour.UpdateMovement(axis);

        public void SnapHorizontalPos(float targetHorizontalPos)
            => transform.position = new Vector3(targetHorizontalPos, transform.position.y, transform.position.z);

        public void UpdateJump(bool jumping)
            => _jumpBehaviour.UpdateJump(jumping, _groundCheckBehaviour.IsGrounded);

        private void OnTriggerEnter2D(Collider2D other)
            => _groundCheckBehaviour.OnNewContact(other.gameObject);

        private void OnTriggerExit2D(Collider2D other)
            => _groundCheckBehaviour.OnLoseContact(other.gameObject);

        protected override void DisposeBehaviours()
        {
            _runBehaviourVisual.Dispose();
            _jumpBehaviourVisual.Dispose();
        }
    }
}