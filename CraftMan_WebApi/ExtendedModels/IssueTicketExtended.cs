using CraftMan_WebApi.Models;
namespace CraftMan_WebApi.ExtendedModels
{
    public class IssueTicketExtended
    {
        public static Response IssueNewTicket(IssueTicket _IssueTicket)
        {
            Response strReturn = new Response();
            try
            { 
                int i = IssueTicket.InsertTicket(_IssueTicket);
                if (i > 0)
                    {
                        strReturn.StatusCode = 1;
                        strReturn.StatusMessage = "Issue Registered Successfully";
                    }
                    else
                    { strReturn.StatusMessage = "Issue not registered"; }                
            }
            catch (Exception ex) { throw; }
            return strReturn;
        }
    }
}
