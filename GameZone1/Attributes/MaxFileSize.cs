namespace GameZone1.Attributes
{
    public class MaxFileSize :ValidationAttribute
    {
        private readonly int _maxFileSize;

        public MaxFileSize(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file is not null)
            {
                if (file.Length > _maxFileSize)
                {
                    
                    return new ValidationResult($"Maximum allowed size {_maxFileSize / (1024 * 1024)} MB");
                }
            }
            return ValidationResult.Success;
        }


    }
}
