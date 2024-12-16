using System;

namespace Code.Behaviours.Abstraction
{
    public interface IBounceBehaviour : IBehaviour
    {
        event Action OnBounce;
        void Bounce(IBounceableBehaviour bounceableBehaviour);
    }
}