using Code.ActorControllers.Abstraction;
using Code.Actors.Abstraction;
using Code.Behaviours.Abstraction;
using Code.Services.Abstraction;
using UnityEngine;

namespace Code.ActorControllers.Concrete
{
    public class AiFlyerActorController : IActorController
    {
        private readonly IFlyBehaviour _actorFlyBehaviour;
        private readonly ITickService _tickService;
        private readonly FlyMovementData _horizontalMovementData;
        private readonly FlyMovementData _verticalMovementData;
        private readonly float _horizontalMaxSpeed;
        private readonly float _verticalMaxSpeed;

        private bool _isEnabled;
        private float _currentHorizontalMovementTime;
        private float _currentVerticalMovementTime;
        private Vector2 _lastPos;

        public AiFlyerActorController(IActor actor, ITickService tickService, FlyMovementData horizontalMovementData,
            FlyMovementData verticalMovementData)
        {
            if (!actor.TryGetBehaviour(out _actorFlyBehaviour))
                return;
            
            _tickService = tickService;
            _horizontalMovementData = horizontalMovementData;
            _verticalMovementData = verticalMovementData;

            _horizontalMaxSpeed = _horizontalMovementData.CurveAmplitude / _horizontalMovementData.CurveDuration;
            _verticalMaxSpeed = _verticalMovementData.CurveAmplitude / _verticalMovementData.CurveDuration;
            
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
            
            var axis = new Vector2(
                delta.x / (_horizontalMaxSpeed * Time.deltaTime),
                delta.y / (_verticalMaxSpeed * Time.deltaTime));
            
            _actorFlyBehaviour.UpdateMovement(axis);
            
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
                _actorFlyBehaviour.UpdateMovement(Vector2.zero);
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