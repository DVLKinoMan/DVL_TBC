using Microsoft.EntityFrameworkCore;

namespace DVL_TBC.Domain.Models
{
    public class PersonsContext : DbContext
    {
        public PersonsContext(DbContextOptions<PersonsContext> options) : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<RelatedPerson> RelatedPersons { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<RelatedPerson>()
            //    .HasOne(rp => rp.Person1)
            //    .WithMany(p => p.RelatedPersons)
            //    .HasForeignKey(rp => rp.PersonId);
            //modelBuilder.Entity<RelatedPerson>()
            //    .HasOne(rp => rp.Person2)
            //    .WithMany(p => p.RelatedPersons)
            //    .HasForeignKey(rp => rp.RelatedPersonId);

            modelBuilder.Entity<Person>()
                .ToTable("Persons");
                //.HasMany(p => p.RelatedPersons)
                //.WithOne(p => p.Person2)
                //.WillCascadeOnDelete(false);
            modelBuilder.Entity<RelatedPerson>()
                .ToTable("RelatedPersons")
                .HasKey(rp => new {rp.PersonId, rp.RelatedPersonId});
            modelBuilder.Entity<PhoneNumber>().ToTable("PhoneNumbers");
            //modelBuilder.Entity<City>().ToTable("Cities");
        }

    }
}
