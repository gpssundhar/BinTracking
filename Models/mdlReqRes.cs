using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SakthiAutomotive.Models
{
    public class mdlReqRes
    {
    }
    public class ddlValues
    {
        public long ddlId { get; set; }
        public string ddlValue { get; set; }
        public string ddlDesc { get; set; }
        public string ddlAddlnDesc { get; set; }
    }


    //********* Login
    public class mdlLogin
    {
        public string UserId { get; set; }
        public string UserPwd { get; set; }
    }

    // ***** Load Menus 
    public class Menus
    {
        public int MenuType { get; set; }
        public int MenuId { get; set; }
    }


    //**************** Page Action      *****************
    public class mdlPageAction_Pg
    {
        public List<ddlValues> ddlRole { get; set; }
    }

    public class mdlPageAction_Grid
    {
        public string MenuType { get; set; }
        public int MenuId { get; set; }
        public string MenuDesc { get; set; }
        public string ChbShow { get; set; }
        public string ChbFill { get; set; }
    }

    public class mdlPgAction_Save
    {
        public long OrgId { get; set; }
        public long BrId { get; set; }
        public string MenuId { get; set; }
        public string PageAction { get; set; }
    }

    //************************      Employees       ****************

    public class mdlEmployee_Grid
    {
        public long EmpId { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }

    }

    public class mdlEmployee_Save
    {
        public long EmpId { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string AppPwd { get; set; }
        public int Status { get; set; }

    }
}