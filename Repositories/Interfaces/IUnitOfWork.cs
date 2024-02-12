namespace Psinder.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IShelterRepository ShelterRepository { get; }
        IAnimalRepository AnimalRepository { get; }
        ILikeRepository LikeRepository { get; }
        IMessageRepository MessageRepository { get; }
        IUserRepository UserRepository { get; }
        Task<bool> Complete();
    }
}
