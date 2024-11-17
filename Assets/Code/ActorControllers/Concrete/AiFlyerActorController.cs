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
        private readonly AnimationCurve _horizontalMovementCurve;
        private readonly AnimationCurve _verticalMovementCurve;
        private readonly float _horizontalCurveAmplitude;
        private readonly float _verticalCurveAmplitude;
        private readonly float _horizontalCurveDuration;
        private readonly float _verticalCurveDuration;

        private bool _isEnabled;
        private float _currentHorizontalMovementTime;
        private float _currentVerticalMovementTime;
        private Vector2 _lastPos;

        public AiFlyerActorController(IFlyerActor actor, ITickService tickService, AnimationCurve horizontalMovementCurve,
            AnimationCurve verticalMovementCurve, float horizontalCurveAmplitude, float verticalCurveAmplitude,
            float horizontalCurveDuration, float verticalCurveDuration)
        {
            _actor = actor;
            _tickService = tickService;
            _horizontalMovementCurve = horizontalMovementCurve;
            _verticalMovementCurve = verticalMovementCurve;
            _horizontalCurveAmplitude = horizontalCurveAmplitude;
            _verticalCurveAmplitude = verticalCurveAmplitude;
            _horizontalCurveDuration = horizontalCurveDuration;
            _verticalCurveDuration = verticalCurveDuration;

            _lastPos = CalculateCurrentPos();

            _tickService.OnTick += OnTick;
        }

        private void OnTick()
        {
            if (!_isEnabled)
                return;

            if (ShouldResetHorizontalMovementTime())
                ResetHorizontalMovementTime();

            if (ShouldResetVerticalMovementTime())
                ResetVerticalMovementTime();

            var newPos = CalculateCurrentPos();
            var delta = newPos - _lastPos;
            
            _actor.UpdateMovement(delta);
            
            _lastPos = newPos;
            
            UpdateMovementTime();
        }

        private bool ShouldResetHorizontalMovementTime()
            => _currentHorizontalMovementTime >= _horizontalCurveDuration;

        private bool ShouldResetVerticalMovementTime()
            => _currentVerticalMovementTime >= _verticalCurveDuration;

        private void ResetHorizontalMovementTime()
            => _currentHorizontalMovementTime = 0;

        private void ResetVerticalMovementTime()
            => _currentVerticalMovementTime = 0;

        private Vector2 CalculateCurrentPos()
            => new (CalculateHorizontalPos(), CalculateVerticalPos());

        private float CalculateHorizontalPos()
        {
            var normalizedHorizontalMovementTime = _currentHorizontalMovementTime / _horizontalCurveDuration;
            return _horizontalMovementCurve.Evaluate(normalizedHorizontalMovementTime) * _horizontalCurveAmplitude;
        }

        private float CalculateVerticalPos()
        {
            var normalizedVerticalMovementTime = _currentVerticalMovementTime / _verticalCurveDuration;
            return _verticalMovementCurve.Evaluate(normalizedVerticalMovementTime) * _verticalCurveAmplitude;
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
}