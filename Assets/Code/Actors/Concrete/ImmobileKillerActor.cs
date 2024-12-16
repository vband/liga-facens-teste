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
            TryAddBehaviour(_killBehaviour);

            _killTriggerObserver.OnTriggerEnter += OnKillTriggerEnter;
        }

        private void OnKillTriggerEnter(GameObject go)
        {
            var actor = go.GetComponentInParent<BaseActor>();

            if (actor == null)
                return;

            if (!actor.TryGetBehaviour<IKillableBehaviour>(out var killableBehaviour))
                return;
            
            _killBehaviour.Kill(killableBehaviour);
        }

        protected override void DisposeBehaviours()
        {
            _killTriggerObserver.OnTriggerEnter -= OnKillTriggerEnter;
        }
    }
}