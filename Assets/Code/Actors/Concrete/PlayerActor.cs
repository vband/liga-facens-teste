using Code.ActorControllers.Concrete;

namespace Code.Actors.Concrete
{
    public class PlayerActor : RunnerJumperActor
    {
        protected override void BindController()
        {
            _controller = new InputActorController(this);
            _controller.SetEnabled(true);
        }

        protected override void DisposeController()
        {
            _controller.SetEnabled(false);
            _controller.Dispose();
            _controller = null;
        }
    }
}