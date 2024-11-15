using Code.ActorControllers.Abstraction;
using Code.ActorControllers.Concrete;
using Code.Services.Abstraction;

namespace Code.Services.Concrete
{
    public class InputSystemActorControllerService : IInputSystemActorControllerService
    {
        public IActorController ActorController { get; }

        public InputSystemActorControllerService()
        {
            ActorController = new InputSystemActorController();
            ActorController.SetEnabled(true);
        }
    }
}