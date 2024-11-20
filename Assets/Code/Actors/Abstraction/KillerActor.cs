using Code.Behaviours.Abstraction;
using Code.Behaviours.Concrete;
using Code.Utils;
using UnityEngine;

namespace Code.Actors.Abstraction
{
    public abstract class KillerActor : KillableActor
    {
        [SerializeField] private CollisionObserver _killTriggerObserver;

        private IKillBehaviour _killBehaviour;
        
        protected override void InitBehaviours()
        {
            base.InitBehaviours();
            
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
            base.DisposeBehaviours();
            
            _killTriggerObserver.OnTriggerEnter -= OnKillTriggerEnter;
        }
    }
}