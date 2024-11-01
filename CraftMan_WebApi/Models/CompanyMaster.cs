﻿using CraftMan_WebApi.DataAccessLayer;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Generic;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CraftMan_WebApi.Models
{
    public class CompanyMaster
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
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
       public string CompetenceDescription { get; set; }
        public string CompanyReferences { get; set; }
      
        public string[] JobList { get;   set; }
      
        public Response ValidateCompany(CompanyMaster _Company)
        {
            string qstr = " select Username from dbo.tblCompanyMaster where upper(EmailId) = upper('" + _Company.EmailId + "') and upper(Password)= upper('" + _Company.Password + "')";
            DBAccess db = new DBAccess();
            return db.validate(qstr);
        }
        public static int InsertCompany(CompanyMaster _Company)
        {
            string qstr = " INSERT into dbo.tblCompanyMaster(Username, Password, Active, LocationId, MobileNumber, ContactPerson, EmailId, CreatedOn,joblist)     VALUES('" + _Company.Username + "', '" + _Company.Password + "', '" + _Company.Active + "', '" + _Company.LocationId + "', '" + _Company.MobileNumber + "', '" + _Company.ContactPerson + "', '" + _Company.EmailId + "', getdate(),'"+ string.Join(",", _Company.JobList) + "')";

            int h = 0;
            DBAccess db = new DBAccess();
            int i = db.ExecuteNonQuery(qstr);
           
            return i; 
        }
        public static int UpdateCompany(CompanyMaster _Company)
        {
            string qstr = "UPDATE dbo.tblCompanyMaster " +
                             "SET  "+
                             "   Password = '" + _Company.Password + "',"+
                             "   Active = '" + _Company.Active + "',"+
                             "   LocationId = '" + _Company.LocationId + "',"+
                             "   MobileNumber = '" + _Company.MobileNumber + "',"  +
                             "   ContactPerson = '" + _Company.ContactPerson + "',"  +
                             "   WHERE " +
                             "   EmailId ='" + _Company.EmailId + "'  ";

            DBAccess db = new DBAccess();
            return db.ExecuteNonQuery(qstr);
        }

    }
}