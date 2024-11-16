using Code.Actors.Abstraction;
using Code.Behaviours.Concrete;

namespace Code.Actors.Concrete
{
    public abstract class BounceableActor : Rigidbody2DActor, IBounceableActor
    {
        public BounceableBehaviour BounceableBehaviour { get; private set; }

        protected override void InitBehaviours()
        {
            BounceableBehaviour = new BounceableBehaviour(_rigidbody2D);
        }
    }
}