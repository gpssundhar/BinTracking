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


    //*********************     Transporter ******************

    public class mdlTransporter_Grid
    {
        public int TransporterId { get; set; }
        public string TransporterCode { get; set; }
        public string TransporterDesc { get; set; }
        public string ContactNo { get; set; }
        public int Status { get; set; }
    }

    public class mdlTransporter_Save
    {
        public int TransporterId { get; set; }
        public string TransporterCode { get; set; }
        public string TransporterDesc { get; set; }
        public string ContactNo { get; set; }
        public int Status { get; set; }
    }


    public class mdlTVehicle
    {
        public long TransporterId { get; set; }
        public string VehicleNo { get; set; }
        public string VehicleType { get; set; }
        public string Remarks { get; set; }
        public long Status { get; set; }
    }

    //*********************     Shifts      ******************

    public class mdlShift_Grid
    {
        public int ShiftId { get; set; }
        public string ShiftCode { get; set; }
        public string ShiftDesc { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public int Status { get; set; }
    }

    public class mdlShift_Save
    {
        public int ShiftId { get; set; }
        public string ShiftCode { get; set; }
        public string ShiftDesc { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public int Status { get; set; }
    }

    public class mdlProduct_Grid
    {
        public int ProductId { get; set; }
        public string ProductDesc { get; set; }
        public string ProductCode { get; set; }
        public int Stock { get; set; }
        public int Status { get; set; }

    }

    public class mdlProduct_Save
    {
        public int ProductId { get; set; }
        public string ProductDesc { get; set; }
        public string ProductCode { get; set; }
        public int Stock { get; set; }
        public int Status { get; set; }

    }

    public class mdlCustomer_Grid
    {
        public long CustId { get; set; }
        public string CustCode { get; set; }
        public string CustDesc { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public int Status { get; set; }
    }

    public class mdlCustomer_Save
    {
        public long CustId { get; set; }
        public string CustCode { get; set; }
        public string CustDesc { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public int Status { get; set; }
    }


    //******************************        Reason Master       ********************

    public class mdlReason_Grid
    {
        public int ReasonId { get; set; }
        public string ReasonDesc { get; set; }
        public int Status { get; set; }
    }

    public class mdlReason_Save
    {
        public int ReasonId { get; set; }
        public string ReasonDesc { get; set; }
        public int Status { get; set; }
    }

    //*****************************     Report      ****************************


    public class mdlRptStockAdj_Pg
    {
        public List<ddlValues> ddlCustomer { get; set; }
    }
    public class mdlStockAdjust_Rpt
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public long CustId { get; set; }
        public int Format { get; set; }
        public int PgIdx { get; set; }
    }
    

    public class mdlStockAdjust_Grid
    {
        public string EmpName { get; set; }
        public string CustDesc { get; set; }
        public string ProductDesc { get; set; }
        public int AdjustQty { get; set; }
        public int Stock { get; set; }
        public string SlNos { get; set; }
        public string ReasonDesc { get; set; }
    }


    public class mdlRptInventory_Pg
    {
        public List<ddlValues> ddlCustomer { get; set; }
    }

    public class mdlRpt_Inventory_Rpt
    {
        public long CustId { get; set; }
        public int Format { get; set; }
        public int PgIdx { get; set; }
    }

    public class mdlInventory_Grid
    {
        public string CustDesc { get; set; }
        public int ProductId { get; set; }
        public string ProductDesc { get; set; }
        public string Barcode { get; set; }
        public int BarcodeQty { get; set; }
    }

    public class mdlInventorySLNo
    {
        public string Barcode { get; set; }
    }
}