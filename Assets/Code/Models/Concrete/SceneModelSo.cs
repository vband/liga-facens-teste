using Code.Models.Abstraction;
using UnityEngine;

namespace Code.Models.Concrete
{
    [CreateAssetMenu(fileName = "New" + nameof(SceneModelSo), menuName = "Create " + nameof(SceneModelSo))]
    public class SceneModelSo : ScriptableObject, ISceneModel
    {
        [SerializeField] private string _sceneName;
        public string SceneName => _sceneName;
    }
}