using DVL_TBC.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DVL_TBC.PersonsApi.Attributes;

namespace DVL_TBC.PersonsApi.Models
{
    //todo
    public class EditPersonRequest
    {
        [StringLength(50, MinimumLength = 2)]
        [GeoOrLatinCharacters]
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public Gender? Gender { get; set; }

        public string? PrivateNumber { get; set; }

        public DateTime? BirthDate { get; set; }

        public int? CityId { get; set; }

        public List<PhoneNumber>? PhoneNumbers { get; set; }
    }
}
