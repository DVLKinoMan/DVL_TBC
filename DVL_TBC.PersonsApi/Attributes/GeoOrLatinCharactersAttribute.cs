using System.ComponentModel.DataAnnotations;
using System.Linq;
using DVL_TBC.PersonsApi.Resources;

namespace DVL_TBC.PersonsApi.Attributes
{
    public class GeoOrLatinCharactersAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext) =>
            value switch
            {
                string { } val => IsValid(val) ? ValidationResult.Success!
                    : new ValidationResult($"{Translations.ErrorAllLettersGeoOrLatin} Param: {val}"),
                _ => new ValidationResult(Translations.ErrorAllLettersGeoOrLatin)
            };

        public static bool IsValid(string value) => IsAllLatin(value) || IsAllGeo(value);

        private static bool IsAllGeo(string s) => s.All(ch => ch >= 'ა' && ch <= 'ჰ');

        private static bool IsAllLatin(string s) => s.ToLower().All(ch => ch >= 'a' && ch <= 'z');
    }
}
