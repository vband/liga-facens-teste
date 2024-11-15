using Code.ActorControllers.Abstraction;
using Code.ActorControllers.Concrete;

namespace Code.Actors.Concrete
{
    public class PlayerActor : JumperActor
    {
        private IRunnerJumperActorController _controller;
        
        protected override void BindController()
        {
            _controller = new InputActorController();
            _controller.SetEnabled(true);
            
            _controller.OnRunAction += UpdateMovement;
            _controller.OnJumpAction += UpdateJump;
        }

        protected override void DisposeController()
        {
            _controller.OnRunAction -= UpdateMovement;
            _controller.OnJumpAction -= UpdateJump;

            _controller.SetEnabled(false);
            _controller.Dispose();
            _controller = null;
        }
    }
}