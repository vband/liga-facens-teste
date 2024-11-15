using Code.ActorControllers.Abstraction;
using Code.Actors.Abstraction;
using Code.Movement.Abstraction;
using Code.Movement.Concrete;
using UnityEngine;

namespace Code.Actors.Concrete
{
    public abstract class BaseActor : MonoBehaviour, IActor
    {
        [Tooltip("References")]
        [SerializeField] private Rigidbody2D _rigidbody2D;

        [Tooltip("Parameters")]
        [SerializeField] private float _horizontalSpeed;
        [SerializeField] private AnimationCurve _jumpVelocityCurve;
        [SerializeField] private float _jumpMaxVelocity;
        [SerializeField] private float _jumpMaxDuration;
        
        private IActorController _controller;
        private IRunner _runner;
        private IJumper _jumper;

        protected abstract IActorController GetController();

        private void Awake()
        {
            _controller = GetController();

            _runner = new Rigidbody2DRunner(_rigidbody2D, _horizontalSpeed);
            _jumper = new Rigidbody2DJumper(_rigidbody2D, _jumpVelocityCurve, _jumpMaxVelocity, _jumpMaxDuration);
            
            _controller.OnMoveAction += OnMove;
            _controller.OnJumpAction += OnJump;
        }

        private void OnDestroy()
        {
            _controller.OnMoveAction -= OnMove;
            _controller.OnJumpAction -= OnJump;
        }

        private void OnMove(float axis)
            => _runner.UpdateMovement(axis);

        private void OnJump(bool jumping)
            => _jumper.UpdateJump(jumping);
    }
}