using System.Collections;
using Code.Behaviours.Abstraction;
using UnityEngine;

namespace Code.Behaviours.Concrete
{
    public class Rigidbody2DJumpBehaviour : IJumpBehaviour
    {
        public float VerticalVelocity => _rigidbody2D.velocity.y;

        private readonly Rigidbody2D _rigidbody2D;
        private readonly MonoBehaviour _coroutineStarter;
        private readonly float _jumpVelocity;
        private readonly float _jumpMaxDuration;
        private readonly WaitForEndOfFrame _waitForEndOfFrame;

        private bool _isJumping;
        private float _jumpStartTime;
        private Coroutine _jumpCoroutine;

        public Rigidbody2DJumpBehaviour(Rigidbody2D rigidbody2D, MonoBehaviour coroutineStarter, float jumpVelocity,
            float jumpMaxDuration)
        {
            _rigidbody2D = rigidbody2D;
            _coroutineStarter = coroutineStarter;
            _jumpVelocity = jumpVelocity;
            _jumpMaxDuration = jumpMaxDuration;
            _waitForEndOfFrame = new WaitForEndOfFrame();
        }

        public void UpdateJump(bool jumping)
        {
            if (!_isJumping && jumping && _jumpCoroutine == null)
                StartJumpCoroutine();
            if (!jumping && _jumpCoroutine != null)
                StopJumpCoroutine();

            _isJumping = jumping;
        }

        private IEnumerator JumpCoroutine()
        {
            _jumpStartTime = Time.time;

            while (Time.time - _jumpStartTime < _jumpMaxDuration)
            {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpVelocity);
                yield return _waitForEndOfFrame;
            }
            
            StopJumpCoroutine();
        }

        private void StartJumpCoroutine()
            => _jumpCoroutine = _coroutineStarter.StartCoroutine(JumpCoroutine());

        private void StopJumpCoroutine()
        {
            _coroutineStarter.StopCoroutine(_jumpCoroutine);
            _jumpCoroutine = null;
        }
    }
}
