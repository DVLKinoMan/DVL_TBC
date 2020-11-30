using System;
using DVL_TBC.Domain.Models;
using System.Collections.Generic;

namespace DVL_TBC.PersonsApi.Models
{
    public class GetPersonResponse : GeneralPersonResponse
    {
        public GetPersonResponse(int id, string firstName, string lastName, Gender? gender, string privateNumber,
            DateTime birthDate, string? cityName, List<PhoneNumberViewModel>? phoneNumbers, int? cityId,
            List<Domain.Models.RelatedPersonViewModel>? relatedPersons, string? profilePictureRelativePath) : base(id, firstName,
            lastName, gender, privateNumber, birthDate, cityName, phoneNumbers)
        {
            CityId = cityId;
            RelatedPersons = relatedPersons;
            ProfilePictureRelativePath = profilePictureRelativePath;
        }

        public int? CityId { get; set; }

        public List<Domain.Models.RelatedPersonViewModel>? RelatedPersons { get; set; }

        public string? ProfilePictureRelativePath { get; set; }

        public byte[]? ProfilePicture { get; set; }
    }
}
