using Code.ActorControllers.Abstraction;

namespace Code.Services.Abstraction
{
    public interface IInputSystemActorControllerService : IService
    {
        IActorController ActorController { get; }
    }
}