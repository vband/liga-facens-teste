using Code.ActorControllers.Abstraction;
using Code.Actors.Abstraction;
using UnityEngine;

namespace Code.Actors.Concrete
{
    public abstract class BaseActor : MonoBehaviour, IActor
    {
        protected IActorController _controller;

        protected abstract void InitBehaviours();
        protected abstract void DisposeBehaviours();
        protected abstract void BindController();
        protected abstract void DisposeController();

        private void Awake()
        {
            InitBehaviours();
            BindController();
        }

        private void OnDestroy()
        {
            Dispose();
        }

        public void Dispose()
        {
            DisposeBehaviours();
            DisposeController();
        }
    }
}