using Code.Models.Abstraction;
using UnityEngine;

namespace Code.Models.Concrete
{
    [CreateAssetMenu(fileName = "New" + nameof(LevelSceneModelSo), menuName = "Create " + nameof(LevelSceneModelSo))]
    public class LevelSceneModelSo : ScriptableObject, ILevelSceneModel
    {
        [SerializeField] private string _sceneName;
        public string LevelSceneName => _sceneName;
    }
}