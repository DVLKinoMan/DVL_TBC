using DVL_TBC.Domain.Abstract;
using DVL_TBC.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVL_TBC.Domain.Concrete
{
    public class PersonsRepository : IPersonsRepository
    {
        private readonly PersonsContext _context;

        public PersonsRepository(PersonsContext context)
        {
            _context = context;
        }

        public async Task<Person> GetAsync(int personId)
        {
            var person =
                await _context.Persons.Include("PhoneNumbers")
                        .Include("City")
                        .FirstOrDefaultAsync(p => p.Id == personId) switch
                {
                    { } p => p,
                    _ => throw new ArgumentException("Person was not found with the given Id", nameof(personId))
                };

            var related1 = await _context.RelatedPersons
                .Where(rp => rp.PersonId == personId)
                .Select(rp => new { rp.ConnectionType, rp.Person2.PrivateNumber, rp.Person2.FirstName, rp.Person2.LastName })
                .ToListAsync();

            var related2 = await _context.RelatedPersons
                .Where(rp => rp.RelatedPersonId == personId)
                .Select(rp => new { rp.ConnectionType, rp.Person1.PrivateNumber, rp.Person1.FirstName, rp.Person1.LastName })
                .ToListAsync();

            related1.AddRange(related2);
            person.RelatedPersonViewModels = related1.Select(rp =>
                    new RelatedPersonViewModel(rp.ConnectionType, $"{rp.FirstName} {rp.LastName}", rp.PrivateNumber))
                .ToList();

            return person;
        }

        public async Task AddAsync(Person person)
        {
            await _context.Persons.AddAsync(person);

            if (person.RelatedPersons is { } relPer)
                await _context.RelatedPersons.AddRangeAsync(relPer.Select(rp => new RelatedPerson()
                    {ConnectionType = rp.ConnectionType, Person1 = person, RelatedPersonId = rp.RelatedPersonId}));

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int personId) =>
            await DeleteAsync(await _context.Persons.FirstOrDefaultAsync(p => p.Id == personId) switch
            {
                { } p => p,
                _ => throw new ArgumentException("Person was not found with the given Id", nameof(personId))
            });

        private async Task DeleteAsync(Person person)
        {
            _context.RelatedPersons.RemoveRange(_context.RelatedPersons.Where(rp =>
                rp.PersonId == person.Id || rp.RelatedPersonId == person.Id));
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Person>> ListAsync(string? firstName, string? lastName, string? privateNumber, int? itemsPerPage, int? currentPageNumber)
        {
            var query = _context.Persons.Include("PhoneNumbers").Include("City").Where(p => (firstName == null || p.FirstName.Contains(firstName)) &&
                                        (lastName == null || p.LastName.Contains(lastName)) &&
                                        (privateNumber == null || p.PrivateNumber.Contains(privateNumber)));

            if (itemsPerPage is { } itPerPage && currentPageNumber is { } curPage)
                return await query.Skip(itPerPage * (curPage - 1))
                    .Take(itPerPage)
                    .ToListAsync();

            return await query.ToListAsync();
        }

        public async Task<List<Person>> ListAsync(string? firstName, string? lastName, string? privateNumber,
            int? id, Gender? gender, DateTime? birthDate, int? cityId, string? cityName,
            string? profilePictureRelativePath, int? itemsPerPage, int? currentPageNumber)
        {
            var query = _context.Persons.Include("PhoneNumbers").Include("City").Where(p =>
                (firstName == null || p.FirstName == firstName) &&
                (lastName == null || p.LastName == lastName) &&
                (privateNumber == null || p.PrivateNumber == privateNumber) &&
                (id == null || p.Id == id) &&
                (gender == null || p.Gender == gender) &&
                (birthDate == null || p.BirthDate == birthDate) &&
                (cityId == null || p.CityId == cityId) &&
                (cityName == null || p.City!.Name == cityName) &&
                (profilePictureRelativePath == null ||
                 p.ProfilePictureRelativePath == profilePictureRelativePath)
            );

            if (itemsPerPage is { } itPerPage && currentPageNumber is { } curPage)
                return await query.Skip(itPerPage * (curPage - 1))
                    .Take(itPerPage)
                    .ToListAsync();

            return await query.ToListAsync();
        }

        public async Task EditAsync(int personId, string? firstName, string? lastName, Gender? gender, string? privateNumber, DateTime? birthDate,
            int? cityId, List<PhoneNumber>? phoneNumbers)
        {
            var person = await _context.Persons.FirstOrDefaultAsync(p => p.Id == personId) switch
            {
                { } p => p,
                _ => throw new ArgumentException("Person was not found with the given Id", nameof(personId))
            };

            if (firstName != null)
                person.FirstName = firstName;
            if (lastName != null)
                person.LastName = lastName;
            if (gender != null)
                person.Gender = gender;
            if (privateNumber != null)
                person.PrivateNumber = privateNumber;
            if (birthDate is { } date)
                person.BirthDate = date;
            if (cityId != null)
                person.CityId = cityId;
            if (phoneNumbers != null)
                person.PhoneNumbers = phoneNumbers;

            await _context.SaveChangesAsync();
        }

        public async Task ChangeProfilePictureAsync(int personId, string relativePath)
        {
            var person = await _context.Persons.FirstOrDefaultAsync(p => p.Id == personId) switch
            {
                { } p => p,
                _ => throw new ArgumentException("Person was not found with the given Id", nameof(personId))
            };

            person.ProfilePictureRelativePath = relativePath;
            await _context.SaveChangesAsync();
        }
    }
}
