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
        private readonly float _initialWaitTime;

        private bool _isEnabled;
        private float _jumpStartTime;
        private float _currentWaitTime;
        private bool _isUnderInitialWaitTime = true;
        
        public AiJumperActorController(IJumperActor actor, ITickService tickService, float jumpTimeInterval,
            float initialWaitTime)
        {
            _actor = actor;
            _tickService = tickService;
            _jumpTimeInterval = jumpTimeInterval;
            _initialWaitTime = initialWaitTime;
            _currentWaitTime = Time.time;

            _tickService.OnTick += OnTick;
        }

        private void OnTick()
        {
            if (!_isEnabled)
                return;

            if (_isUnderInitialWaitTime && _currentWaitTime < _initialWaitTime)
            {
                _currentWaitTime += Time.deltaTime;
                return;
            }
            
            if (_isUnderInitialWaitTime && _currentWaitTime >= _initialWaitTime)
            {
                _isUnderInitialWaitTime = false;
                _jumpStartTime = Time.time;
            }
            
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