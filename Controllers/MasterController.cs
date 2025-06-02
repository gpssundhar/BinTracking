using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using SakthiAutomotive.Models;

namespace SakthiAutomotive.Controllers
{
    public class MasterController : Controller
    {
        HomeController ObjCom = new HomeController();
        readonly MasterLogic objMas = new MasterLogic();
        // GET: Master
        public ActionResult Employees()
        {
            if (ObjCom.ChkLgnSession(Request.Cookies) != 1)
                return RedirectToAction(Globals.CNTRLMETHOD_LOGIN, Globals.CONTROLLER_LOGIN);

            ViewBag.PgAction = ObjCom.GetPageAction(Globals.MNU_MAS_EMPLOYEES, Request.Cookies);
            if (ViewBag.PgAction[3] != '1')
                return RedirectToAction(Globals.CNTRLMETHOD_DASHBOARD, Globals.CONTROLLER_DASHBOARD);

            return View();
        }

        [HttpGet]
        [Route("Master/Employee_Grid")]
        public ActionResult Employee_Grid(int Status)
        {
            string MTHDNAME = "Employee_Grid";
            try
            {
                if (ObjCom.ChkLgnSession(Request.Cookies) != 1)
                    return RedirectToAction(Globals.CNTRLMETHOD_LOGIN, Globals.CONTROLLER_LOGIN);

                List<mdlEmployee_Grid> lstEmp = objMas.GetDataList<mdlEmployee_Grid>("EXEC Employee_Grid " + Status);
                if (lstEmp == null)
                    return ObjCom.JsonRspMsg(1, 0, MTHDNAME, 1, Globals.SERVER_ERROR, null);

                return ObjCom.JsonRspList(1, MTHDNAME, lstEmp.Count(), lstEmp);
            }
            catch (Exception Ex)
            {
                return ObjCom.JsonRspException(MTHDNAME + " C[3 : 1,3]", Ex.Message);
            }
        }


        [HttpPost]
        [Route("Master/Employee_Save")]
        public ActionResult Employee_Save(mdlEmployee_Save data)
        {
            string MTHDNAME = ": Employee_Save";
            try
            {
                Int64 Ret = 0;
                DataTable dt = new DataTable();

                if (ObjCom.ChkLgnSession(Request.Cookies) != 1)
                    return RedirectToAction(Globals.CNTRLMETHOD_LOGIN, Globals.CONTROLLER_LOGIN);

                if (data.AppPwd == null)
                    data.AppPwd = "";

                string[] ipdata = { data.EmpCode, data.EmpName, data.Email, data.Mobile, data.AppPwd };
                if (objMas.Text_ChkInputs(new int[] { 0, 0, 0, 0, 1 }, ref ipdata, new string[] { "Employee Code", "Employee Name", "Email", "Mobile", "Password" },
                        new int[] { MasterLogic.TXTIP_ALPHA_NOS_SPL_ANY, MasterLogic.TXTIP_ALPHA_NOS_SPL_ANY, MasterLogic.TXTIP_EMAIL, MasterLogic.TXTIP_NOS, 
                            data.EmpId == 0 ? MasterLogic.TXTIP_ALPHA_NOS_SPL_ANY : 0}, new int[] { 0, 0, 0, 10, 0 }, new int[] {0, 0, 0, 10, 0 },
                        new int[] { 0, 0, 0, 0, 0 }) != 1)
                    return ObjCom.JsonRspMsg(1, 0, MTHDNAME, 7, objMas.DBErrBuf, null);

                data.EmpCode = ipdata[0];data.EmpName = ipdata[1]; data.Email = ipdata[2]; data.Mobile = ipdata[3]; data.AppPwd = ipdata[4];

                Ret = objMas.ExecProcedure(1, "EXEC Employee_Save " + Globals.MNU_MAS_EMPLOYEES + "," + data.EmpId + ",'" + data.EmpCode + "','" +
                    data.EmpName + "','" + data.Mobile + "','" + data.Email + "','" + data.AppPwd + "'," + data.Status + ", " +
                    Request.Cookies.Get(Globals.COOKIE_LGNEMPID).Value);

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


        [HttpPost]
        [Route("Master/Employee_Delete")]
        public ActionResult Employee_Delete(long EmpId)
        {
            string MTHDNAME = "Employee_Delete";
            try
            {
                Int64 Ret = 0;

                if (ObjCom.ChkLgnSession(Request.Cookies) != 1)
                    return RedirectToAction(Globals.CNTRLMETHOD_LOGIN, Globals.CONTROLLER_LOGIN);

                Ret = objMas.ExecProcedure(0, "EXEC Employee_Delete " + Globals.MNU_MAS_EMPLOYEES + "," + EmpId + ", " + 
                    Request.Cookies.Get(Globals.COOKIE_LGNEMPID).Value);
                if (Ret <= 0)
                    return ObjCom.JsonRspMsg(1, 0, MTHDNAME, 5, Ret == -1 ? Globals.SERVER_ERROR : objMas.DBErrBuf, null);

                return ObjCom.JsonRspMsg(0, 1, MTHDNAME, 0, "Delete Success", null);
            }
            catch (Exception Ex)
            {
                return ObjCom.JsonRspException(MTHDNAME, Ex.Message);
            }
        }
        
        public ActionResult Transporter()
        {
            return View();
        }
        
        public ActionResult Shifts()
        {
            return View();

        }
        public ActionResult Products()
        {
            return View();

        }
        public ActionResult Customers()
        {
            return View();

        }
        public ActionResult Reason()
        {
            return View();

        }
        public ActionResult PageAction(string ip=null)
        {
            string MTHDNAME = "PageAction";
            try
            {
                int Mnu = 0;
                mdlPageAction_Pg model = new mdlPageAction_Pg();

                if (ObjCom.ChkLgnSession(Request.Cookies) != 1 || ip == null)
                    return RedirectToAction(Globals.CNTRLMETHOD_LOGIN, Globals.CONTROLLER_LOGIN);

                Mnu = Convert.ToInt32(ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(ip)).Split('^')[0]);

                ViewBag.Mnu = Mnu;
                ViewBag.PgAction = ObjCom.GetPageAction(Mnu, Request.Cookies);
                if (ViewBag.PgAction[3] != '1')
                    return RedirectToAction(Globals.CNTRLMETHOD_DASHBOARD, Globals.CONTROLLER_DASHBOARD);

                model.ddlRole = objMas.GetDataList<ddlValues>("Select EmpId As ddlId,EmpName As ddlDesc From Employees Where Status=1 order by EmpName");
                if (model.ddlRole == null)
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

        [HttpGet]
        [Route("Master/PageAction_Grid")]
        public ActionResult PageAction_Grid(long? Emp)
        {
            string MTHDNAME = "PageAction_Grid";
            try
            {
                if (ObjCom.ChkLgnSession(Request.Cookies) != 1 || Emp == 0)
                    return ObjCom.JsonRspMsg(1, 3, MTHDNAME, 0, Globals.SESSION_TIMEOUT, null);

                List<mdlPageAction_Grid> Actions = new List<mdlPageAction_Grid>();

                Actions = objMas.GetDataList<mdlPageAction_Grid>("EXEC PageAction_Get " + Emp);
                if (Actions == null)
                    return ObjCom.JsonRspMsg(1, 0, MTHDNAME, 1, "Data Fetch Failed", null);

                return ObjCom.JsonRspList(0, MTHDNAME, Actions.Count(), Actions);
            }
            catch (Exception Ex)
            {
                return ObjCom.JsonRspException(MTHDNAME, Ex.Message);
            }
        }


        [HttpPost]
        [Route("Master/PageAction_Save")]
        public ActionResult PageAction_Save(decimal? SelId, int? Mnu, List<mdlPgAction_Save> data)
        {
            string MTHDNAME = "PageAction_Save";
            try
            {
                Int64 Ret = 0;
                string Error;
                string LgnEmpId;
                ArrayList qryLst = new ArrayList();

                Error = "";
                LgnEmpId = "";
                qryLst.Clear();

                if (ObjCom.ChkLgnSession(Request.Cookies) != 1)
                    return ObjCom.JsonRspMsg(1, 3, MTHDNAME, 0, Globals.SESSION_TIMEOUT, null);

                LgnEmpId = Request.Cookies.Get(Globals.COOKIE_LGNEMPID).Value;


                // TmpTable --> Idx PgIdx, EmpId LgnUsr, I1 MenuId, V1 PageAction
                qryLst.Add("delete from TmpTable where Idx=" + Globals.MNU_MAS_PAGEACTION + " and EmpId =" + LgnEmpId);
                if (data != null)
                    foreach (var item in data)
                        qryLst.Add("insert into TmpTable (Idx,EmpId,IData1,VData1) values(" + Mnu + "," + LgnEmpId + "," + item.MenuId + ",'" + item.PageAction + "')");

                Ret = objMas.ExecMultipleQry(qryLst);

                if (Ret == -1 || (qryLst.Count >= 2 && Ret == 0))
                    return ObjCom.JsonRspMsg(1, 0, MTHDNAME, 2, Globals.SERVER_ERROR, null);

                Ret = objMas.ExecProcedure("EXEC PageAction_Save " + Mnu + ", " + SelId + "," + LgnEmpId + ", @ErrorMsg OUTPUT", ref Error);
                if (Ret >= 1 || (Ret == 0 && Error == "1"))
                    return ObjCom.JsonRspMsg(0, 1, MTHDNAME, 1, Globals.SAVE_SUCCESS, null);
                else if (Ret == 0)
                    return ObjCom.JsonRspMsg(1, 1, MTHDNAME, 3, Globals.SAVE_FAILED, null);
                else
                    return ObjCom.JsonRspMsg(1, 0, MTHDNAME, 4, Globals.SERVER_ERROR, null);
            }
            catch (Exception Ex)
            {
                return ObjCom.JsonRspException(MTHDNAME, Ex.Message);
            }
        }
    }
}