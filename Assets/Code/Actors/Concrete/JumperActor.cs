using Code.Actors.Abstraction;
using Code.Behaviours.Abstraction;
using Code.Behaviours.Concrete;
using UnityEngine;

namespace Code.Actors.Concrete
{
    public abstract class JumperActor : RunnerActor, IJumperActor
    {
        [SerializeField] private float _jumpVelocity;
        [SerializeField] private float _jumpMaxDuration;
        
        private IJumpBehaviour _jumpBehaviour;
        private IGroundCheckBehaviour _groundCheckBehaviour;

        protected override void InitBehaviours()
        {
            base.InitBehaviours();
            _jumpBehaviour = new Rigidbody2DJumpBehaviour(_rigidbody2D, this, _jumpVelocity, _jumpMaxDuration);
            _groundCheckBehaviour = new GroundCheckBehaviour();
        }
        
        protected void UpdateJump(bool jumping)
            => _jumpBehaviour.UpdateJump(jumping, _groundCheckBehaviour.IsGrounded);

        private void OnTriggerEnter2D(Collider2D other)
            => _groundCheckBehaviour.OnNewContact(other.gameObject);

        private void OnTriggerExit2D(Collider2D other)
            => _groundCheckBehaviour.OnLoseContact(other.gameObject);
    }
}