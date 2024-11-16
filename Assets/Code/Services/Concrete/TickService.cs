using System;
using Code.Services.Abstraction;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Services.Concrete
{
    public class TickService : ITickService
    {
        public event Action OnTick;

        public TickService()
        {
            var updater = new GameObject(nameof(TickServiceUpdater)).AddComponent<TickServiceUpdater>();
            Object.DontDestroyOnLoad(updater);
            updater.OnUpdate += () => OnTick?.Invoke();
        }
    }

    public class TickServiceUpdater : MonoBehaviour
    {
        public event Action OnUpdate;

        private void Update() => OnUpdate?.Invoke();
    }
}