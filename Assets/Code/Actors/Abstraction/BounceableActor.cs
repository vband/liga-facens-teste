using Code.Behaviours.Concrete;

namespace Code.Actors.Abstraction
{
    public abstract class BounceableActor : ControllableActor
    {
        public BounceableBehaviour BounceableBehaviour { get; private set; }

        protected override void InitBehaviours()
        {
            BounceableBehaviour = new BounceableBehaviour(_rigidbody2D);
        }
    }
}