using Code.ActorControllers.Abstraction;
using Code.Actors.Abstraction;
using Code.Services.Abstraction;
using Code.Services.Concrete;

namespace Code.Actors.Concrete
{
    public class InputSystemActor : BaseActor
    {
        private IActorController _controller;
        
        protected override IActorController GetController()
        {
            if (_controller != null)
                return _controller;

            var inputSystemActorControllerService = ServiceLocator.Get<IInputSystemActorControllerService>();
            _controller = inputSystemActorControllerService.ActorController;

            return _controller;
        }
    }
}