using Code.Actors.Abstraction;
using Code.Behaviours.Abstraction;
using Code.Behaviours.Concrete;
using Code.Behaviours.Visuals.Abstraction;
using Code.Behaviours.Visuals.Concrete;
using Code.Utils;
using UnityEngine;

namespace Code.Actors.Concrete
{
    public class ImmobileBouncerActor : BaseActor
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private CollisionObserver _bounceTriggerObserver;
        [SerializeField] private float _bounceVerticalVelocity;
        [SerializeField] private float _bounceDuration;
        
        private IBounceBehaviour _bounceBehaviour;
        private IBounceBehaviourVisual _bounceBehaviourVisual;
        
        protected override void InitBehaviours()
        {
            _bounceBehaviour = new BounceBehaviour(_bounceVerticalVelocity, _bounceDuration, this);
            _bounceBehaviourVisual = new BounceBehaviourVisual(_animator, _bounceBehaviour);

            _bounceTriggerObserver.OnTriggerEnter += OnBounceTriggerEnter;
        }

        private void OnBounceTriggerEnter(GameObject go)
        {
            var bounceableActor = go.GetComponentInParent<BounceableActor>();
            
            if (bounceableActor == null)
                return;
            
            _bounceBehaviour.Bounce(bounceableActor.BounceableBehaviour);
        }

        protected override void DisposeBehaviours()
        {
            _bounceBehaviourVisual.Dispose();
            
            _bounceTriggerObserver.OnTriggerEnter -= OnBounceTriggerEnter;
        }
    }
}