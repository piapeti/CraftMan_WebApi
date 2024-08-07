namespace CraftMan_WebApi.Models
{
    public class CompanyMaster
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public string UserType { get; set; }
        public int Pkey_UId { get; set; }
        public int LocationId { get; set; }
        public string MobileNumber { get; set; }
        public string ContactPerson { get; set; }
        public string EmailId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string CompanyName { get; set; }
        public string CompanyRegistrationNumber { get; set; }
        public string CompanyPresentation { get; set; }
        public byte[] Logotype { get; set; }
        public string CompetenceDescription { get; set; }
        public string CompanyReferences { get; set; }
    }
}
