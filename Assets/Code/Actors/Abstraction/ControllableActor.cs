using Code.ActorControllers.Abstraction;
using UnityEngine;

namespace Code.Actors.Abstraction
{
    public abstract class ControllableActor : BaseActor
    {
        [SerializeField] protected Rigidbody2D _rigidbody2D;
        
        protected IActorController _controller;

        protected abstract void BindController();

        protected override void Init()
        {
            base.Init();
            BindController();
        }

        public override void Dispose()
        {
            base.Dispose();
            DisposeController();
        }
        
        private void DisposeController()
        {
            _controller.SetEnabled(false);
            _controller.Dispose();
            _controller = null;
        }
    }
}