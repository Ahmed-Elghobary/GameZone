namespace GameZone.Attributes
{
    public class AllowedExtension:ValidationAttribute
    {
        private readonly string _allowdExtensions;
        public AllowedExtension( string allowedExtensions)
        {
            _allowdExtensions = allowedExtensions;
        }

        protected override ValidationResult? IsValid
            (object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if(file is not null ) 
            { 
                var Extension= Path.GetExtension(file.FileName);
                var ISallowed = _allowdExtensions.Split(',').Contains(Extension,StringComparer.OrdinalIgnoreCase);

                if (!ISallowed)
                {
                    return new ValidationResult(errorMessage: $" Only {_allowdExtensions} Are Allow!");
                }
            }
            return ValidationResult.Success;
        }
    }
}
