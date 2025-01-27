using GameZone1.ViewModels;

namespace GameZone1.Services
{
    public interface IGameService
    {
        IEnumerable<Game> GetAll();
        Game? GetById(int id);
        Task Create(CreateGameFormVM Model);
        Task<Game?> Update(EditGameFormVM model);
        bool Delete (int id);
    }
}
