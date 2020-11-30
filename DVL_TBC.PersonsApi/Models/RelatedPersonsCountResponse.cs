using DVL_TBC.Domain.Models;

namespace DVL_TBC.PersonsApi.Models
{
    public class RelatedPersonsCountResponse
    {
        public string PrivateNumber { get; set; }
        public string FullName { get; set; }
        public PersonConnectionType ConnectionType { get; set; }
        public int RelatedPersonsCount { get; set; }
         
        public RelatedPersonsCountResponse(string privateNumber, string fullName, PersonConnectionType connectionType, int relatedPersonsCount)
        {
            PrivateNumber = privateNumber;
            FullName = fullName;
            ConnectionType = connectionType;
            RelatedPersonsCount = relatedPersonsCount;
        }
    }
}
