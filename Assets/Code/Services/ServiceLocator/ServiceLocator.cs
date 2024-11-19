using System;
using System.Collections.Generic;
using Code.Services.Abstraction;
using Code.Services.Concrete;
using Code.Utils;

namespace Code.Services.ServiceLocator
{
    public class ServiceLocator : MonoBehaviourSingleton<ServiceLocator>
    {
        private readonly Dictionary<Type, Func<IService>> _constructors = new();
        private readonly Dictionary<Type, IService> _services = new();

        public static T Get<T>() where T : IService
        {
            if (Instance._services.TryGetValue(typeof(T), out var service))
                return (T) service;
            
            service = Instance._constructors[typeof(T)]();
            Instance._services[typeof(T)] = service;

            return (T) service;
        }

        private static void Register<T>(Func<T> constructor, bool nonLazy = false) where T : IService
        {
            Instance._constructors[typeof(T)] = () => constructor();
            
            if (nonLazy)
                Get<T>();
        }

        protected override void Init()
            => InstallServices();

        private static void InstallServices()
        {
            InstallTickService();
            InstallLevelScenesService();
            InstallLevelSelectionMenuService();
            InstallLevelFinishedService();
            InstallLevelFailedService();
            InstallLevelModelsService();
            InstallStartMenuSceneService();
        }

        private static void InstallTickService()
            => Register<ITickService>(() => new TickService());

        private static void InstallLevelScenesService()
            => Register<ILevelScenesService>(() => new LevelScenesService());

        private static void InstallLevelSelectionMenuService()
            => Register<ILevelSelectionMenuService>(() => new LevelSelectionMenuService());

        private static void InstallLevelFinishedService()
            => Register<ILevelFinishedService>(() => new LevelFinishedService());

        private static void InstallLevelFailedService()
            => Register<ILevelFailedService>(() => new LevelFailedService());

        private static void InstallLevelModelsService()
            => Register<ILevelModelsService>(() => new LevelModelsService(Get<ILevelScenesService>()));

        private static void InstallStartMenuSceneService()
            => Register<IStartMenuSceneService>(() => new StartMenuSceneService());
    }
}