namespace DVL_TBC.Domain.Models
{
    public class RelatedPersonViewModel
    {
        public PersonConnectionType ConnectionType { get; set; }
        public string FullName { get; set; }
        public string PrivateNumber { get; set; }

        public RelatedPersonViewModel(PersonConnectionType connectionType, string fullName, string privateNumber)
        {
            ConnectionType = connectionType;
            FullName = fullName;
            PrivateNumber = privateNumber;
        }

    }
}
