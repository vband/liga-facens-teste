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
    public class AiRunnerActor : BounceableActor
    {
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
            TryAddBehaviour(_runBehaviour);
            
            _killBehaviour = new KillBehaviour();
            TryAddBehaviour(_killBehaviour);
            
            _runBehaviourVisual = new RunBehaviourVisual(_animator, _spriteRenderer, _runBehaviour, tickService);
            
            _killTriggerObserver.OnTriggerEnter += OnKillTriggerEnter;
        }
        
        protected override void BindController()
        {
            var tickService = ServiceLocator.Get<ITickService>();
            _controller = new AiRunnerActorController(this, tickService, _movementWidth, _flipInitialDirection);
            _controller.SetEnabled(true);
        }

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
            _runBehaviourVisual.Dispose();
            
            _killTriggerObserver.OnTriggerEnter -= OnKillTriggerEnter;
        }
    }
}