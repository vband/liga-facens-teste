using System;
using Code.Actors.Abstraction;
using Code.Behaviours.Abstraction;
using Object = UnityEngine.Object;

namespace Code.Behaviours.Concrete
{
    public class KillableBehaviour : IKillableBehaviour
    {
        public event Action OnDied;

        private readonly KillableActor _actor;

        public KillableBehaviour(KillableActor actor)
        {
            _actor = actor;
        }

        public void Die()
        {
            OnDied?.Invoke();
            Object.Destroy(_actor.gameObject);
        }
    }
}