﻿using Psinder.Data;
using Psinder.Dtos.ShelterDtos;
using Psinder.Helpers;

namespace Psinder.Services.Interfaces
{
    public interface IShelterService
    {
        Task<PagedResult<ShelterDto>> GetAllAsync(PageQuery query);
        Task<ShelterDto> GetByIdAsync(int id);
        Task<int> AddAsync(CreateShelterDto dto, int userId);
        Task<ShelterDto> UpdateAsync(int id, UpdateShelterDto dto, int userId);
        Task DeleteAsync(int id, int userId);
    }
}
