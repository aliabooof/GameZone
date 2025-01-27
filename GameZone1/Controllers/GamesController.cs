using GameZone1.Services;
using GameZone1.ViewModels;
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
        public IActionResult Edit(int id)
        {
            var game = _gameService.GetById(id);
            if (game == null)
            {
                return NotFound();
            }
            EditGameFormVM viewModel = new EditGameFormVM() 
            {
                Id = id,
                Name = game.Name,
                Description = game.Description,
                CategoryId = game.CategoryId,
                SelectedDevices = game.Devices.Select(d=>d.DeviceId).ToList(),
                Categories = _categoriesService.GetSelectList(),
                Devices = _devicesService.GetSelectList(),
                CurrentCover = game.Cover,
            };
            return View(viewModel); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameFormVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoriesService.GetSelectList();

                model.Devices = _devicesService.GetSelectList();
                return View(model);
            }

           var game =  await _gameService.Update(model);

            if (game == null)
                return BadRequest();

            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {  
            var isDealted = _gameService.Delete(id);
            
            return isDealted? Ok():BadRequest();
        }
    
    }



}


