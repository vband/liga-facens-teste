using Code.Movement.Abstraction;
using UnityEngine;

namespace Code.Movement.Concrete
{
    public class Rigidbody2DRunner : IRunner
    {
        public float HorizontalVelocity => _rigidbody2D.velocity.x;

        private readonly Rigidbody2D _rigidbody2D;
        private readonly float _speed;

        public Rigidbody2DRunner(Rigidbody2D rigidbody2D, float horizontalSpeed)
        {
            _rigidbody2D = rigidbody2D;
            _speed = horizontalSpeed;
        }

        public void UpdateMovement(float axis)
        {
            var horizontalVelocity = axis * _speed * Time.deltaTime;
            _rigidbody2D.velocity = new Vector2(horizontalVelocity, _rigidbody2D.velocity.y);
        }
    }
}
