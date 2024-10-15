namespace BusinessManagementReporting.Core.DTOs.Report.CustomerDemographics
{
    public class CustomerDemographicsDto
    {
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public DateTime Birthdate { get; set; }
        public List<string> Branches { get; set; }
    }
}
