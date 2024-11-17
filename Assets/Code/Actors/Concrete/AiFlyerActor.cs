using Code.ActorControllers.Concrete;
using Code.Actors.Abstraction;
using Code.Behaviours.Abstraction;
using Code.Behaviours.Concrete;
using Code.Services.Abstraction;
using Code.Services.ServiceLocator;
using Code.Utils;
using UnityEngine;

namespace Code.Actors.Concrete
{
    public class AiFlyerActor : ControllableActor, IFlyerActor
    {
        [SerializeField] private CollisionObserver _killTriggerObserver;
        [SerializeField] private FlyMovementData _horizontalMovementData;
        [SerializeField] private FlyMovementData _verticalMovementData;

        private IFlyBehaviour _flyBehaviour;
        private IKillBehaviour _killBehaviour;
        
        protected override void InitBehaviours()
        {
            _flyBehaviour = new FlyBehaviour(transform);
            _killBehaviour = new KillBehaviour();
            
            _killTriggerObserver.OnTriggerEnter += OnKillTriggerEnter;
        }

        protected override void BindController()
        {
            var tickService = ServiceLocator.Get<ITickService>();

            _controller = new AiFlyerActorController(this, tickService, _horizontalMovementData, _verticalMovementData);
            _controller.SetEnabled(true);
        }

        public void UpdateMovement(Vector2 position)
            => _flyBehaviour.UpdateMovement(position);

        private void OnKillTriggerEnter(GameObject go)
        {
            var killableActor = go.GetComponentInParent<KillableActor>();

            if (killableActor == null)
                return;
            
            _killBehaviour.Kill(killableActor.KillableBehaviour);
        }

        protected override void DisposeBehaviours()
        {
            _killTriggerObserver.OnTriggerEnter -= OnKillTriggerEnter;
        }
    }
}