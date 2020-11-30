using System.ComponentModel.DataAnnotations;
using System.Linq;
using DVL_TBC.PersonsApi.Resources;

namespace DVL_TBC.PersonsApi.Attributes
{
    public class PrivateNumberAttribute : ValidationAttribute
    {
        private readonly int _length;
        
        public PrivateNumberAttribute(int length)
        {
            _length = length;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext) =>
            value switch
            {
                string { } val when val.Length != _length => new ValidationResult(
                    string.Format(Translations.ErrorPrivateNumberLength, _length)),
                string { } val => val.All(char.IsDigit)
                    ? ValidationResult.Success!
                    : new ValidationResult(Translations.ErrorPrivateNumberOnlyDigits),
                _ => new ValidationResult(Translations.ErrorPrivateNumberString)
            };
    }
}
