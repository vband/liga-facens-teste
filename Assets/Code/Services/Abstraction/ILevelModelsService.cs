using System.Collections.Generic;
using System.Threading.Tasks;
using Code.Models.Abstraction;

namespace Code.Services.Abstraction
{
    public interface ILevelModelsService : IService
    {
        Task<List<ILevelModel>> LoadLevelModelsAsync();
        Task WriteLevelUnlockedAsync(int levelIndex);
    }
}