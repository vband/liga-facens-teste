using Code.ActorControllers.Abstraction;
using Code.Actors.Abstraction;
using Code.Services.Abstraction;
using UnityEngine;

namespace Code.ActorControllers.Concrete
{
    public class AiRunnerActorController : IActorController
    {
        private readonly IRunnerActor _actor;
        private readonly ITickService _tickService;
        private readonly float _movementWidth;

        private bool _isEnabled;
        private bool _hasCalculatedBounds;

        private Bounds _movementBounds;
        private int _currentMovementDirection = 1;
        
        public AiRunnerActorController(IRunnerActor actor, ITickService tickService, float movementWidth)
        {
            _actor = actor;
            _tickService = tickService;
            _movementWidth = movementWidth;

            _tickService.OnTick += OnTick;
        }

        private void OnTick()
        {
            if (!_isEnabled)
                return;

            if (!_hasCalculatedBounds)
                _movementBounds = CalculateBounds();

            if (HasReachedBoundsLimit())
                _currentMovementDirection *= -1;
            
            _actor.UpdateMovement(_currentMovementDirection);
        }

        private bool HasReachedBoundsLimit()
            => _actor.HorizontalPos - _actor.HorizontalSpeed * Time.deltaTime <= _movementBounds.min.x
               || _actor.HorizontalPos + _actor.HorizontalSpeed * Time.deltaTime >= _movementBounds.max.x;

        private Bounds CalculateBounds()
        {
            var initialPos = _actor.HorizontalPos;
            var center = new Vector3(initialPos, 0, 0);
            var size = new Vector3(_movementWidth, 0, 0);
            
            _hasCalculatedBounds = true;
            
            return new Bounds(center, size);
        }

        public void Dispose()
        {
            _tickService.OnTick -= OnTick;
        }

        public void SetEnabled(bool enabled)
        {
            _isEnabled = enabled;
            
            if (!_isEnabled)
                _actor.UpdateMovement(0);
        }
    }
}