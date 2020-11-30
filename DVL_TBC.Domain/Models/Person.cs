using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVL_TBC.Domain.Models
{
    public class Person
    {
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        [Column(Order = 2)]
        public string FirstName { get; set; } = default!;

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        [Column(Order = 3)]
        public string LastName { get; set; } = default!;

        [Column(Order = 4)]
        public Gender? Gender { get; set; }

        [Required]
        [Column(Order = 5)]
        [MinLength(11)]
        [MaxLength(11)]
        public string PrivateNumber { get; set; } = default!;

        [Required]
        [Column(Order = 6)]
        public DateTime BirthDate { get; set; }

        [Column(Order = 7)]
        public int? CityId { get; set; }

        [Column(Order = 9)]
        public string? ProfilePictureRelativePath { get; set; }

        [ForeignKey("CityId")]
        public City? City { get; set; }

        [NotMapped]
        public List<RelatedPerson>? RelatedPersons { get; set; }

        [NotMapped]
        public List<RelatedPersonViewModel>? RelatedPersonViewModels { get; set; }

        [NotMapped]
        public byte[]? ProfilePicture { get; set; }

        public ICollection<PhoneNumber>? PhoneNumbers { get; set; }
    }
}
