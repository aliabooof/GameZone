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
            return View();
        }

        public IActionResult Create()
        {

            /*if (_dbContext.Categories == null)
                throw new Exception("Categories collection is null.");*/

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
