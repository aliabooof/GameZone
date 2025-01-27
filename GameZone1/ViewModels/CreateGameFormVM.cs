
using GameZone1.Attributes;
using GameZone1.ViewModels;
using System.Runtime.CompilerServices;

namespace GameZone.ViewModels
{
    public class CreateGameFormVM : GameFormVM
    {

       
        /*[Extension]*/ //not works well with MVC
        [AllowedExtensions(FileSettings.AllowedExtensions)]
        [MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile Cover { get; set; } = default!;
    }
}
