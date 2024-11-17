using Code.ActorControllers.Abstraction;
using Code.Actors.Abstraction;
using Code.Services.Abstraction;
using UnityEngine;

namespace Code.ActorControllers.Concrete
{
    public class AiFlyerActorController : IActorController
    {
        private readonly IFlyerActor _actor;
        private readonly ITickService _tickService;
        private readonly FlyMovementData _horizontalMovementData;
        private readonly FlyMovementData _verticalMovementData;

        private bool _isEnabled;
        private float _currentHorizontalMovementTime;
        private float _currentVerticalMovementTime;
        private Vector2 _lastPos;

        public AiFlyerActorController(IFlyerActor actor, ITickService tickService, FlyMovementData horizontalMovementData,
            FlyMovementData verticalMovementData)
        {
            _actor = actor;
            _tickService = tickService;
            _horizontalMovementData = horizontalMovementData;
            _verticalMovementData = verticalMovementData;
            
            _currentHorizontalMovementTime = _horizontalMovementData.TimeOffset;
            _currentVerticalMovementTime = _verticalMovementData.TimeOffset;

            _lastPos = CalculateCurrentPos();

            _tickService.OnTick += OnTick;
        }

        private void OnTick()
        {
            if (!_isEnabled)
                return;

            if (ShouldResetMovementTime(_currentHorizontalMovementTime, _horizontalMovementData))
                _currentHorizontalMovementTime = 0;

            if (ShouldResetMovementTime(_currentVerticalMovementTime, _verticalMovementData))
                _currentVerticalMovementTime = 0;

            var newPos = CalculateCurrentPos();
            var delta = newPos - _lastPos;
            
            _actor.UpdateMovement(delta);
            
            _lastPos = newPos;
            
            UpdateMovementTime();
        }

        private static bool ShouldResetMovementTime(float currentMovementTime, FlyMovementData movementData)
            => currentMovementTime >= movementData.CurveDuration;

        private Vector2 CalculateCurrentPos()
            => new (CalculateMovementPos(_currentHorizontalMovementTime, _horizontalMovementData),
                CalculateMovementPos(_currentVerticalMovementTime, _verticalMovementData));

        private static float CalculateMovementPos(float currentMovementTime, FlyMovementData movementData)
        {
            var normalizedMovementTime = currentMovementTime / movementData.CurveDuration;
            return movementData.MovementCurve.Evaluate(normalizedMovementTime) * movementData.CurveAmplitude;
        }

        private void UpdateMovementTime()
        {
            _currentHorizontalMovementTime += Time.deltaTime;
            _currentVerticalMovementTime += Time.deltaTime;
        }

        public void Dispose()
        {
            _tickService.OnTick -= OnTick;
        }

        public void SetEnabled(bool enabled)
        {
            _isEnabled = enabled;
            
            if (!_isEnabled)
                _actor.UpdateMovement(Vector2.zero);
        }
    }

    [System.Serializable]
    public struct FlyMovementData
    {
        public AnimationCurve MovementCurve;
        public float CurveAmplitude;
        public float CurveDuration;
        public float TimeOffset;
    }
}