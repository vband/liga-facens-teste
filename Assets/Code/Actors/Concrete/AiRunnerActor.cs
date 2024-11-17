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
    public class AiRunnerActor : BounceableActor, IRunnerActor
    {
        public float HorizontalPos => transform.position.x;
        
        [SerializeField] protected Animator _animator;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private CollisionObserver _killTriggerObserver;
        [SerializeField] private float _horizontalSpeed;
        [SerializeField] private float _movementWidth;
        [SerializeField] private bool _flipInitialDirection;
        
        private IRunBehaviour _runBehaviour;
        private IRunBehaviourVisual _runBehaviourVisual;
        private IKillBehaviour _killBehaviour;

        protected override void InitBehaviours()
        {
            base.InitBehaviours();
            
            var tickService = ServiceLocator.Get<ITickService>();
            
            _runBehaviour = new RunBehaviour(_rigidbody2D, _horizontalSpeed);
            _runBehaviourVisual = new RunBehaviourVisual(_animator, _spriteRenderer, _runBehaviour, tickService);
            _killBehaviour = new KillBehaviour();
            
            _killTriggerObserver.OnTriggerEnter += OnKillTriggerEnter;
        }
        
        protected override void BindController()
        {
            var tickService = ServiceLocator.Get<ITickService>();
            _controller = new AiRunnerActorController(this, tickService, _movementWidth, _flipInitialDirection);
            _controller.SetEnabled(true);
        }

        public void UpdateMovement(float axis)
            => _runBehaviour.UpdateMovement(axis);

        public void SnapHorizontalPos(float targetHorizontalPos)
            => transform.position = new Vector3(targetHorizontalPos, transform.position.y, transform.position.z);

        private void OnKillTriggerEnter(GameObject go)
        {
            var killableActor = go.GetComponentInParent<KillableActor>();

            if (killableActor == null)
                return;
            
            _killBehaviour.Kill(killableActor.KillableBehaviour);
        }

        protected override void DisposeBehaviours()
        {
            _runBehaviourVisual.Dispose();
            
            _killTriggerObserver.OnTriggerEnter -= OnKillTriggerEnter;
        }
    }
}