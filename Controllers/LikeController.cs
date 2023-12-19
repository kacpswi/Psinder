using Microsoft.AspNetCore.Mvc;
using Psinder.Dtos.AnimalDtos;
using Psinder.Extensions;
using Psinder.Services.Interfaces;

namespace Psinder.Controllers
{
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly ILikeService _likeService;
        public LikeController(ILikeService likeService)
        {
            _likeService = likeService;
        }

        [HttpPost("{animalId}")]
        public async Task<ActionResult> AddLike(int animalId)
        {
            
            await _likeService.AddLikeAsync(animalId, User.GetUserId());
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<AnimalDto>>> GetUserLikes()
        {
            var result = await _likeService.GetLikesAsync(User.GetUserId());
            return Ok(result);
        }

    }
}
