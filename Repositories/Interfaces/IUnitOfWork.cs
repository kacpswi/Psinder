namespace Psinder.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IShelterRepository ShelterRepository { get; }
        IAnimalRepository AnimalRepository { get; }
        ILikeRepository LikeRepository { get; }
        Task<bool> Complete();
    }
}
