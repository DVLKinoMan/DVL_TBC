using System.ComponentModel.DataAnnotations;
using DVL_TBC.Domain.Models;

namespace DVL_TBC.PersonsApi.Models
{
    public class PhoneNumberViewModel
    {
        public PhoneNumberType Type { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Phone number length should be between 4 and 50", MinimumLength = 4)]
        public string Number { get; set; } = default!;
    }
}
