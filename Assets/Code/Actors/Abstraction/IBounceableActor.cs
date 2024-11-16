using Code.Behaviours.Concrete;

namespace Code.Actors.Abstraction
{
    public interface IBounceableActor : IActor
    {
        BounceableBehaviour BounceableBehaviour { get; }
    }
}