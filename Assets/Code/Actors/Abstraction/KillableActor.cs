using Code.Behaviours.Abstraction;
using Code.Behaviours.Concrete;
using Code.Behaviours.Visuals.Abstraction;
using Code.Behaviours.Visuals.Concrete;
using UnityEngine;

namespace Code.Actors.Abstraction
{
    public abstract class KillableActor : BounceableActor
    {
        public IKillableBehaviour KillableBehaviour { get; private set; }

        [SerializeField] protected Animator _animator;
        [SerializeField] private float _waitTimeBeforeDeathNotification;
        
        private IKillableBehaviourVisual _killableBehaviourVisual;

        protected override void InitBehaviours()
        {
            base.InitBehaviours();
            KillableBehaviour = new KillableBehaviour(this, _waitTimeBeforeDeathNotification);
            _killableBehaviourVisual = new KillableBehaviourVisual(_animator, KillableBehaviour);

            KillableBehaviour.OnDied += RigidbodySleep;
        }

        private void RigidbodySleep()
            => _rigidbody2D.Sleep();

        protected override void DisposeBehaviours()
        {
            _killableBehaviourVisual.Dispose();
            KillableBehaviour.OnDied -= RigidbodySleep;
        }
    }
}