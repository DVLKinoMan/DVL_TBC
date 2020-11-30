using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVL_TBC.Domain.Models
{
    public class PhoneNumber
    {
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(Order = 2)]
        public int PersonId { get; set; }

        [Required]
        [Column(Order = 3)]
        public PhoneNumberType Type { get; set; }

        [Required]
        [Column(Order = 4)]
        [MinLength(4)]
        [MaxLength(50)]
        public string Number { get; set; } = default!;

        //[ForeignKey("PersonId")] public Person Person { get; set; } = default!;
    }
}
