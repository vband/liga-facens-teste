using Code.Movement.Abstraction;
using UnityEngine;

namespace Code.Movement.Concrete
{
    public class Rigidbody2DJumper : IJumper
    {
        public float VerticalVelocity => _rigidbody2D.velocity.y;

        private readonly Rigidbody2D _rigidbody2D;
        private readonly AnimationCurve _jumpVelocityCurve;
        private readonly float _jumpMaxDuration;

        public Rigidbody2DJumper(Rigidbody2D rigidbody2D, AnimationCurve jumpVelocityCurve, float jumpMaxDuration)
        {
            _rigidbody2D = rigidbody2D;
            _jumpVelocityCurve = jumpVelocityCurve;
            _jumpMaxDuration = jumpMaxDuration;
        }

        public void UpdateJump(bool jumping)
        {
            
        }
    }
}
