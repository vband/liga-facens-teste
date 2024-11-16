using System;

namespace Code.Behaviours.Abstraction
{
    public interface IBouncerBehaviour
    {
        event Action OnBounce;
        void Bounce(IBounceableBehaviour bounceableBehaviour);
    }
}