using System;
using System.ComponentModel.DataAnnotations;
using DVL_TBC.PersonsApi.Resources;

namespace DVL_TBC.PersonsApi.Attributes
{
    public class BirthDateAttribute : ValidationAttribute
    {
        private readonly int _minAge;

        public BirthDateAttribute(int minAge)
        {
            _minAge = minAge;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext) =>
            value switch
            {
                DateTime { } dateTime => IsValid(dateTime, _minAge)
                    ? ValidationResult.Success!
                    : new ValidationResult(string.Format(Translations.ErrorMinAgeShouldBe, _minAge)),
                _ => new ValidationResult(Translations.ErrorBirthDateDateTime)
            };

        public static bool IsValid(DateTime birthDate, int minAge) => CalculateAge(birthDate) >= minAge;

        private static int CalculateAge(DateTime birthDate) =>
            (int.Parse(DateTime.Now.ToString("yyyyMMdd")) - int.Parse(birthDate.ToString("yyyyMMdd"))) / 10_000;
    }
}
