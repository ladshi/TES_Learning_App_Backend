using TES_Learning_App.Application_Layer.Dtos;

namespace TES_Learning_App.Application_Layer.Interfaces
{
    public interface ILevelService
    {
        Task<IEnumerable<LevelResponseDTO>> GetAllLevelsAsync();
        Task<LevelResponseDTO?> GetLevelByIdAsync(int id);
        Task<LevelResponseDTO> CreateLevelAsync(CreateLevelDTO createLevelDto);
        Task<LevelResponseDTO> UpdateLevelAsync(UpdateLevelDTO updateLevelDto);
        Task DeleteLevelAsync(int id);
    }
}
