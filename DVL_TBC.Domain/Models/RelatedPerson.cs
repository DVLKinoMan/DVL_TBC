using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVL_TBC.Domain.Models
{
    public class RelatedPerson
    {
        [Required]
        [Column(Order = 1)]
        public int PersonId { get; set; }

        [ForeignKey("PersonId")]
        public Person Person1 { get; set; } = default!;

        [Required]
        [Column(Order = 2)]
        public int RelatedPersonId { get; set; }

        [ForeignKey("RelatedPersonId")]
        public Person Person2 { get; set; } = default!;

        [Required]
        [Column(Order = 3)]
        public PersonConnectionType ConnectionType { get; set; }

    }
}
