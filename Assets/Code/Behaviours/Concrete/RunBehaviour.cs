using Code.Behaviours.Abstraction;
using UnityEngine;

namespace Code.Behaviours.Concrete
{
    public class RunBehaviour : IRunBehaviour
    {
        public float HorizontalVelocity => _rigidbody2D.velocity.x;
        public float HorizontalPos => _rigidbody2D.transform.position.x;

        private readonly Rigidbody2D _rigidbody2D;
        private readonly float _speed;

        public RunBehaviour(Rigidbody2D rigidbody2D, float horizontalSpeed)
        {
            _rigidbody2D = rigidbody2D;
            _speed = horizontalSpeed;
        }

        public void UpdateMovement(float axis)
        {
            var horizontalVelocity = axis * _speed;
            _rigidbody2D.velocity = new Vector2(horizontalVelocity, _rigidbody2D.velocity.y);
        }

        public void SnapHorizontalPos(float targetHorizontalPos)
        {
            var transform = _rigidbody2D.transform;
            var oldPos = transform.position;
            var newPos = new Vector3(targetHorizontalPos, oldPos.y, oldPos.z);
            transform.position = newPos;
        }
    }
}
