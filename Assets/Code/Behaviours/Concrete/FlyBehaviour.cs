using Code.Behaviours.Abstraction;
using UnityEngine;

namespace Code.Behaviours.Concrete
{
    public class FlyBehaviour : IFlyBehaviour
    {
        private readonly Transform _transform;

        public FlyBehaviour(Transform transform)
        {
            _transform = transform;
        }
        
        public void UpdateMovement(Vector2 delta)
        {
            var oldPos = _transform.position;
            
            var newPos = new Vector3(
                oldPos.x + delta.x,
                oldPos.y + delta.y,
                oldPos.z);

            _transform.position = newPos;
        }
    }
}