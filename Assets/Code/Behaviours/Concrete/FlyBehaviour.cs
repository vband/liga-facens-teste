using Code.Behaviours.Abstraction;
using UnityEngine;

namespace Code.Behaviours.Concrete
{
    public class FlyBehaviour : IFlyBehaviour
    {
        private readonly Transform _transform;
        private readonly float _horizontalMaxSpeed;
        private readonly float _verticalMaxSpeed;

        public FlyBehaviour(Transform transform, float maxSpeed)
        {
            _transform = transform;
            _horizontalMaxSpeed = _verticalMaxSpeed = maxSpeed;
        }

        public FlyBehaviour(Transform transform, float horizontalMaxSpeed, float verticalMaxSpeed)
        {
            _transform = transform;
            _horizontalMaxSpeed = horizontalMaxSpeed;
            _verticalMaxSpeed = verticalMaxSpeed;
        }

        public void UpdateMovement(Vector2 axis)
        {
            var oldPos = _transform.position;
            
            var delta = new Vector2(
                axis.x * _horizontalMaxSpeed * Time.deltaTime,
                axis.y * _verticalMaxSpeed * Time.deltaTime);
            
            var newPos = new Vector3(
                oldPos.x + delta.x,
                oldPos.y + delta.y,
                oldPos.z);

            _transform.position = newPos;
        }
    }
}