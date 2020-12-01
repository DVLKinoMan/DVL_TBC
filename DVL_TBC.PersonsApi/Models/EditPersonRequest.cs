using DVL_TBC.Domain.Models;
using System;
using System.Collections.Generic;

namespace DVL_TBC.PersonsApi.Models
{
    public class EditPersonRequest
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public Gender? Gender { get; set; }

        public string? PrivateNumber { get; set; }

        public DateTime? BirthDate { get; set; }

        public int? CityId { get; set; }

        public List<PhoneNumberViewModel>? PhoneNumbers { get; set; }
    }
}
