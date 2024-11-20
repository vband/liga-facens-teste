using Code.ActorControllers.Concrete;
using Code.Actors.Abstraction;
using Code.Behaviours.Abstraction;
using Code.Behaviours.Concrete;
using Code.Services.Abstraction;
using Code.Services.ServiceLocator;
using UnityEngine;

namespace Code.Actors.Concrete
{
    public class AiFlyerActor : KillerActor
    {
        [SerializeField] private FlyMovementData _horizontalMovementData;
        [SerializeField] private FlyMovementData _verticalMovementData;

        private IFlyBehaviour _flyBehaviour;
        
        protected override void InitBehaviours()
        {
            base.InitBehaviours();
            
            _flyBehaviour = new FlyBehaviour(transform,
                _horizontalMovementData.CurveAmplitude / _horizontalMovementData.CurveDuration,
                _verticalMovementData.CurveAmplitude / _verticalMovementData.CurveDuration);
            TryAddBehaviour(_flyBehaviour);
        }

        protected override void BindController()
        {
            var tickService = ServiceLocator.Get<ITickService>();

            _controller = new AiFlyerActorController(this, tickService, _horizontalMovementData, _verticalMovementData);
            _controller.SetEnabled(true);
        }
    }
}