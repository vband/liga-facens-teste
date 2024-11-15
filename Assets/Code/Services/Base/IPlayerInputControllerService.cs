using Code.Input.Base;

namespace Code.Services.Base
{
    public interface IPlayerInputControllerService : IService
    {
        IPlayerInputController PlayerInputController { get; }
    }
}