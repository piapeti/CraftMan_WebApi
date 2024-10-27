using CraftMan_WebApi.DataAccessLayer;
namespace CraftMan_WebApi.Models
{
    public class IssueTicket
    {
        public int TicketId { get; set; }
        public string ReportingPerson { get; set; }
        public string ReportingDescription { get; set; }
        public int OperationId { get; set; }
        public string Status { get; set; }
        public string ToCraftmanType { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Pincode { get; set; }
        //  public byte[] ImageData { get; set; }
        public List<IFormFile> ImageData { get; set; }

        public static int InsertTicket(IssueTicket _IssueTicket)
        {
            string qstr = " INSERT INTO [Craftman].[dbo].[tblIssueTicketMaster]     (  ReportingPerson, ReportingDescription, OperationId, Status, ToCraftmanType, Address, City, Pincode) VALUES     ( '" + _IssueTicket.ReportingPerson + "', '" + _IssueTicket.ReportingDescription + "', '" + _IssueTicket.OperationId + "','" + _IssueTicket.Status + "','" + _IssueTicket.ToCraftmanType + "','" + _IssueTicket.Address + "','" + _IssueTicket.City + "','" + _IssueTicket.Pincode + "')" ;

            DBAccess db = new DBAccess();
            //insert multiple imgs here
            return db.ExecuteNonQuery(qstr);
        }
    }
}
