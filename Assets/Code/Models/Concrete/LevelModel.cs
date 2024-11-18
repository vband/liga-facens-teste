using Code.Models.Abstraction;

namespace Code.Models.Concrete
{
    [System.Serializable]
    public class LevelModel : ILevelModel
    {
        public string Label { get; }

        public bool Unlocked { get; set; }
        
        public int LevelIndex { get; }
        
        public float BestCompletionTime { get; set; }

        public LevelModel(string label, bool unlocked, int levelIndex)
        {
            Label = label;
            Unlocked = unlocked;
            LevelIndex = levelIndex;
            BestCompletionTime = float.PositiveInfinity;
        }
    }
}