using System.Collections.Generic;
using System.Threading.Tasks;
using DVL_TBC.Domain.Models;

namespace DVL_TBC.Domain.Abstract
{
    public interface IRelatedPersonsRepository
    {
        Task AddAsync(RelatedPerson relatedPerson);

        Task DeleteAsync(int personId, int relatedPersonId);

        Task<List<(string privateNumber, string fullName, PersonConnectionType connectionType, int relatedPersonsCount
            )>> GetRelatedPersonsCountAsync();
    }
}
