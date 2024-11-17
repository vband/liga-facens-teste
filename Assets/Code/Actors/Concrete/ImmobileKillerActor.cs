using Code.Actors.Abstraction;
using Code.Behaviours.Abstraction;
using Code.Behaviours.Concrete;
using Code.Utils;
using UnityEngine;

namespace Code.Actors.Concrete
{
    public class ImmobileKillerActor : BaseActor
    {
        [SerializeField] private CollisionObserver _killTriggerObserver;

        private IKillBehaviour _killBehaviour;
        
        protected override void InitBehaviours()
        {
            _killBehaviour = new KillBehaviour();

            _killTriggerObserver.OnTriggerEnter += OnKillTriggerEnter;
        }

        private void OnKillTriggerEnter(GameObject go)
        {
            var killableActor = go.GetComponentInParent<KillableActor>();

            if (killableActor == null)
                return;
            
            _killBehaviour.Kill(killableActor.KillableBehaviour);
        }

        protected override void DisposeBehaviours()
        {
            _killTriggerObserver.OnTriggerEnter -= OnKillTriggerEnter;
        }
    }
}