using DVL_TBC.Domain.Models;

namespace DVL_TBC.PersonsApi.Models
{
    public class RelatedPersonForAddRequest
    {
        public PersonConnectionType ConnectionType { get; set; }

        public int RelatedPersonId { get; set; }
    }
}
