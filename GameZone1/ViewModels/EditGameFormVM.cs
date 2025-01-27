using GameZone1.Attributes;

namespace GameZone1.ViewModels
{
    public class EditGameFormVM : GameFormVM
    {
        public int Id { get; set; }
        public string? CurrentCover { get; set; }
        /*[Extension]*/ //not works well with MVC
        [AllowedExtensions(FileSettings.AllowedExtensions)]
        [MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile? Cover { get; set; } = default!;
    }
}
