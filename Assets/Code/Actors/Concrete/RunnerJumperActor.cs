﻿using Code.Actors.Abstraction;
using Code.Behaviours.Abstraction;
using Code.Behaviours.Concrete;
using Code.Behaviours.Visuals;
using Code.Behaviours.Visuals.Abstraction;
using Code.Behaviours.Visuals.Concrete;
using UnityEngine;

namespace Code.Actors.Concrete
{
    public abstract class RunnerJumperActor : Rigidbody2DActor, IRunnerJumperActor
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private float _horizontalSpeed;
        [SerializeField] private float _jumpVelocity;
        [SerializeField] private float _jumpMaxDuration;
        
        private IRunBehaviour _runBehaviour;
        private IJumpBehaviour _jumpBehaviour;
        private IGroundCheckBehaviour _groundCheckBehaviour;
        private IRunBehaviourVisual _runBehaviourVisual;
        
        protected override void InitBehaviours()
        {
            _runBehaviour = new Rigidbody2DRunBehaviour(_rigidbody2D, _horizontalSpeed);
            _jumpBehaviour = new Rigidbody2DJumpBehaviour(_rigidbody2D, this, _jumpVelocity, _jumpMaxDuration);
            _groundCheckBehaviour = new GroundCheckBehaviour();
            _runBehaviourVisual = new RunBehaviourVisual(_animator, _spriteRenderer, _runBehaviour, this);
        }

        public void UpdateMovement(float axis)
            => _runBehaviour.UpdateMovement(axis);
        
        public void UpdateJump(bool jumping)
            => _jumpBehaviour.UpdateJump(jumping, _groundCheckBehaviour.IsGrounded);

        private void OnTriggerEnter2D(Collider2D other)
            => _groundCheckBehaviour.OnNewContact(other.gameObject);

        private void OnTriggerExit2D(Collider2D other)
            => _groundCheckBehaviour.OnLoseContact(other.gameObject);
    }
}