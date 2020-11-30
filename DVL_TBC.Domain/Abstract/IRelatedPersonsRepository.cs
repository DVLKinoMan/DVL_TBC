using System.Collections.Generic;
using DVL_TBC.Domain.Models;

namespace DVL_TBC.Domain.Abstract
{
    public interface IRelatedPersonsRepository
    {
        void Add(RelatedPerson relatedPerson);

        void Delete(int personId, int relatedPersonId);

        List<(string privateNumber, string fullName, PersonConnectionType connectionType, int relatedPersonsCount
            )> GetRelatedPersonsCount();
    }
}
