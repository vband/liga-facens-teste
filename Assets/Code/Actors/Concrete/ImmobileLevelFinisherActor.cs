using Code.Actors.Abstraction;
using Code.Behaviours.Abstraction;
using Code.Behaviours.Concrete;
using Code.Services.Abstraction;
using Code.Services.ServiceLocator;
using Code.Utils;
using UnityEngine;

namespace Code.Actors.Concrete
{
    public class ImmobileLevelFinisherActor : BaseActor
    {
        [SerializeField] private CollisionObserver _finishLevelTriggerObserver;
        
        private IFinishLevelBehaviour _finishLevelBehaviour;
        
        protected override void InitBehaviours()
        {
            var levelFinishedMenuService = ServiceLocator.Get<ILevelFinishedMenuService>();
            var levelScenesService = ServiceLocator.Get<ILevelScenesService>();
            _finishLevelBehaviour = new FinishLevelBehaviour(levelFinishedMenuService, levelScenesService);

            _finishLevelTriggerObserver.OnTriggerEnter += OnFinishLevelTriggerEnter;
        }

        private void OnFinishLevelTriggerEnter(GameObject go)
        {
            var playerActor = go.GetComponentInParent<PlayerActor>();

            if (playerActor == null)
                return;
            
            _finishLevelBehaviour.FinishCurrentLevel();
        }

        protected override void DisposeBehaviours()
        {
            _finishLevelTriggerObserver.OnTriggerEnter -= OnFinishLevelTriggerEnter;
        }
    }
}