namespace GameZone.Attributes
{
    public class MaxSize:ValidationAttribute
    {
        private readonly int _maxSize;
        public MaxSize(int maxSize)
        {
            _maxSize = maxSize;
        }

        protected override ValidationResult? IsValid
            (object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file is not null)
            {
               
                if (file.Length>_maxSize)
                {
                    return new ValidationResult(errorMessage: $"Maxmum Size Allow {_maxSize} Bytes");
                }
            }
            return ValidationResult.Success;
        }
    }
}
