using Code.ActorControllers.Abstraction;
using Code.Actors.Abstraction;
using Code.Behaviours.Abstraction;
using Code.Services.Abstraction;
using UnityEngine;

namespace Code.ActorControllers.Concrete
{
    public class AiRunnerActorController : IActorController
    {
        private const float Epsilon = 0.1f;

        private readonly IRunBehaviour _actorRunBehaviour;
        private readonly ITickService _tickService;
        private readonly float _movementWidth;

        private bool _isEnabled;
        private bool _hasCalculatedBounds;

        private Bounds _movementBounds;
        private int _currentMovementDirection = 1;
        
        public AiRunnerActorController(IActor actor, ITickService tickService, float movementWidth,
            bool flipInitialDirection)
        {
            if (!actor.TryGetBehaviour(out _actorRunBehaviour))
                return;
            
            _tickService = tickService;
            _movementWidth = movementWidth;

            if (flipInitialDirection)
                _currentMovementDirection *= -1;

            _tickService.OnTick += OnTick;
        }

        private void OnTick()
        {
            if (!_isEnabled)
                return;

            if (!_hasCalculatedBounds)
                _movementBounds = CalculateBounds();

            if (HasReachedBoundsLimit())
            {
                SnapPositionInsideBounds();
                _currentMovementDirection *= -1;
            }
            
            _actorRunBehaviour.UpdateMovement(_currentMovementDirection);
        }

        private bool HasReachedBoundsLimit()
            => _actorRunBehaviour.HorizontalPos <= _movementBounds.min.x || _actorRunBehaviour.HorizontalPos >= _movementBounds.max.x;

        private void SnapPositionInsideBounds()
        {
            if (_actorRunBehaviour.HorizontalPos > _movementBounds.center.x)
                _actorRunBehaviour.SnapHorizontalPos(_movementBounds.max.x - Epsilon);
            else
                _actorRunBehaviour.SnapHorizontalPos(_movementBounds.min.x + Epsilon);
        }

        private Bounds CalculateBounds()
        {
            var initialPos = _actorRunBehaviour.HorizontalPos;
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
                _actorRunBehaviour.UpdateMovement(0);
        }
    }
}