using CraftMan_WebApi.Models;

namespace CraftMan_WebApi.Interface
{
    public interface ICompanyMaster
    {
        CompanyMaster GetCompanyDetail(string user);
        int GetTotalcnt(string user);
        Response ValidateCompany(CompanyMaster company);
        int InsertCompany(CompanyMaster company);
        //int UpdateCompany(CompanyMaster company);
} }
