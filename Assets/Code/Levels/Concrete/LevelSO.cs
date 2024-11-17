using Code.Levels.Abstraction;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Levels.Concrete
{
    [CreateAssetMenu(fileName = "New" + nameof(LevelSO), menuName = "Create " + nameof(LevelSO))]
    public class LevelSO : ScriptableObject, ILevel
    {
        [SerializeField] private string _sceneName;
        
        public void LoadLevel()
            => SceneManager.LoadSceneAsync(_sceneName);
    }
}