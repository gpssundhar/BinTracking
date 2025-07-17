using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml;
using BinTracking.Models;

namespace BinTracking.Controllers
{
    public class ReportController : Controller
    {
        HomeController ObjCom = new HomeController();
        readonly MasterLogic objMas = new MasterLogic();
        // GET: Report
        public ActionResult RptStockAdjust(string ip = null)
        {
            string MTHDNAME = "RptStockAdjust";
            try
            {
                int Mnu = 0;
                mdlRptStockAdj_Pg model = new mdlRptStockAdj_Pg();
                if (ObjCom.ChkLgnSession(Request.Cookies) != 1 || ip == null)
                    return RedirectToAction(Globals.CNTRLMETHOD_LOGIN, Globals.CONTROLLER_LOGIN);

                Mnu = Convert.ToInt32(ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(ip)).Split('^')[0]);

                ViewBag.Mnu = Mnu;
                ViewBag.PgAction = ObjCom.GetPageAction(Mnu, Request.Cookies);
                if (ViewBag.PgAction[3] != '1')
                    return RedirectToAction(Globals.CNTRLMETHOD_DASHBOARD, Globals.CONTROLLER_DASHBOARD);

                ViewBag.dvCust = Mnu == Globals.MNU_RPT_STOCKADJUST ? "none" : "block";
                ViewBag.grdCust = Mnu == Globals.MNU_RPT_STOCKADJUST ? "false" : "true";
                ViewBag.lblHeader = Mnu == Globals.MNU_RPT_STOCKADJUST ? "Stock Adjustment Report" : "Customer Stock Adjustment Report";

                model.ddlCustomer = objMas.GetDataList<ddlValues>("Select CustId As ddlId,CustDesc As ddlDesc From Customers Where Status=1 order by CustDesc");
                if (model.ddlCustomer == null)
                {
                    objMas.PrintLog(MTHDNAME, objMas.DBErrBuf);
                    ViewBag.ErrorMsg = Globals.SERVER_ERROR;
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return ObjCom.JsonRspException(MTHDNAME, ex.Message);
            }
        }

        private DataTable Rpt_StockAdjust_GetQry(mdlStockAdjust_Rpt data)
        {
            string MTHDNAME = "Rpt_StockAdjust_GetQry";
            try
            {
                DataTable dt = new DataTable();

                return dt = objMas.GetDataTable("EXEC Rpt_StockAdjust " + data.PgIdx + "," + data.CustId + ", '" + data.FromDate + "','" + data.ToDate + "'");
            }
            catch (Exception ex)
            {
                ObjCom.JsonRspException(MTHDNAME, ex.Message);
                return null;
            }
        }

        public ActionResult RptStockAdjust_Grid(mdlStockAdjust_Rpt data)
        {
            string MTHDNAME = "RptStockAdjust_Grid";
            try
            {
                DataTable dt = new DataTable();

                DateTime FromDate = Convert.ToDateTime(data.FromDate);
                DateTime ToDate = Convert.ToDateTime(data.ToDate);
                data.FromDate = FromDate.ToString("dd-MM-yyyy");
                data.ToDate = ToDate.ToString("dd-MM-yyyy");

                if (ObjCom.ChkLgnSession(Request.Cookies) != 1)
                    return RedirectToAction(Globals.CNTRLMETHOD_LOGIN, Globals.CONTROLLER_LOGIN);

                if (objMas.ChkDateRange(MasterLogic.DTRNG_LESS_EQUAL_TODAY, MasterLogic.DTRNG_LESS_EQUAL_TODAY, 1, 1, "From", "To", data.FromDate, data.ToDate) != 1)
                    return ObjCom.JsonRspMsg(1, 0, MTHDNAME, 1, objMas.DBErrBuf, "");

                data.FromDate = FromDate.ToString("yyyy-MM-dd");
                data.ToDate = ToDate.ToString("yyyy-MM-dd");

                dt = Rpt_StockAdjust_GetQry(data);
                if (dt == null)
                    return ObjCom.JsonRspMsg(1, 0, MTHDNAME, 1, "Report Data Fetch Failed", "");

                var lstItem = objMas.ConvertToList<mdlStockAdjust_Grid>(dt);
                if (lstItem == null)
                    return ObjCom.JsonRspMsg(1, 0, MTHDNAME, 2, "Report Data Fetch Failed", "");
                else if (lstItem.Count() == 0)
                    return ObjCom.JsonRspMsg(0, 0, MTHDNAME, 3, "Report Datas Not Found For This Selection ...", "");

                return ObjCom.JsonRspList(0, MTHDNAME, lstItem.Count, lstItem);
            }
            catch (Exception ex)
            {
                return ObjCom.JsonRspException(MTHDNAME, ex.Message);
            }
        }

        public ActionResult RptStockAdjust_Dwnld(mdlStockAdjust_Rpt data)
        {
            string MTHDNAME = "RptStockAdjust_Dwnld";
            try
            {
                if (ObjCom.ChkLgnSession(Request.Cookies) != 1)
                    return RedirectToAction(Globals.CNTRLMETHOD_LOGIN, Globals.CONTROLLER_LOGIN);

                int ret = 0;
                string RptName = "";
                string[] ExlCols = { "Customer", "Product", "Adjusted Qty", "Stock Qty", "Serial Numbers", "Reason", "Employee" };
                DataTable dt = new DataTable();
                DateTime FromDate = Convert.ToDateTime(data.FromDate);
                DateTime ToDate = Convert.ToDateTime(data.ToDate);
                data.FromDate = FromDate.ToString("dd-MM-yyyy");
                data.ToDate = ToDate.ToString("dd-MM-yyyy");

                if (objMas.ChkDateRange(MasterLogic.DTRNG_LESS_EQUAL_TODAY, MasterLogic.DTRNG_LESS_EQUAL_TODAY, 1, 1, "From", "To", data.FromDate, data.ToDate) != 1)
                    return ObjCom.JsonRspMsg(1, 0, MTHDNAME, 1, objMas.DBErrBuf, "");

                data.FromDate = FromDate.ToString("yyyy-MM-dd");
                data.ToDate = ToDate.ToString("yyyy-MM-dd");

                dt = Rpt_StockAdjust_GetQry(data);

                if (dt == null)
                    return ObjCom.JsonRspMsg(1, 0, MTHDNAME, 1, "Report Generate Failed", null);
                else if (dt.Rows.Count == 0)
                    return ObjCom.JsonRspMsg(0, 0, MTHDNAME, 1, "No Data Found For this Selection ", null);
                if (data.PgIdx == Globals.MNU_RPT_STOCKADJUST)
                {
                    dt.Columns.Remove("CustDesc");
                    ExlCols = ExlCols.Where(x => x != "Customer").ToArray();
                }
                dt.Columns.Remove("StkAdjType");

                RptName = data.PgIdx == Globals.MNU_RPT_STOCKADJUST ? "Stock Adjusted Report" : "Customer Stock Adjusted Report";

                int[] DType = Enumerable.Range(0, dt.Columns.Count).Select(n => MasterLogic.RPTSTYLE_STRING_SHORT).ToArray();
                for (ret = 0; ret < ExlCols.Length; ret++)
                    dt.Columns[ret].ColumnName = ExlCols[ret];
                ret = objMas.GenerateReport(data.Format, 0, 1, 1, "Stock Adjust", RptName, DType, null, null, null,
                    objMas.ModifyDateFmt(data.FromDate, MasterLogic.DTFMT_YYMD_TO_DMYY),
                    objMas.ModifyDateFmt(data.ToDate, MasterLogic.DTFMT_YYMD_TO_DMYY), null, null, null, dt);
                if (ret != 1)
                    return ObjCom.JsonRspMsg(0, 0, MTHDNAME, 1, "Report Generate Failed", null);

                return ObjCom.JsonRspMsg(0, 1, MTHDNAME, 0, objMas.DBErrBuf, null);

            }
            catch (Exception Ex)
            {
                return ObjCom.JsonRspException(MTHDNAME, Ex.Message);
            }
        }


        public ActionResult RptInventory(string ip = null)
        {
            string MTHDNAME = "RptInventory";
            try
            {
                int Mnu = 0;
                mdlRptInventory_Pg model = new mdlRptInventory_Pg();
                if (ObjCom.ChkLgnSession(Request.Cookies) != 1 || ip == null)
                    return RedirectToAction(Globals.CNTRLMETHOD_LOGIN, Globals.CONTROLLER_LOGIN);

                Mnu = Convert.ToInt32(ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(ip)).Split('^')[0]);

                ViewBag.Mnu = Mnu;
                ViewBag.PgAction = ObjCom.GetPageAction(Mnu, Request.Cookies);
                if (ViewBag.PgAction[3] != '1')
                    return RedirectToAction(Globals.CNTRLMETHOD_DASHBOARD, Globals.CONTROLLER_DASHBOARD);

                ViewBag.dvCust = Mnu == Globals.MNU_RPT_INVENTORY ? "none" : "block";
                ViewBag.grdCust = Mnu == Globals.MNU_RPT_INVENTORY ? "false" : "true";
                ViewBag.lblHeader = Mnu == Globals.MNU_RPT_INVENTORY ? "Inventory Report" : "Customer Inventory Report";

                model.ddlCustomer = objMas.GetDataList<ddlValues>("Select CustId As ddlId,CustDesc As ddlDesc From Customers Where Status=1 order by CustDesc");
                if (model.ddlCustomer == null)
                {
                    objMas.PrintLog(MTHDNAME, objMas.DBErrBuf);
                    ViewBag.ErrorMsg = Globals.SERVER_ERROR;
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return ObjCom.JsonRspException(MTHDNAME, ex.Message);
            }
        }


        private DataTable Rpt_Inventory_GetQry(mdlRpt_Inventory_Rpt data)
        {
            string MTHDNAME = "Rpt_Inventory_GetQry";
            try
            {
                DataTable dt = new DataTable();

                return dt = objMas.GetDataTable("EXEC Rpt_Inventory " + data.PgIdx + "," + data.CustId);
            }
            catch (Exception ex)
            {
                ObjCom.JsonRspException(MTHDNAME, ex.Message);
                return null;
            }
        }

        public ActionResult RptInventory_Grid(mdlRpt_Inventory_Rpt data)
        {
            string MTHDNAME = "RptInventory_Grid";
            try
            {
                DataTable dt = new DataTable();

                if (ObjCom.ChkLgnSession(Request.Cookies) != 1)
                    return RedirectToAction(Globals.CNTRLMETHOD_LOGIN, Globals.CONTROLLER_LOGIN);

                dt = Rpt_Inventory_GetQry(data);
                if (dt == null)
                    return ObjCom.JsonRspMsg(1, 0, MTHDNAME, 1, "Report Data Fetch Failed", "");

                var lstItem = objMas.ConvertToList<mdlInventory_Grid>(dt);
                if (lstItem == null)
                    return ObjCom.JsonRspMsg(1, 0, MTHDNAME, 2, "Report Data Fetch Failed", "");
                else if (lstItem.Count() == 0)
                    return ObjCom.JsonRspMsg(0, 0, MTHDNAME, 3, "Report Datas Not Found For This Selection ...", "");

                return ObjCom.JsonRspList(0, MTHDNAME, lstItem.Count, lstItem);
            }
            catch (Exception ex)
            {
                return ObjCom.JsonRspException(MTHDNAME, ex.Message);
            }
        }

        public ActionResult RptInventory_Dwnld(mdlRpt_Inventory_Rpt data)
        {
            string MTHDNAME = "RptInventory_Dwnld";
            try
            {
                if (ObjCom.ChkLgnSession(Request.Cookies) != 1)
                    return RedirectToAction(Globals.CNTRLMETHOD_LOGIN, Globals.CONTROLLER_LOGIN);

                int ret = 0;
                string RptName = "";
                string[] ExlCols = { "Customer", "Product", "Stock Qty", "Serial Numbers" };
                DataTable dt = new DataTable();

                dt = Rpt_Inventory_GetQry(data);

                if (dt == null)
                    return ObjCom.JsonRspMsg(1, 0, MTHDNAME, 1, "Report Generate Failed", null);
                else if (dt.Rows.Count == 0)
                    return ObjCom.JsonRspMsg(0, 0, MTHDNAME, 1, "No Data Found For this Selection ", null);
                if (data.PgIdx == Globals.MNU_RPT_INVENTORY)
                {
                    dt.Columns.Remove("CustDesc");
                    ExlCols = ExlCols.Where(x => x != "Customer").ToArray();
                }
                dt.Columns.Remove("ProductId");

                RptName = data.PgIdx == Globals.MNU_RPT_INVENTORY ? "Inventory Report" : "Customer Inventory Report";

                int[] DType = Enumerable.Range(0, dt.Columns.Count).Select(n => MasterLogic.RPTSTYLE_STRING_SHORT).ToArray();
                for (ret = 0; ret < ExlCols.Length; ret++)
                    dt.Columns[ret].ColumnName = ExlCols[ret];
                ret = objMas.GenerateReport(data.Format, 0, 1, 1, "Inventory", RptName, DType, null, null, null,
                    "", "", null, null, null, dt);
                if (ret != 1)
                    return ObjCom.JsonRspMsg(0, 0, MTHDNAME, 1, "Report Generate Failed", null);

                return ObjCom.JsonRspMsg(0, 1, MTHDNAME, 0, objMas.DBErrBuf, null);

            }
            catch (Exception Ex)
            {
                return ObjCom.JsonRspException(MTHDNAME, Ex.Message);
            }
        }


        #region Report - Inward
        public ActionResult RptStockInward()
        {
            string MTHDNAME = "RptStockInward";
            try
            {
                mdlRptStockInward_Pg model = new mdlRptStockInward_Pg();
                if (ObjCom.ChkLgnSession(Request.Cookies) != 1)
                    return RedirectToAction(Globals.CNTRLMETHOD_LOGIN, Globals.CONTROLLER_LOGIN);

                ViewBag.PgAction = ObjCom.GetPageAction(Globals.MNU_RPT_STOCKINWARD, Request.Cookies);
                if (ViewBag.PgAction[3] != '1')
                    return RedirectToAction(Globals.CNTRLMETHOD_DASHBOARD, Globals.CONTROLLER_DASHBOARD);

                model.ddlProduct = objMas.GetDataList<ddlValues>("Select ProductId As ddlId,ProductDesc As ddlDesc From Products Where Status=1 order by ProductDesc");
                if (model.ddlProduct == null)
                {
                    objMas.PrintLog(MTHDNAME, objMas.DBErrBuf);
                    ViewBag.ErrorMsg = Globals.SERVER_ERROR;
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return ObjCom.JsonRspException(MTHDNAME, ex.Message);
            }
        }


        private DataTable Rpt_StockInward_GetQry(mdlRpt_StockInward_Rpt data)
        {
            string MTHDNAME = "Rpt_StockInward_GetQry";
            try
            {
                DataTable dt = new DataTable();

                return dt = objMas.GetDataTable("EXEC Rpt_StockInward '" + data.FromDate + "','" + data.ToDate + "', " + data.ProductId);
            }
            catch (Exception ex)
            {
                ObjCom.JsonRspException(MTHDNAME, ex.Message);
                return null;
            }
        }


        public ActionResult RptStockInward_Grid(mdlRpt_StockInward_Rpt data)
        {
            string MTHDNAME = "RptStockInward_Grid";
            try
            {
                DataTable dt = new DataTable();
                DateTime FromDate = Convert.ToDateTime(data.FromDate);
                DateTime ToDate = Convert.ToDateTime(data.ToDate);
                data.FromDate = FromDate.ToString("dd-MM-yyyy");
                data.ToDate = ToDate.ToString("dd-MM-yyyy");

                if (ObjCom.ChkLgnSession(Request.Cookies) != 1)
                    return RedirectToAction(Globals.CNTRLMETHOD_LOGIN, Globals.CONTROLLER_LOGIN);

                if (objMas.ChkDateRange(MasterLogic.DTRNG_LESS_EQUAL_TODAY, MasterLogic.DTRNG_LESS_EQUAL_TODAY, 1, 1, "From", "To", data.FromDate, data.ToDate) != 1)
                    return ObjCom.JsonRspMsg(1, 0, MTHDNAME, 1, objMas.DBErrBuf, "");

                data.FromDate = FromDate.ToString("yyyy-MM-dd");
                data.ToDate = ToDate.ToString("yyyy-MM-dd");

                dt = Rpt_StockInward_GetQry(data);
                if (dt == null)
                    return ObjCom.JsonRspMsg(1, 0, MTHDNAME, 1, "Report Data Fetch Failed", "");

                var lstItem = objMas.ConvertToList<mdlRptStockInward_Grid>(dt);
                if (lstItem == null)
                    return ObjCom.JsonRspMsg(1, 0, MTHDNAME, 2, "Report Data Fetch Failed", "");
                else if (lstItem.Count() == 0)
                    return ObjCom.JsonRspMsg(0, 0, MTHDNAME, 3, "Report Datas Not Found For This Selection ...", "");

                return ObjCom.JsonRspList(0, MTHDNAME, lstItem.Count, lstItem);
            }
            catch (Exception ex)
            {
                return ObjCom.JsonRspException(MTHDNAME, ex.Message);
            }
        }

        public ActionResult RptStockInward_Dwnld(mdlRpt_StockInward_Rpt data)
        {
            string MTHDNAME = "RptStockInward_Dwnld";
            try
            {
                if (ObjCom.ChkLgnSession(Request.Cookies) != 1)
                    return RedirectToAction(Globals.CNTRLMETHOD_LOGIN, Globals.CONTROLLER_LOGIN);

                int ret = 0;
                string[] ExlCols = { "Date", "Product", "Inward Qty", "From SlNo", "To SlNo", "Employee", "Remarks" };
                DataTable dt = new DataTable();
                DateTime FromDate = Convert.ToDateTime(data.FromDate);
                DateTime ToDate = Convert.ToDateTime(data.ToDate);
                data.FromDate = FromDate.ToString("dd-MM-yyyy");
                data.ToDate = ToDate.ToString("dd-MM-yyyy");

                if (objMas.ChkDateRange(MasterLogic.DTRNG_LESS_EQUAL_TODAY, MasterLogic.DTRNG_LESS_EQUAL_TODAY, 1, 1, "From", "To", data.FromDate, data.ToDate) != 1)
                    return ObjCom.JsonRspMsg(1, 0, MTHDNAME, 1, objMas.DBErrBuf, "");

                data.FromDate = FromDate.ToString("yyyy-MM-dd");
                data.ToDate = ToDate.ToString("yyyy-MM-dd");

                dt = Rpt_StockInward_GetQry(data);

                if (dt == null)
                    return ObjCom.JsonRspMsg(1, 0, MTHDNAME, 1, "Report Generate Failed", null);
                else if (dt.Rows.Count == 0)
                    return ObjCom.JsonRspMsg(0, 0, MTHDNAME, 1, "No Data Found For this Selection ", null);

                int[] DType = Enumerable.Range(0, dt.Columns.Count).Select(n => MasterLogic.RPTSTYLE_STRING_SHORT).ToArray();
                for (ret = 0; ret < ExlCols.Length; ret++)
                    dt.Columns[ret].ColumnName = ExlCols[ret];
                ret = objMas.GenerateReport(data.Format, 0, 1, 1, "Stock Inward", "Stock Inward Report", DType, null, null, null,
                    "", "", null, null, null, dt);
                if (ret != 1)
                    return ObjCom.JsonRspMsg(0, 0, MTHDNAME, 1, "Report Generate Failed", null);

                return ObjCom.JsonRspMsg(0, 1, MTHDNAME, 0, objMas.DBErrBuf, null);

            }
            catch (Exception Ex)
            {
                return ObjCom.JsonRspException(MTHDNAME, Ex.Message);
            }
        }

        #endregion

        public ActionResult RptCheck_InOut(string ip = null)
        {
            string MTHDNAME = "RptCheck_InOut";
            try
            {
                int Mnu = 0;
                mdlRptCheck_InOut_Pg model = new mdlRptCheck_InOut_Pg();
                if (ObjCom.ChkLgnSession(Request.Cookies) != 1 || ip == null)
                    return RedirectToAction(Globals.CNTRLMETHOD_LOGIN, Globals.CONTROLLER_LOGIN);

                Mnu = Convert.ToInt32(ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(ip)).Split('^')[0]);

                ViewBag.Mnu = Mnu;
                ViewBag.PgAction = ObjCom.GetPageAction(Mnu, Request.Cookies);
                if (ViewBag.PgAction[3] != '1')
                    return RedirectToAction(Globals.CNTRLMETHOD_DASHBOARD, Globals.CONTROLLER_DASHBOARD);

                ViewBag.lblHeader = Mnu == Globals.MNU_RPT_CHECKIN ? "Check-In Report" : "Check-Out Report";

                model.ddlProduct = objMas.GetDataList<ddlValues>("Select ProductId As ddlId,ProductDesc As ddlDesc From Products Where Status=1 order by ProductDesc");
                if (model.ddlProduct == null)
                {
                    objMas.PrintLog(MTHDNAME, objMas.DBErrBuf);
                    ViewBag.ErrorMsg = Globals.SERVER_ERROR;
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return ObjCom.JsonRspException(MTHDNAME, ex.Message);
            }
        }


        private DataTable RptCheck_InOut_GetQry(mdlRpt_CheckInOut_Rpt data)
        {
            string MTHDNAME = "RptCheck_InOut_GetQry";
            try
            {
                DataTable dt = new DataTable();
                int TranType = data.PgIdx == Globals.MNU_RPT_CHECKIN ? 1 : 2;

                return dt = objMas.GetDataTable("EXEC Rpt_Check_InOut '" + data.FromDate + "','" + data.ToDate + "', " + data.ProductId + "," + TranType);
            }
            catch (Exception ex)
            {
                ObjCom.JsonRspException(MTHDNAME, ex.Message);
                return null;
            }
        }


        public ActionResult RptCheck_InOut_Grid(mdlRpt_CheckInOut_Rpt data)
        {
            string MTHDNAME = "RptCheck_InOut_Grid";
            try
            {
                DataTable dt = new DataTable();
                DateTime FromDate = Convert.ToDateTime(data.FromDate);
                DateTime ToDate = Convert.ToDateTime(data.ToDate);
                data.FromDate = FromDate.ToString("dd-MM-yyyy");
                data.ToDate = ToDate.ToString("dd-MM-yyyy");

                if (ObjCom.ChkLgnSession(Request.Cookies) != 1)
                    return RedirectToAction(Globals.CNTRLMETHOD_LOGIN, Globals.CONTROLLER_LOGIN);

                if (objMas.ChkDateRange(MasterLogic.DTRNG_LESS_EQUAL_TODAY, MasterLogic.DTRNG_LESS_EQUAL_TODAY, 1, 1, "From", "To", data.FromDate, data.ToDate) != 1)
                    return ObjCom.JsonRspMsg(1, 0, MTHDNAME, 1, objMas.DBErrBuf, "");

                data.FromDate = FromDate.ToString("yyyy-MM-dd");
                data.ToDate = ToDate.ToString("yyyy-MM-dd");

                dt = RptCheck_InOut_GetQry(data);
                if (dt == null)
                    return ObjCom.JsonRspMsg(1, 0, MTHDNAME, 1, "Report Data Fetch Failed", "");

                var lstItem = objMas.ConvertToList<mdlRptCheckInOut_Grid>(dt);
                if (lstItem == null)
                    return ObjCom.JsonRspMsg(1, 0, MTHDNAME, 2, "Report Data Fetch Failed", "");
                else if (lstItem.Count() == 0)
                    return ObjCom.JsonRspMsg(0, 0, MTHDNAME, 3, "Report Datas Not Found For This Selection ...", "");

                return ObjCom.JsonRspList(0, MTHDNAME, lstItem.Count, lstItem);
            }
            catch (Exception ex)
            {
                return ObjCom.JsonRspException(MTHDNAME, ex.Message);
            }
        }

        public ActionResult RptCheck_InOut_Dwnld(mdlRpt_CheckInOut_Rpt data)
        {
            string MTHDNAME = "RptCheck_InOut_Dwnld";
            try
            {
                if (ObjCom.ChkLgnSession(Request.Cookies) != 1)
                    return RedirectToAction(Globals.CNTRLMETHOD_LOGIN, Globals.CONTROLLER_LOGIN);

                string RptName = "";
                int ret = 0;
                string[] ExlCols = { "Date", "Transporter","Vehicle", "Customer","Product", "Qty", "Employee", "Serial Numbers" };
                DataTable dt = new DataTable();
                DateTime FromDate = Convert.ToDateTime(data.FromDate);
                DateTime ToDate = Convert.ToDateTime(data.ToDate);
                data.FromDate = FromDate.ToString("dd-MM-yyyy");
                data.ToDate = ToDate.ToString("dd-MM-yyyy");

                if (objMas.ChkDateRange(MasterLogic.DTRNG_LESS_EQUAL_TODAY, MasterLogic.DTRNG_LESS_EQUAL_TODAY, 1, 1, "From", "To", data.FromDate, data.ToDate) != 1)
                    return ObjCom.JsonRspMsg(1, 0, MTHDNAME, 1, objMas.DBErrBuf, "");

                data.FromDate = FromDate.ToString("yyyy-MM-dd");
                data.ToDate = ToDate.ToString("yyyy-MM-dd");

                dt = RptCheck_InOut_GetQry(data);

                if (dt == null)
                    return ObjCom.JsonRspMsg(1, 0, MTHDNAME, 1, "Report Generate Failed", null);
                else if (dt.Rows.Count == 0)
                    return ObjCom.JsonRspMsg(0, 0, MTHDNAME, 1, "No Data Found For this Selection ", null);

                RptName = data.PgIdx == Globals.MNU_RPT_CHECKIN ? "Check-In Report" : "Check-Out Report";

                int[] DType = Enumerable.Range(0, dt.Columns.Count).Select(n => MasterLogic.RPTSTYLE_STRING_SHORT).ToArray();
                for (ret = 0; ret < ExlCols.Length; ret++)
                    dt.Columns[ret].ColumnName = ExlCols[ret];
                ret = objMas.GenerateReport(data.Format, 0, 1, 1, RptName, RptName, DType, null, null, null,
                    data.FromDate, data.ToDate, null, null, null, dt);
                if (ret != 1)
                    return ObjCom.JsonRspMsg(0, 0, MTHDNAME, 1, "Report Generate Failed", null);

                return ObjCom.JsonRspMsg(0, 1, MTHDNAME, 0, objMas.DBErrBuf, null);

            }
            catch (Exception Ex)
            {
                return ObjCom.JsonRspException(MTHDNAME, Ex.Message);
            }
        }
    }
}