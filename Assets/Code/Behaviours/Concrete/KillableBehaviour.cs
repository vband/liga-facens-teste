using System;
using System.Collections;
using Code.Actors.Abstraction;
using Code.Behaviours.Abstraction;
using UnityEngine;

namespace Code.Behaviours.Concrete
{
    public class KillableBehaviour : IKillableBehaviour
    {
        public event Action OnDied;
        public event Action OnDeathNotification;

        private readonly KillableActor _actor;
        private readonly WaitForSeconds _waitForSeconds;

        public KillableBehaviour(KillableActor actor, float waitTimeBeforeDeathNotification)
        {
            _actor = actor;
            _waitForSeconds = new WaitForSeconds(waitTimeBeforeDeathNotification);
        }

        public void Die()
        {
            OnDied?.Invoke();
            _actor.Dispose();
            _actor.StartCoroutine(InvokeDeathNotificationCoroutine());
        }

        private IEnumerator InvokeDeathNotificationCoroutine()
        {
            yield return _waitForSeconds;
            OnDeathNotification?.Invoke();
        }
    }
}