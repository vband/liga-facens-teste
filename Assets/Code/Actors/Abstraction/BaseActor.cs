using System;
using System.Collections.Generic;
using Code.Behaviours.Abstraction;
using UnityEngine;

namespace Code.Actors.Abstraction
{
    public abstract class BaseActor : MonoBehaviour, IActor
    {
        private readonly Dictionary<Type, IBehaviour> _behaviours = new();

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

        public bool TryAddBehaviour<T>(T behaviour) where T : IBehaviour
            => _behaviours.TryAdd(typeof(T), behaviour);

        public bool TryGetBehaviour<T>(out T behaviour) where T : IBehaviour
        {
            
            var result = _behaviours.TryGetValue(typeof(T), out var element);
            behaviour = (T) element;
            return result;
        }
    }
}