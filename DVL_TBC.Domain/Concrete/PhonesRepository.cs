using DVL_TBC.Domain.Abstract;
using DVL_TBC.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using DVL_TBC.Domain.Resources;

namespace DVL_TBC.Domain.Concrete
{
    public class PhonesRepository : IPhonesRepository
    {
        private readonly PersonsContext _context;

        public PhonesRepository(PersonsContext context)
        {
            _context = context;
        }

        public async Task AddAsync(PhoneNumber phoneNumber)
        {
            await _context.AddAsync(phoneNumber);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int personId, string number)
        {
            var phoneNumb =
                await _context.PhoneNumbers.FirstOrDefaultAsync(ph => personId == ph.PersonId && ph.Number == number) switch
                {
                    { } p => p,
                    _ => throw new ArgumentException(Translations.ErrorPhoneNumberNotFound)
                };

            _context.PhoneNumbers.Remove(phoneNumb);
            await _context.SaveChangesAsync();
        }
    }
}
