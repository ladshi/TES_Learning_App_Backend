using AutoMapper;
using TES_Learning_App.Application_Layer.Dtos;
using TES_Learning_App.Application_Layer.Interfaces;
using TES_Learning_App.Domain.Entities;
using TES_Learning_App.Domain.Interfaces.Repositories;

namespace TES_Learning_App.Application_Layer.Services
{
    public class LevelService : ILevelService
    {
        private readonly ILevelRepository _levelRepository;
        private readonly IMapper _mapper;

        public LevelService(ILevelRepository levelRepository, IMapper mapper)
        {
            _levelRepository = levelRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LevelResponseDTO>> GetAllLevelsAsync()
        {
            var levels = await _levelRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<LevelResponseDTO>>(levels);
        }

        public async Task<LevelResponseDTO?> GetLevelByIdAsync(int id)
        {
            var level = await _levelRepository.GetByIdAsync(id);
            return _mapper.Map<LevelResponseDTO>(level);
        }

        public async Task<LevelResponseDTO> CreateLevelAsync(CreateLevelDTO createLevelDto)
        {
            var level = _mapper.Map<Level>(createLevelDto);
            var createdLevel = await _levelRepository.AddAsync(level);
            return _mapper.Map<LevelResponseDTO>(createdLevel);
        }

        public async Task<LevelResponseDTO> UpdateLevelAsync(UpdateLevelDTO updateLevelDto)
        {
            var existingLevel = await _levelRepository.GetByIdAsync(updateLevelDto.Id);
            if (existingLevel == null)
                throw new KeyNotFoundException($"Level with ID {updateLevelDto.Id} not found.");

            _mapper.Map(updateLevelDto, existingLevel);
            await _levelRepository.UpdateAsync(existingLevel);
            return _mapper.Map<LevelResponseDTO>(existingLevel);
        }

        public async Task DeleteLevelAsync(int id)
        {
            if (!await _levelRepository.ExistsAsync(id))
                throw new KeyNotFoundException($"Level with ID {id} not found.");

            await _levelRepository.DeleteAsync(id);
        }
    }
}
