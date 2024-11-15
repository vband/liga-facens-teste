using Code.Movement.Abstraction;
using UnityEngine;

namespace Code.Movement.Concrete
{
    public class Rigidbody2DJumper : IJumper
    {
        public float VerticalVelocity => _rigidbody2D.velocity.y;

        private readonly Rigidbody2D _rigidbody2D;
        private readonly AnimationCurve _jumpVelocityCurve;
        private readonly float _jumpMaxVelocity;
        private readonly float _jumpMaxDuration;

        private bool _isJumping;
        private float _jumpStartTime;

        public Rigidbody2DJumper(Rigidbody2D rigidbody2D, AnimationCurve jumpVelocityCurve, float jumpMaxVelocity,
            float jumpMaxDuration)
        {
            _rigidbody2D = rigidbody2D;
            _jumpVelocityCurve = jumpVelocityCurve;
            _jumpMaxVelocity = jumpMaxVelocity;
            _jumpMaxDuration = jumpMaxDuration;
        }

        public void UpdateJump(bool jumping)
        {
            float verticalVelocity;
            
            if (!_isJumping && jumping)
                _jumpStartTime = Time.time;

            if (!jumping)
                verticalVelocity = 0;
            else
            {
                var jumpCurrentTime = Time.time - _jumpStartTime;
                var jumpCurrentTimeNormalized = Mathf.Clamp01(_jumpMaxDuration / jumpCurrentTime);
                verticalVelocity = _jumpVelocityCurve.Evaluate(jumpCurrentTimeNormalized) * _jumpMaxVelocity;
            }

            verticalVelocity = Mathf.Min(verticalVelocity, _rigidbody2D.velocity.y);
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, verticalVelocity);

            _isJumping = jumping;
        }
    }
}
