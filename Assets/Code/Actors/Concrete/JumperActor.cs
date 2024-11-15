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

        protected override void InitBehaviours()
        {
            base.InitBehaviours();
            _jumpBehaviour = new Rigidbody2DJumpBehaviour(_rigidbody2D, this, _jumpVelocity, _jumpMaxDuration);
        }
        
        protected void UpdateJump(bool jumping)
            => _jumpBehaviour.UpdateJump(jumping);
    }
}