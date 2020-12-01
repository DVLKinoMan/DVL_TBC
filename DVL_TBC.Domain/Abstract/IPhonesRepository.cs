using System.Threading.Tasks;
using DVL_TBC.Domain.Models;

namespace DVL_TBC.Domain.Abstract
{
    public interface IPhonesRepository
    {
        Task AddAsync(PhoneNumber phoneNumber);

        Task DeleteAsync(int personId, string number);
    }
}
