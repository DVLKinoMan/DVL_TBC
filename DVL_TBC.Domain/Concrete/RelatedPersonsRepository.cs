using DVL_TBC.Domain.Abstract;
using DVL_TBC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DVL_TBC.Domain.Concrete
{
    public class RelatedPersonsRepository : IRelatedPersonsRepository
    {
        private readonly PersonsContext _context;

        public RelatedPersonsRepository(PersonsContext context)
        {
            _context = context;
        }

        public async Task AddAsync(RelatedPerson relatedPerson)
        {
            _context.RelatedPersons.Add(relatedPerson);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int personId, int relatedPersonId)
        {
            var relatedPerson = await _context.RelatedPersons.FirstOrDefaultAsync(rp =>
                    (rp.PersonId == personId && rp.RelatedPersonId == relatedPersonId) ||
                    (rp.PersonId == relatedPersonId && rp.RelatedPersonId == personId)) switch
                {
                    { } rp => rp,
                    _ => throw new ArgumentException(
                        $"RelatedPerson was not found with the given {nameof(personId)} and {nameof(relatedPersonId)}")
                };

            _context.RelatedPersons.Remove(relatedPerson);
            await _context.SaveChangesAsync();
        }

        public async Task<List<(string privateNumber, string fullName, PersonConnectionType connectionType, int relatedPersonsCount
            )>> GetRelatedPersonsCountAsync()
        {
            var groupedWithPersonId = await (from p in _context.Persons
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
                }).ToListAsync();

            var groupedWithRelatedPersonId = await (from p in _context.Persons
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
                }).ToListAsync();

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
