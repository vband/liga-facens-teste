using Code.ActorControllers.Abstraction;
using Code.Actors.Abstraction;
using Code.Services.Abstraction;
using UnityEngine;

namespace Code.ActorControllers.Concrete
{
    public class AiJumperActorController : IActorController
    {
        private readonly IJumperActor _actor;
        private readonly ITickService _tickService;
        private readonly float _jumpTimeInterval;

        private bool _isEnabled;
        private float _jumpStartTime;
        
        public AiJumperActorController(IJumperActor actor, ITickService tickService, float jumpTimeInterval)
        {
            _actor = actor;
            _tickService = tickService;
            _jumpTimeInterval = jumpTimeInterval;

            _tickService.OnTick += OnTick;
        }

        private void OnTick()
        {
            if (!_isEnabled)
                return;
            
            if (Time.time - _jumpStartTime < _jumpTimeInterval)
                _actor.UpdateJump(true);
            else
            {
                _actor.UpdateJump(false);
                _jumpStartTime = Time.time;
            }
        }
        
        public void Dispose()
        {
            _tickService.OnTick -= OnTick;
        }

        public void SetEnabled(bool enabled)
        {
            _isEnabled = enabled;
            
            if (!_isEnabled)
                _actor.UpdateJump(false);
        }
    }
}