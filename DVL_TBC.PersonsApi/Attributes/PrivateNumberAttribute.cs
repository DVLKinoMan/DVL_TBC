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
                string { } val when !HasValidLength(val, _length) => new ValidationResult(
                    string.Format(Translations.ErrorPrivateNumberLength, _length)),
                string { } val => IsValid(val) ? ValidationResult.Success!
                    : new ValidationResult(Translations.ErrorPrivateNumberOnlyDigits),
                _ => new ValidationResult(Translations.ErrorPrivateNumberString)
            };

        public static bool IsValid(string val) => val.All(char.IsDigit);

        public static bool HasValidLength(string val, int length) => val.Length == length;
    }
}
