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
    public class AiRunnerActor : KillerActor
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private float _horizontalSpeed;
        [SerializeField] private float _movementWidth;
        [SerializeField] private bool _flipInitialDirection;
        
        private IRunBehaviour _runBehaviour;
        private IRunBehaviourVisual _runBehaviourVisual;

        protected override void InitBehaviours()
        {
            base.InitBehaviours();
            
            var tickService = ServiceLocator.Get<ITickService>();
            
            _runBehaviour = new RunBehaviour(_rigidbody2D, _horizontalSpeed);
            TryAddBehaviour(_runBehaviour);
            
            _runBehaviourVisual = new RunBehaviourVisual(_animator, _spriteRenderer, _runBehaviour, tickService);
        }
        
        protected override void BindController()
        {
            var tickService = ServiceLocator.Get<ITickService>();
            _controller = new AiRunnerActorController(this, tickService, _movementWidth, _flipInitialDirection);
            _controller.SetEnabled(true);
        }

        protected override void DisposeBehaviours()
        {
            base.DisposeBehaviours();
            
            _runBehaviourVisual.Dispose();
        }
    }
}