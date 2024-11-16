using Code.Behaviours.Abstraction;
using Code.Behaviours.Concrete;

namespace Code.Actors.Abstraction
{
    public abstract class KillableActor : BounceableActor
    {
        public IKillableBehaviour KillableBehaviour { get; private set; }

        protected override void InitBehaviours()
        {
            base.InitBehaviours();
            KillableBehaviour = new KillableBehaviour(this);
        }
    }
}