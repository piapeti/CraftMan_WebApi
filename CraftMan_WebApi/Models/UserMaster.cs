namespace CraftMan_WebApi.Models
{
    public class UserMaster
    {
        
            public string Username { get; set; }
            public string Password { get; set; }
            public string Active { get; set; }
            public string UserType { get; set; }
            public int Pkey_UId { get; set; } // Primary key
            public int? LocationId { get; set; } // Nullable integer
            public string MobileNumber { get; set; }
            public string ContactPerson { get; set; }
            public string EmailId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public void call() {

            Console.WriteLine("callmethod");
        }
        private void call1()
        {

            Console.WriteLine("call1method");
        }

    }
}
