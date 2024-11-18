using Code.LevelScenes.Abstraction;
using UnityEngine;

namespace Code.LevelScenes.Concrete
{
    [CreateAssetMenu(fileName = "New" + nameof(LevelSceneSO), menuName = "Create " + nameof(LevelSceneSO))]
    public class LevelSceneSO : ScriptableObject, ILevelScene
    {
        [SerializeField] private string _sceneName;
        public string LevelSceneName => _sceneName;
    }
}