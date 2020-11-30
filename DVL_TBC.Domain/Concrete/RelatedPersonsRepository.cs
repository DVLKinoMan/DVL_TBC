using DVL_TBC.Domain.Abstract;
using DVL_TBC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DVL_TBC.Domain.Concrete
{
    public class RelatedPersonsRepository : IRelatedPersonsRepository
    {
        private readonly PersonsContext _context;

        public RelatedPersonsRepository(PersonsContext context)
        {
            _context = context;
        }

        public void Add(RelatedPerson relatedPerson)
        {
            _context.RelatedPersons.Add(relatedPerson);
            _context.SaveChanges();
        }

        public void Delete(int personId, int relatedPersonId)
        {
            var relatedPerson = _context.RelatedPersons.FirstOrDefault(rp =>
                    (rp.PersonId == personId && rp.RelatedPersonId == relatedPersonId) ||
                    (rp.PersonId == relatedPersonId && rp.RelatedPersonId == personId)) switch
                {
                    { } rp => rp,
                    _ => throw new ArgumentException(
                        $"RelatedPerson was not found with the given {nameof(personId)} and {nameof(relatedPersonId)}")
                };

            _context.RelatedPersons.Remove(relatedPerson);
            _context.SaveChanges();
        }

        public List<(string privateNumber, string fullName, PersonConnectionType connectionType, int relatedPersonsCount
            )> GetRelatedPersonsCount()
        {
            var groupedWithPersonId = (from p in _context.Persons
                join rp in _context.RelatedPersons on p.Id equals rp.PersonId
                group p by new { p.Id, p.FirstName, p.LastName, p.PrivateNumber, rp.ConnectionType }
                into gr
                select new
                {
                    id = gr.Key.Id,
                    firstName = gr.Key.FirstName,
                    lastName = gr.Key.LastName,
                    privateNumber = gr.Key.PrivateNumber,
                    connectionType = gr.Key.ConnectionType,
                    count = gr.Count()
                }).ToList();

            var groupedWithRelatedPersonId = (from p in _context.Persons
                join rp in _context.RelatedPersons on p.Id equals rp.RelatedPersonId
                group p by new { p.Id, p.FirstName, p.LastName, p.PrivateNumber, rp.ConnectionType }
                into gr
                select new
                {
                    id = gr.Key.Id,
                    firstName = gr.Key.FirstName,
                    lastName = gr.Key.LastName,
                    privateNumber = gr.Key.PrivateNumber,
                    connectionType = gr.Key.ConnectionType,
                    count = gr.Count()
                }).ToList();

            return (from p in groupedWithRelatedPersonId.Concat(groupedWithPersonId)
                    group p by new { p.privateNumber, p.firstName, p.lastName, p.connectionType }
                    into gr
                    select
                    (
                        gr.Key.privateNumber,
                        fullName: $"{gr.Key.firstName} {gr.Key.lastName}",
                        gr.Key.connectionType,
                        gr.Count()
                    ))
                .OrderBy(p => p.fullName)
                .ToList();
        }
    }
}
