namespace GameZone1.Services
{
    public interface IGameService
    {
        Task Create(CreateGameFormVM Model);
    }
}
