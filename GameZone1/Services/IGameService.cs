namespace GameZone1.Services
{
    public interface IGameService
    {
        IEnumerable<Game> GetAll();
        Task Create(CreateGameFormVM Model);
    }
}
