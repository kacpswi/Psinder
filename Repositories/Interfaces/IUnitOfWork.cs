namespace Psinder.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IShelterRepository ShelterRepository { get; }
        IAnimalRepository AnimalRepository { get; }
        Task<bool> Complete();
    }
}
