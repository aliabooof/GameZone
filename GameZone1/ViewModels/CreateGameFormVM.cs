
using GameZone1.Attributes;
using System.Runtime.CompilerServices;

namespace GameZone.ViewModels
{
    public class CreateGameFormVM
    {

        [MaxLength(250)]
        public string Name { get; set; } = string.Empty;
        [Display(Name="Category")]
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories{ get; set; } = Enumerable.Empty<SelectListItem>();
        [Display(Name = "Supported Devices")]
        public List<int> SelectedDevices { get; set; } = default!;
        public IEnumerable<SelectListItem> Devices{ get; set; } = Enumerable.Empty<SelectListItem>();    

        [MaxLength(2500)]
        public string Description { get; set; } = string.Empty;
        /*[Extension]*/ //not works well with MVC
        [AllowedExtensions(FileSettings.AllowedExtensions)]
        [MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile Cover { get; set; } = default!;
    }
}
