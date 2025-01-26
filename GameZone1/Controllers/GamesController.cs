using GameZone1.Services;
using System.Runtime.CompilerServices;

namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
     
        private readonly ICategoriesService _categoriesService;
        private readonly IDevicesService _devicesService;
        private readonly IGameService _gameService;

        public GamesController( ICategoriesService categoriesService,IDevicesService devicesService,IGameService gameService)
        {
         
            _categoriesService = categoriesService;
            _devicesService = devicesService;
            _gameService = gameService;
        }
        public IActionResult Index()
        {
            var games = _gameService.GetAll();
            return View(games);
        }

        public IActionResult Details(int id)
        {
            var game = _gameService.GetById(id);
            if (game == null)
            {
                return NotFound();
            }
            return View(game);
        }

        public IActionResult Create()
        {
            CreateGameFormVM viewModel = new CreateGameFormVM()
            {
                Categories = _categoriesService.GetSelectList(),
                Devices = _devicesService.GetSelectList()

            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameFormVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoriesService.GetSelectList();

                model.Devices = _devicesService.GetSelectList();
                return View(model);
            }
    
           await _gameService.Create(model);

         
               
            return RedirectToAction(nameof(Index));
        }
    }
}
