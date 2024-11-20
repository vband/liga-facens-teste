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
    public class AiFlyerActor : ControllableActor
    {
        [SerializeField] private CollisionObserver _killTriggerObserver;
        [SerializeField] private FlyMovementData _horizontalMovementData;
        [SerializeField] private FlyMovementData _verticalMovementData;

        private IFlyBehaviour _flyBehaviour;
        private IKillBehaviour _killBehaviour;
        
        protected override void InitBehaviours()
        {
            _flyBehaviour = new FlyBehaviour(transform,
                _horizontalMovementData.CurveAmplitude / _horizontalMovementData.CurveDuration,
                _verticalMovementData.CurveAmplitude / _verticalMovementData.CurveDuration);
            TryAddBehaviour(_flyBehaviour);
            
            _killBehaviour = new KillBehaviour();
            TryAddBehaviour(_killBehaviour);
            
            _killTriggerObserver.OnTriggerEnter += OnKillTriggerEnter;
        }

        protected override void BindController()
        {
            var tickService = ServiceLocator.Get<ITickService>();

            _controller = new AiFlyerActorController(this, tickService, _horizontalMovementData, _verticalMovementData);
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
            _killTriggerObserver.OnTriggerEnter -= OnKillTriggerEnter;
        }
    }
}