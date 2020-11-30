using DVL_TBC.Domain.Models;
using System;
using System.Collections.Generic;

namespace DVL_TBC.PersonsApi.Models
{
    public class GeneralPersonResponse
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender? Gender { get; set; }

        public string PrivateNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public string? CityName { get; set; }

        public List<PhoneNumberViewModel>? PhoneNumbers { get; set; }

        public GeneralPersonResponse(int id, string firstName, string lastName, Gender? gender, string privateNumber,
            DateTime birthDate, string? cityName, List<PhoneNumberViewModel>? phoneNumbers)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            PrivateNumber = privateNumber;
            BirthDate = birthDate;
            CityName = cityName;
            PhoneNumbers = phoneNumbers;
        }
    }
}
