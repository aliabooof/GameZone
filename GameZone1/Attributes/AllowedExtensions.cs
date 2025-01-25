namespace GameZone1.Attributes
{
    public class AllowedExtensions : ValidationAttribute

    {
        private readonly string _allowedExtensions;

        public AllowedExtensions(string allowedExtensions)
        {
            _allowedExtensions = allowedExtensions;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file is not null)
            {
                var extension = Path.GetExtension(file.FileName);
                var isValid = _allowedExtensions.Split(',').Contains(extension,StringComparer.OrdinalIgnoreCase );

                if (!isValid)
                {
                    return new ValidationResult($"Only {_allowedExtensions} are supported.");
                }
            }


            return ValidationResult.Success;
        }


    }
}
