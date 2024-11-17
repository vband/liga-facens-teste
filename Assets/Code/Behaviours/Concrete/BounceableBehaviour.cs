using Code.Behaviours.Abstraction;
using UnityEngine;

namespace Code.Behaviours.Concrete
{
    public class BounceableBehaviour : IBounceableBehaviour
    {
        private readonly Rigidbody2D _rigidbody2D;
        
        public BounceableBehaviour(Rigidbody2D rigidbody2D)
        {
            _rigidbody2D = rigidbody2D;
        }
        
        public void UpdateBounce(float verticalVelocity)
        {
            if (_rigidbody2D == null) // An actor can die while under the influence of a BounceBehaviour
                return;
            
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, verticalVelocity);
        }
    }
}