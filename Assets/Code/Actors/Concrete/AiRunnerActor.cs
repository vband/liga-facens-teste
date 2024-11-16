using Code.ActorControllers.Concrete;
using Code.Services.Abstraction;
using Code.Services.ServiceLocator;
using UnityEngine;

namespace Code.Actors.Concrete
{
    public class AiRunnerActor : RunnerActor
    {
        [SerializeField] private float _movementWidth;
        
        protected override void BindController()
        {
            var tickService = ServiceLocator.Get<ITickService>();
            _controller = new AiRunnerActorController(this, tickService, _movementWidth);
            _controller.SetEnabled(true);
        }
    }
}