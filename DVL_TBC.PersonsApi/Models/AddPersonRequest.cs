using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DVL_TBC.Domain.Models;
using DVL_TBC.PersonsApi.Attributes;

namespace DVL_TBC.PersonsApi.Models
{
    public class AddPersonRequest
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        [GeoOrLatinCharacters]
        public string FirstName { get; set; } = default!;

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [GeoOrLatinCharacters]
        public string LastName { get; set; } = default!;

        public Gender? Gender { get; set; }

        [Required]
        [PrivateNumber(11)]
        public string PrivateNumber { get; set; } = default!;

        [Required] 
        [BirthDate(18)]
        public DateTime BirthDate { get; set; }

        public int? CityId { get; set; }

        public List<PhoneNumberViewModel>? PhoneNumbers { get; set; }

        public List<RelatedPersonForAddRequest>? RelatedPersons { get; set; }
    }
}
