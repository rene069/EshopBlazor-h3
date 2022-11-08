using System.ComponentModel.DataAnnotations;

namespace Eshop.CustomValidation
{
    public class CustomFileExtention : ValidationAttribute
    {

        private string[] _fileExtension = new[] { ".png", ".jpg", ".jpeg", };

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if (value != null)
            {
                IFormFile file = value as IFormFile;
                if (!_fileExtension.Contains(Path.GetExtension(file.FileName)))
                {
                    var result = new ValidationResult("Wrong file type you can only upload .png, .jpg or .jpeg");
                    return result;
                }
            }
            return null;
        }
    }
}
