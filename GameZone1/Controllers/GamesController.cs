




namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
     
        private readonly ICategoriesService _categoriesService;
        private readonly IDevicesService _devicesService;

        public GamesController( ICategoriesService categoriesService,IDevicesService devicesService)
        {
         
            _categoriesService = categoriesService;
            _devicesService = devicesService;
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
        public IActionResult Create(CreateGameFormVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoriesService.GetSelectList();

                model.Devices = _devicesService.GetSelectList();
                return View(model);
            }
            //save game to database


            //save cover to server
               
            return RedirectToAction(nameof(Index));
        }
    }
}
