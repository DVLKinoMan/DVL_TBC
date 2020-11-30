using DVL_TBC.Domain.Models;
using DVL_TBC.PersonsApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace DVL_TBC.PersonsApi.Extensions
{
    public static class Converters
    {
        public static Person ToPerson(this AddPersonRequest addPersonRequest) =>
            new Person()
            {
                FirstName = addPersonRequest.FirstName,
                LastName = addPersonRequest.LastName,
                Gender = addPersonRequest.Gender,
                PrivateNumber = addPersonRequest.PrivateNumber,
                BirthDate = addPersonRequest.BirthDate,
                CityId = addPersonRequest.CityId,
                PhoneNumbers = addPersonRequest.PhoneNumbers?.ToPhoneNumbers().ToList(),
                RelatedPersons = addPersonRequest.RelatedPersons?.ToRelatedPersons().ToList()
            };

        public static GetPersonResponse ToGetPersonResponse(this Person person) =>
            new GetPersonResponse(person.Id,
                person.FirstName,
                person.LastName,
                person.Gender,
                person.PrivateNumber,
                person.BirthDate,
                person.City?.Name,
                person.PhoneNumbers?.ToPhoneNumbersViewModel().ToList(),
                person.CityId,
                person.RelatedPersonViewModels,
                person.ProfilePictureRelativePath);

        #region RelatedPerson

        public static IEnumerable<RelatedPerson> ToRelatedPersons(
            this IEnumerable<RelatedPersonForAddRequest> addRequestRelatedPersons) =>
            addRequestRelatedPersons.Select(rp => rp.ToRelatedPerson());

        public static RelatedPerson ToRelatedPerson(this RelatedPersonForAddRequest addRequestRelatedPerson) =>
            new RelatedPerson()
            {
                ConnectionType = addRequestRelatedPerson.ConnectionType,
                RelatedPersonId = addRequestRelatedPerson.RelatedPersonId
            };

        #endregion

        #region GeneralPersonResponse

        public static IEnumerable<GeneralPersonResponse> ToGeneralPersonResponses(this IEnumerable<Person> persons) =>
            persons.Select(p => p.ToGeneralPersonResponse());

        public static GeneralPersonResponse ToGeneralPersonResponse(this Person person) =>
            new GeneralPersonResponse(person.Id,
                person.FirstName,
                person.LastName,
                person.Gender,
                person.PrivateNumber,
                person.BirthDate,
                person.City?.Name,
                person.PhoneNumbers?.ToPhoneNumbersViewModel().ToList());

        #endregion

        #region PhoneNumberViewModel

        public static IEnumerable<PhoneNumberViewModel> ToPhoneNumbersViewModel(
            this IEnumerable<PhoneNumber> phoneNumbers) =>
            phoneNumbers.Select(pn => pn.ToPhoneNumberViewModel());

        public static PhoneNumberViewModel ToPhoneNumberViewModel(this PhoneNumber phoneNumber) =>
            new PhoneNumberViewModel()
            {
                Number = phoneNumber.Number,
                Type = phoneNumber.Type
            };

        #endregion

        #region PhoneNumber

        public static IEnumerable<PhoneNumber> ToPhoneNumbers(
            this IEnumerable<PhoneNumberViewModel> phoneNumberViewModels) =>
            phoneNumberViewModels.Select(phoneNumberViewModel => phoneNumberViewModel.ToPhoneNumber());

        public static PhoneNumber ToPhoneNumber(this PhoneNumberViewModel phoneNumberViewModel) =>
            new PhoneNumber()
            {
                Number = phoneNumberViewModel.Number,
                Type = phoneNumberViewModel.Type
            };

        #endregion
    }
}
