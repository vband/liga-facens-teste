using System;

namespace Code.Behaviours.Abstraction
{
    public interface IBounceBehaviour
    {
        event Action OnBounce;
        void Bounce(IBounceableBehaviour bounceableBehaviour);
    }
}