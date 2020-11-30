using DVL_TBC.Domain.Abstract;
using DVL_TBC.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DVL_TBC.Domain.Concrete
{
    public class PersonsRepository : IPersonsRepository
    {
        private readonly PersonsContext _context;

        public PersonsRepository(PersonsContext context)
        {
            _context = context;
        }

        public Person Get(int personId)
        {
            var person =
                _context.Persons.Include("PhoneNumbers")
                        .Include("City")
                        .FirstOrDefault(p => p.Id == personId) switch
                {
                    { } p => p,
                    _ => throw new ArgumentException("Person was not found with the given Id", nameof(personId))
                };

            var related1 = _context.RelatedPersons
                .Where(rp => rp.PersonId == personId)
                .Select(rp => new { rp.ConnectionType, rp.Person2.PrivateNumber, rp.Person2.FirstName, rp.Person2.LastName })
                .ToList();

            var related2 = _context.RelatedPersons
                .Where(rp => rp.RelatedPersonId == personId)
                .Select(rp => new { rp.ConnectionType, rp.Person1.PrivateNumber, rp.Person1.FirstName, rp.Person1.LastName })
                .ToList();

            related1.AddRange(related2);
            person.RelatedPersonViewModels = related1.Select(rp =>
                    new RelatedPersonViewModel(rp.ConnectionType, $"{rp.FirstName} {rp.LastName}", rp.PrivateNumber))
                .ToList();

            return person;
        }

        public void Add(Person person)
        {
            _context.Persons.Add(person);

            _context.RelatedPersons.AddRange(person.RelatedPersons.Select(rp => new RelatedPerson()
                {ConnectionType = rp.ConnectionType, Person1 = person, RelatedPersonId = rp.RelatedPersonId}));

            _context.SaveChanges();
        }

        public void Delete(int personId) =>
            Delete(_context.Persons.FirstOrDefault(p => p.Id == personId) switch
            {
                { } p => p,
                _ => throw new ArgumentException("Person was not found with the given Id", nameof(personId))
            });

        private void Delete(Person person)
        {
            _context.RelatedPersons.RemoveRange(_context.RelatedPersons.Where(rp =>
                rp.PersonId == person.Id || rp.RelatedPersonId == person.Id));
            _context.Persons.Remove(person);
            _context.SaveChanges();
        }

        public List<Person> List(string? firstName, string? lastName, string? privateNumber, int? itemsPerPage, int? currentPageNumber)
        {
            var query = _context.Persons.Include("PhoneNumbers").Include("City").Where(p => (firstName == null || p.FirstName.Contains(firstName)) &&
                                        (lastName == null || p.LastName.Contains(lastName)) &&
                                        (privateNumber == null || p.PrivateNumber.Contains(privateNumber)));

            if (itemsPerPage is { } itPerPage && currentPageNumber is { } curPage)
                return query.Skip(itPerPage * (curPage - 1))
                    .Take(itPerPage)
                    .ToList();

            return query.ToList();
        }

        public List<Person> List(string? firstName, string? lastName, string? privateNumber,
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
                return query.Skip(itPerPage * (curPage - 1))
                    .Take(itPerPage)
                    .ToList();

            return query.ToList();
        }

        public void Edit(int personId, string? firstName, string? lastName, Gender? gender, string? privateNumber, DateTime? birthDate,
            int? cityId, List<PhoneNumber>? phoneNumbers)
        {
            var person = _context.Persons.FirstOrDefault(p => p.Id == personId) switch
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
            {
                //todo change phoneNumbers
            }

            _context.SaveChanges();
        }

        public void ChangeProfilePicture(int personId, string relativePath)
        {
            var person = _context.Persons.FirstOrDefault(p => p.Id == personId) switch
            {
                { } p => p,
                _ => throw new ArgumentException("Person was not found with the given Id", nameof(personId))
            };

            person.ProfilePictureRelativePath = relativePath;
            _context.SaveChanges();
        }
    }
}
