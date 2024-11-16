using UnityEngine;

namespace Code.Actors.Abstraction
{
    public abstract class BaseActor : MonoBehaviour, IActor
    {
        protected abstract void InitBehaviours();
        protected abstract void DisposeBehaviours();

        private void Awake()
        {
            Init();
        }

        protected virtual void Init()
        {
            InitBehaviours();
        }

        private void OnDestroy()
        {
            Dispose();
        }

        public virtual void Dispose()
        {
            DisposeBehaviours();
        }
    }
}