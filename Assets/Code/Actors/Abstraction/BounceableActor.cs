using Code.Behaviours.Abstraction;
using Code.Behaviours.Concrete;

namespace Code.Actors.Abstraction
{
    public abstract class BounceableActor : ControllableActor
    {
        private IBounceableBehaviour _bounceableBehaviour;
        
        protected override void InitBehaviours()
        {
            _bounceableBehaviour = new BounceableBehaviour(_rigidbody2D);
            TryAddBehaviour(_bounceableBehaviour);
        }

        protected void SetBounceableBehaviourActive(bool active)
            => _bounceableBehaviour.Enabled = active;
    }
}