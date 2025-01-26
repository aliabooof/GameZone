

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
        public IEnumerable<Game> GetAll()
              => _context.Games.AsNoTracking().ToList();
    
        public async Task Create(CreateGameFormVM model)
        {
            // {Path.GetFileNameWithoutExtension(game.Cover.FileName)}{Path.GetExtension(game.Cover.FileName)} = {Path.GetFileName(game.Cover.FileName)}
            var coverName = $"{Guid.NewGuid()}{Path.GetFileName(model.Cover.FileName)}";
            var path = Path.Combine(imagePath,coverName);

            using var stream = File.Create(path);
            await model.Cover.CopyToAsync(stream);
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

    }
}
