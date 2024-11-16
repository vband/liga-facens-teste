using System;
using System.Collections;
using Code.Behaviours.Abstraction;
using UnityEngine;

namespace Code.Behaviours.Concrete
{
    public class BounceBehaviour : IBounceBehaviour
    {
        public event Action OnBounce;

        private readonly float _verticalVelocity;
        private readonly float _maxDuration;
        private readonly MonoBehaviour _coroutineStarter;
        private readonly WaitForEndOfFrame _waitForEndOfFrame;

        public BounceBehaviour(float verticalVelocity, float maxDuration, MonoBehaviour coroutineStarter)
        {
            _verticalVelocity = verticalVelocity;
            _maxDuration = maxDuration;
            _coroutineStarter = coroutineStarter;
            _waitForEndOfFrame = new WaitForEndOfFrame();
        }

        public void Bounce(IBounceableBehaviour bounceableBehaviour)
        {
            OnBounce?.Invoke();
            _coroutineStarter.StartCoroutine(BounceCoroutine(bounceableBehaviour, _verticalVelocity, _maxDuration));
        }

        private IEnumerator BounceCoroutine(IBounceableBehaviour bounceableBehaviour, float verticalVelocity,
            float maxDuration)
        {
            var startTime = Time.time;

            while (Time.time - startTime < maxDuration)
            {
                bounceableBehaviour.UpdateBounce(verticalVelocity);
                yield return _waitForEndOfFrame;
            }
        }
    }
}