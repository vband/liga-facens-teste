using Code.Behaviours.Abstraction;
using Code.Behaviours.Concrete;
using Code.Behaviours.Visuals.Abstraction;
using Code.Behaviours.Visuals.Concrete;
using UnityEngine;

namespace Code.Actors.Abstraction
{
    public abstract class KillableActor : BounceableActor
    {
        [SerializeField] protected Animator _animator;
        [SerializeField] private float _waitTimeBeforeDeathNotification;

        private IKillableBehaviour _killableBehaviour;
        private IKillableBehaviourVisual _killableBehaviourVisual;

        protected override void InitBehaviours()
        {
            base.InitBehaviours();
            
            _killableBehaviour = new KillableBehaviour(this, _waitTimeBeforeDeathNotification);
            TryAddBehaviour(_killableBehaviour);
            
            _killableBehaviourVisual = new KillableBehaviourVisual(_animator, _killableBehaviour);

            _killableBehaviour.OnDied += RigidbodySleep;
            _killableBehaviour.OnDied += DisableBounceableBehaviour;
        }

        private void RigidbodySleep()
            => _rigidbody2D.Sleep();

        private void DisableBounceableBehaviour()
            => SetBounceableBehaviourActive(false);

        protected override void DisposeBehaviours()
        {
            _killableBehaviourVisual.Dispose();
            
            _killableBehaviour.OnDied -= RigidbodySleep;
            _killableBehaviour.OnDied -= DisableBounceableBehaviour;
        }
    }
}