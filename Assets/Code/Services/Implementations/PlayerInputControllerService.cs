using Code.Input.Base;
using Code.Input.Implementations;
using Code.Services.Base;

namespace Code.Services.Implementations
{
    public class PlayerInputControllerService : IPlayerInputControllerService
    {
        public IPlayerInputController PlayerInputController { get; }

        public PlayerInputControllerService()
        {
            PlayerInputController = new InputSystemController();
        }
    }
}