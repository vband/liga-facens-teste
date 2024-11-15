using Code.Actors.Abstraction;
using Code.Behaviours.Abstraction;
using Code.Behaviours.Concrete;
using UnityEngine;

namespace Code.Actors.Concrete
{
    public abstract class RunnerActor : Rigidbody2DActor, IRunnerActor
    {
        [SerializeField] private float _horizontalSpeed;
        
        private IRunBehaviour _runBehaviour;
        
        protected override void InitBehaviours()
        {
            _runBehaviour = new Rigidbody2DRunBehaviour(_rigidbody2D, _horizontalSpeed);
        }

        protected void UpdateMovement(float axis)
            => _runBehaviour.UpdateMovement(axis);
    }
}