using Code.Actors.Abstraction;
using Code.Behaviours.Abstraction;
using Code.Behaviours.Concrete;
using UnityEngine;

namespace Code.Actors.Concrete
{
    public class ImmobileBouncerActor : BaseActor
    {
        [SerializeField] private float _bounceVerticalVelocity;
        [SerializeField] private float _bounceDuration;
        
        private IBouncerBehaviour _bouncerBehaviour;
        
        protected override void InitBehaviours()
        {
            _bouncerBehaviour = new BounceBehaviour(_bounceVerticalVelocity, _bounceDuration, this);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var bounceableActor = other.gameObject.GetComponent<BounceableActor>();
            
            if (bounceableActor == null)
                return;
            
            _bouncerBehaviour.Bounce(bounceableActor.BounceableBehaviour);
        }

        protected override void DisposeBehaviours()
        {
            // TODO: Dispose bounceBehaviourVisual
        }
    }
}