using System;
using System.Collections.Generic;
using DVL_TBC.PersonsApi.Attributes;
using DVL_TBC.PersonsApi.Models;
using DVL_TBC.PersonsApi.Resources;
using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DVL_TBC.PersonsApi.Filters
{
    public class PersonActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.TryGetValue("editPersonRequest", out var editRequest) &&
                editRequest is EditPersonRequest { } ePerRequest)
            {
                var validationErrors = new List<ValidationException>();

                if (ePerRequest.FirstName is { } firstName)
                {
                    if (!ValidLength(firstName, 2, 50))
                        validationErrors.Add(new ValidationException(string.Format(
                            Translations.ErrorParameterLengthRange,
                            nameof(ePerRequest.FirstName), 2, 50)));

                    if (!GeoOrLatinCharactersAttribute.IsValid(firstName))
                        validationErrors.Add(new ValidationException(
                            $"{Translations.ErrorAllLettersGeoOrLatin} (Param: {nameof(ePerRequest.FirstName)} ParamValue: {firstName})"));
                }

                if (ePerRequest.LastName is { } lastName)
                {
                    if (!ValidLength(lastName, 2, 50))
                        validationErrors.Add(new ValidationException(string.Format(Translations.ErrorParameterLengthRange,
                            nameof(ePerRequest.LastName), 2, 50)));

                    if (!GeoOrLatinCharactersAttribute.IsValid(lastName))
                        validationErrors.Add(new ValidationException(
                            $"{Translations.ErrorAllLettersGeoOrLatin} (Param: {nameof(ePerRequest.LastName)} ParamValue: {lastName})"));
                }

                if (ePerRequest.BirthDate is { } birthDate && !BirthDateAttribute.IsValid(birthDate, 18))
                    validationErrors.Add(new ValidationException(string.Format(Translations.ErrorMinAgeShouldBe, 18)));

                if (ePerRequest.PrivateNumber is { } privateNumber)
                {
                    if (!PrivateNumberAttribute.HasValidLength(privateNumber, 11))
                        validationErrors.Add(new ValidationException(string.Format(Translations.ErrorPrivateNumberLength, 11)));

                    if (!PrivateNumberAttribute.IsValid(privateNumber))
                        validationErrors.Add(new ValidationException(Translations.ErrorPrivateNumberOnlyDigits));
                }

                if (ePerRequest.PhoneNumbers is { } phoneNumbs &&
                    phoneNumbs.Any(numb => !ValidLength(numb.Number, 4, 50)))
                    validationErrors.Add(new ValidationException(Translations.ErrorPhoneNumberRange));

                if (validationErrors.Count != 0)
                    throw new ValidationException("EditPersonRequest Validation failed.",
                        new AggregateException(validationErrors));
            }

            static bool ValidLength(string val, int minLength, int maxLength) =>
                val.Length >= minLength && val.Length <= maxLength;
        }
    }
}
