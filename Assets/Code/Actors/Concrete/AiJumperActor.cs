using Code.ActorControllers.Concrete;
using Code.Services.Abstraction;
using Code.Services.ServiceLocator;
using UnityEngine;

namespace Code.Actors.Concrete
{
    public class AiJumperActor : JumperActor
    {
        [SerializeField] private float _jumpTimeInterval;
        
        protected override void BindController()
        {
            var tickService = ServiceLocator.Get<ITickService>();
            _controller = new AiJumperActorController(this, tickService, _jumpTimeInterval);
            _controller.SetEnabled(true);
        }
    }
}