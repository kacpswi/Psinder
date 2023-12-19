using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Psinder.Data;
using Psinder.Dtos.AnimalDtos;
using Psinder.Exceptions;
using Psinder.Repositories.Interfaces;
using Psinder.Services.Interfaces;

namespace Psinder.Services
{
    public class LikeService : ILikeService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public LikeService(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public async Task AddLikeAsync(int animalId, int userId)
        {
            var animal = await _uow.AnimalRepository.GetByIdAsync(animalId);
            var user = await _uow.LikeRepository.GetUserWithLikesAsync(userId);

            if(user == null)
            {
                throw new NotFoundException("User not found");
            }

            var userLike = await _uow.LikeRepository.GetUserLikeAsync(userId, animalId);

            if (userLike != null)
            {
                throw new BadRequestException("You already like this animal");
            }

            userLike = new UserLike
            {
                UserId = userId,
                AnimalId = animalId,
            };

            user.LikedAnimals.Add(userLike);

        }

        public async Task<List<AnimalDto>> GetLikesAsync(int userId)
        {
            var animals = await _uow.LikeRepository.GetUserLikesAsync(userId);
            var result = _mapper.Map<List<AnimalDto>>(animals);
            return result;
            
        }
    }
}
