using Code.ActorControllers.Concrete;
using Code.Services.Abstraction;
using Code.Services.ServiceLocator;

namespace Code.Actors.Concrete
{
    public class PlayerActor : RunnerJumperActor
    {
        protected override void BindController()
        {
            var tickService = ServiceLocator.Get<ITickService>();
            _controller = new InputActorController(this, tickService);
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