using Code.Models.Abstraction;
using Code.Models.Concrete;
using Code.Services.Abstraction;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Services.Concrete
{
    public class StartMenuSceneService : IStartMenuSceneService
    {
        private const string StartMenuPath = "StartMenu/StartMenu";
        
        private readonly ISceneModel _startMenuScene;
        
        public StartMenuSceneService()
        {
            _startMenuScene = Resources.Load<SceneModelSo>(StartMenuPath);
        }

        public void LoadStartMenu()
            => SceneManager.LoadSceneAsync(_startMenuScene.SceneName);
    }
}