

using GameZone1.ViewModels;

namespace GameZone1.Services
{
    public class GameService : IGameService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string imagePath;

        public GameService(ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            imagePath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagesPath}";
        }
        private async Task<string> SaveCover(IFormFile Cover)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetFileName(Cover.FileName)}";
            var path = Path.Combine(imagePath, coverName);

            using var stream = File.Create(path);
            await Cover.CopyToAsync(stream);
            return coverName;
        }

        public bool Delete(int id)
        {
            var isDeleted = false ;

            var game = _context.Games.Find(id);
            if (game is null)
                return isDeleted;
            _context.Games.Remove(game);
            var effectedRows = _context.SaveChanges();
            if(effectedRows > 0)
            {
                isDeleted = true;
                var cover = Path.Combine(imagePath, game.Cover);
                File.Delete(cover);
            }

            return isDeleted;

        }
        public IEnumerable<Game> GetAll()
              => _context.Games
                .Include(g =>g.Category)
                .Include(g=>g.Devices)
                .ThenInclude(d=>d.Device)
                .AsNoTracking()
                .ToList();
    
        public async Task Create(CreateGameFormVM model)
        {
            // {Path.GetFileNameWithoutExtension(game.Cover.FileName)}{Path.GetExtension(game.Cover.FileName)} = {Path.GetFileName(game.Cover.FileName)}
            var coverName = await SaveCover(model.Cover);
            /*stream.Dispose();*/ // no need cuz using will dispose automatically
            Game game = new Game()
            {
                Name = model.Name,
                Description = model.Description,
                CategoryId = model.CategoryId,
                Cover =coverName,
                Devices = model.SelectedDevices.Select(d=> new GameDevice { DeviceId = d}).ToList()
            };

            _context.Add(game);
            _context.SaveChanges();
        }

        public Game? GetById(int id)
        => _context.Games
                .Include(g => g.Category)
                .Include(g => g.Devices)
                .ThenInclude(d => d.Device)
                .AsNoTracking()
                .SingleOrDefault(g=>g.Id == id);

        public async Task<Game?> Update(EditGameFormVM model)
        {
            var game = _context.Games
                .Include(g=> g.Devices)
                .SingleOrDefault(g=>g.Id==model.Id);
            if (game == null)
                return null;
            var hasNewCover = model.Cover is not null;
            var oldCover = game.Cover;

            game.Name = model.Name;
            game.Description = model.Description;
            game.CategoryId = model.CategoryId;
            game.Devices = model.SelectedDevices.Select(d=> new GameDevice { DeviceId=d}).ToList();
           
            
            if(hasNewCover)
            {
                game.Cover = await SaveCover(model.Cover!);
            }

            var effectedRows = _context.SaveChanges();
            if (effectedRows > 0)
            {
                if (hasNewCover)
                {
                    var cover = Path.Combine(imagePath, oldCover);
                    File.Delete(cover);
                }
                return game;
            }
            else
            {
                var cover = Path.Combine(imagePath, game.Cover);
                File.Delete(cover);
                return null;
            }
          
        }

     
    }
}
