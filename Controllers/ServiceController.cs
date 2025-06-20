using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BinTracking.Models;

namespace BinTracking.Controllers
{
    public class ServiceController : Controller
    {

        HomeController ObjCom = new HomeController();
        readonly MasterLogic objMas = new MasterLogic();
        // GET: Service
        public ActionResult Inventory(string ip=null)
        {
            string MTHDNAME = "Inventory";
            try
            {
                int Mnu = 0;
                mdlSrv_Inventory_Pg model = new mdlSrv_Inventory_Pg();

                if (ObjCom.ChkLgnSession(Request.Cookies) != 1 || ip == null)
                    return RedirectToAction(Globals.CNTRLMETHOD_LOGIN, Globals.CONTROLLER_LOGIN);
                Mnu = Convert.ToInt32(ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(ip)).Split('^')[0]);

                ViewBag.Mnu = Mnu;
                ViewBag.PgAction = ObjCom.GetPageAction(Mnu, Request.Cookies);
                if (ViewBag.PgAction[3] != '1')
                    return RedirectToAction(Globals.CNTRLMETHOD_DASHBOARD, Globals.CONTROLLER_DASHBOARD);

                ViewBag.dvCust = Mnu == Globals.MNU_SRV_INVENTORY ? "none" : "block";
                ViewBag.lblHeader = Mnu == Globals.MNU_SRV_INVENTORY ? "Inventory" : "Customer Inventory";

                model.ddlProduct = objMas.GetDataList<ddlValues>("Select ProductId As ddlId,ProductDesc As ddlDesc From Products Where Status=1 order by ProductDesc");
                if (model.ddlProduct == null)
                {
                    objMas.PrintLog(MTHDNAME, objMas.DBErrBuf);
                    ViewBag.ErrorMsg = Globals.SERVER_ERROR;
                }
                
                model.ddlCustomer = objMas.GetDataList<ddlValues>("Select CustId As ddlId,CustDesc As ddlDesc From Customers Where Status=1 order by CustDesc");
                if (model.ddlCustomer == null)
                {
                    objMas.PrintLog(MTHDNAME, objMas.DBErrBuf);
                    ViewBag.ErrorMsg = Globals.SERVER_ERROR;
                }

                return View(model);
            }
            catch (Exception Ex)
            {
                return ObjCom.JsonRspException(MTHDNAME, Ex.Message);
            }
        }

        public ActionResult Inventory_Save(mdlSrv_Inventory_Save data)
        {
            string MTHDNAME = ": Inventory_Save";
            try
            {
                Int64 Ret = 0;

                if (ObjCom.ChkLgnSession(Request.Cookies) != 1)
                    return RedirectToAction(Globals.CNTRLMETHOD_LOGIN, Globals.CONTROLLER_LOGIN);

                Ret = objMas.ExecProcedure(1, "EXEC Inventory_Save " + Globals.MNU_MAS_TRANSPORTER + "," + Request.Cookies.Get(Globals.COOKIE_LGNEMPID).Value + 
                    "," + data.CustId + "," +data.ProductId + "," + data.Quantity + ",'" + data.Remarks + "' ");

                if (Ret == 0)
                    return ObjCom.JsonRspMsg(0, 0, MTHDNAME, 2, Globals.SERVER_ERROR + " " + objMas.DBErrBuf, null);
                else if (Ret == 1 || objMas.DBErrBuf.Length != 0)
                    return ObjCom.JsonRspMsg(1, 0, MTHDNAME, 3, Globals.SAVE_FAILED + " " + objMas.DBErrBuf, "");
                else
                    return ObjCom.JsonRspMsg(1, 1, MTHDNAME, 0, Globals.SAVE_SUCCESS, "");
            }
            catch (Exception Ex)
            {
                return ObjCom.JsonRspException(MTHDNAME, Ex.Message);
            }
        }

        public ActionResult StockAdjust(string ip = null)
        {
            string MTHDNAME = ": StockAdjust";
            try
            {
                int Mnu = 0;
                mdlSrv_Inventory_Pg model = new mdlSrv_Inventory_Pg();

                if (ObjCom.ChkLgnSession(Request.Cookies) != 1 || ip == null)
                    return RedirectToAction(Globals.CNTRLMETHOD_LOGIN, Globals.CONTROLLER_LOGIN);
                Mnu = Convert.ToInt32(ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(ip)).Split('^')[0]);

                ViewBag.Mnu = Mnu;
                ViewBag.PgAction = ObjCom.GetPageAction(Mnu, Request.Cookies);
                if (ViewBag.PgAction[3] != '1')
                    return RedirectToAction(Globals.CNTRLMETHOD_DASHBOARD, Globals.CONTROLLER_DASHBOARD);

                ViewBag.dvCust = Mnu == Globals.MNU_SRV_STOCKADJUST ? "none" : "block";
                ViewBag.lblHeader = Mnu == Globals.MNU_SRV_STOCKADJUST ? "Stock Adjustment" : "Customer Stock Adjustment";

                model.ddlProduct = objMas.GetDataList<ddlValues>("Select ProductId As ddlId,ProductDesc As ddlDesc From Products Where Status=1 order by ProductDesc");
                if (model.ddlProduct == null)
                {
                    objMas.PrintLog(MTHDNAME, objMas.DBErrBuf);
                    ViewBag.ErrorMsg = Globals.SERVER_ERROR;
                }

                model.ddlCustomer = objMas.GetDataList<ddlValues>("Select CustId As ddlId,CustDesc As ddlDesc From Customers Where Status=1 order by CustDesc");
                if (model.ddlCustomer == null)
                {
                    objMas.PrintLog(MTHDNAME, objMas.DBErrBuf);
                    ViewBag.ErrorMsg = Globals.SERVER_ERROR;
                }

                model.ddlReason = objMas.GetDataList<ddlValues>("Select ReasonId As ddlId,ReasonDesc As ddlDesc From Reasons Where Status=1 order by ReasonDesc");
                if (model.ddlReason == null)
                {
                    objMas.PrintLog(MTHDNAME, objMas.DBErrBuf);
                    ViewBag.ErrorMsg = Globals.SERVER_ERROR;
                }

                return View(model);
            }
            catch (Exception Ex)
            {
                return ObjCom.JsonRspException(MTHDNAME, Ex.Message);
            }
        }

        public ActionResult StockBarcode_Grid(mdlSrv_StockAdj_Grid data)
        {
            string MTHDNAME = "StockBarcode_Grid";
            try
            {
                List<mdlSrv_StockAdj_Grid> lstItem = objMas.GetDataList<mdlSrv_StockAdj_Grid>("EXEC StkAdjBarcode_Grid " + data.CustId + "," + data.ProductId);
                if (lstItem == null)
                    return ObjCom.JsonRspMsg(1, 0, MTHDNAME, 1, Globals.SERVER_ERROR, null);

                return ObjCom.JsonRspList(1, MTHDNAME, lstItem.Count(), lstItem);
            }
            catch (Exception Ex)
            {
                return ObjCom.JsonRspException(MTHDNAME + " C[3 : 1,3]", Ex.Message);
            }
        }


        public ActionResult StockAdjust_Save(mdlSrv_StockAdj_Save data)
        {
            string MTHDNAME = "StockAdjust_Save";
            try
            {
                string LgnEmpId;
                int Ret, AdjType;
                ArrayList qryLst = new ArrayList();

                LgnEmpId = "";
                Ret = AdjType = 0;

                if (ObjCom.ChkLgnSession(Request.Cookies) != 1)
                    return RedirectToAction(Globals.CNTRLMETHOD_LOGIN, Globals.CONTROLLER_LOGIN);

                LgnEmpId = Request.Cookies.Get(Globals.COOKIE_LGNEMPID).Value;
                AdjType = data.Mnu == Globals.MNU_SRV_STOCKADJUST ? Globals.ADJTYPE_NORMAL : Globals.ADJTYPE_CUSTOMER;

                if (data.ProductId == 0)
                    return ObjCom.JsonRspMsg(1, 0, MTHDNAME, 3, "Please Slect Valid Product", "");

                // TmpTable --> Idx PgIdx, EmpId LgnUsr, I1 CustId, I2 ProductId, I3 SlNo
                qryLst.Add("delete from TmpTable where Idx=" + data.Mnu + " and EmpId =" + LgnEmpId);
                if (data != null)
                    foreach (var item in data.SlNoList)
                        qryLst.Add("insert into TmpTable (Idx,EmpId,IData1,IData2,IData3) values(" + data.Mnu + "," + LgnEmpId + "," + data.CustId + "," +
                            data.ProductId + "," + item.SlNo + ")");

                Ret = objMas.ExecMultipleQry(qryLst);

                if (Ret == -1 || (qryLst.Count >= 2 && Ret == 0))
                    return ObjCom.JsonRspMsg(1, 0, MTHDNAME, 2, Globals.SERVER_ERROR, null);

                Ret = objMas.ExecProcedure(1, "EXEC StockAdjust_Save " + data.Mnu + ", " + LgnEmpId + "," + data.CustId + "," + data.ProductId + "," +
                    AdjType + "," + data.AdjustQty + "," + data.Stock + ",'" + data.SlNos + "'," + data.ReasonId);
                if (Ret == 0)
                    return ObjCom.JsonRspMsg(0, 0, MTHDNAME, 2, Globals.SERVER_ERROR + " " + objMas.DBErrBuf, null);
                else if (Ret == 1 || objMas.DBErrBuf.Length != 0)
                    return ObjCom.JsonRspMsg(1, 0, MTHDNAME, 3, Globals.SAVE_FAILED + " " + objMas.DBErrBuf, "");
                else
                    return ObjCom.JsonRspMsg(1, 1, MTHDNAME, 0, Globals.SAVE_SUCCESS, "");
            }
            catch (Exception Ex)
            {
                return ObjCom.JsonRspException(MTHDNAME + " C[3 : 1,3]", Ex.Message);
            }
        }
    }
}