using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BinTracking.Models;

namespace BinTracking.Controllers
{
    public class HomeController : Controller
    {
        const string PAGEDESC = "Login";
        public string DBErrBuf = ""; 
        readonly MasterLogic objMas = new MasterLogic();

        #region Json 
        public ActionResult JsonRspException(string PgDesc, string Msg)
        {
            try
            {
                objMas.PrintLog(PgDesc, "Exception : " + Msg);
                return Json(new ResponseHelper { Success = 0, Message = Globals.SERVER_ERROR, Data = null });
            }
            catch (Exception ex)
            {
                objMas.PrintLog(PgDesc, "C[0]: " + ex.Message);
                return Json(new ResponseHelper { Success = 0, Message = Globals.SERVER_ERROR, Data = null });
            }
        }


        public ActionResult JsonRspMsg(int PrtLog, int Status, string PgDesc, int ErrNo, string Msg, Object RspData)
        {
            try
            {
                if (Status != 0)
                {
                    objMas.PrintLog(PgDesc, Msg);
                    return Json(new ResponseHelper { Success = Status, Message = Msg, Data = RspData }, JsonRequestBehavior.AllowGet);
                }

                objMas.PrintLog(PgDesc, "[" + ErrNo + "]: " + Msg);
                return Json(new ResponseHelper { Success = 0, Message = Msg, Data = RspData }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                objMas.PrintLog(PgDesc, "C[0]: " + ex.Message);
                return Json(new ResponseHelper { Success = 0, Message = Globals.SERVER_ERROR, Data = RspData }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult JsonRspList(int PrtLog, string PgDesc, int RowCnt, Object RspData)
        {
            try
            {
                objMas.PrintLog(PgDesc, "Rows Count [" + RowCnt + "]");
                return Json(RspData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                objMas.PrintLog(PgDesc, "C[0]: " + ex.Message);
                return Json(new ResponseHelper { Success = 0, Message = Globals.SERVER_ERROR, Data = RspData }); ;
            }
        }


        public ActionResult JsonDDLRspList(int PrtLog, string PgDesc, int RowCnt, Object RspData)
        {
            try
            {
                objMas.PrintLog(PgDesc, "Rows Count [" + RowCnt + "]");
                return Json(new ResponseHelper { Success = 1, Message = "", Data = RspData }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                objMas.PrintLog(PgDesc, "C[0]: " + ex.Message);
                return Json(new ResponseHelper { Success = 0, Message = Globals.SERVER_ERROR, Data = RspData }); ;
            }
        }

        #endregion


        public ActionResult Index()
        {
            objMas.PrintLog("***************    App Version", Globals.APPVER + "    ******************");
            Response.Cookies[Globals.COOKIE_LGNSESSION].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies[Globals.COOKIE_LGNEMPID].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies[Globals.COOKIE_LGNEMPDESC].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies[Globals.COOKIE_LGNUSERID].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies[Globals.COOKIE_LGNEMAIL].Expires = DateTime.Now.AddDays(-1);
            return View();
        }


        [HttpPost]
        [Route("/Home/Login")]
        public ActionResult Login(mdlLogin data)
        {
            string MTHDNAME = "Login";
            try
            {
                int ret, status;
                string sessionId = "";
                DataTable dt = new DataTable();

                ret = status = 0;

                dt = objMas.GetDataTable("Select EmpId, EmpName, Email, [Status] from Employees Where (Email = '" +
                    data.UserId + "' or Mobile = '" + data.UserId + "') and AppPwd = '" + data.UserPwd + "' ");
                if (dt == null)
                    return JsonRspMsg(1, 0, MTHDNAME, 1, Globals.FETCH_FAILED, null);
                else if (dt.Rows.Count == 0)
                    return JsonRspMsg(1, 0, MTHDNAME, 1, "User Not Found", null);
                else if (dt.Rows.Count != 1)
                    return JsonRspMsg(1, 0, MTHDNAME, 1, "Multiple User available. \n Contact System admin.", null);
                
                status = Convert.ToInt32(dt.Rows[0]["Status"]);
                if (status != Globals.STATUS_ACTIVE && status != Globals.STATUS_SUPERADMIN)
                    return JsonRspMsg(1, 0, MTHDNAME, 1, "User - Status Inactive. \n Contact System admin.", null);

                sessionId = Session.SessionID;
                if (sessionId == null || sessionId.Length == 0)
                    return JsonRspMsg(1, 0, MTHDNAME, 1, "Session generate failed. \n Contact System admin.", null);

                ret = objMas.ExecQry("Update Employees set SessionId = '" + sessionId + "' Where (Email = '" + data.UserId +
                    "' or Mobile = '" + data.UserId + "') and AppPwd = '" + data.UserPwd + "'");
                if (ret == -1)
                    return JsonRspMsg(1, 0, MTHDNAME, 1, Globals.SERVER_ERROR, null);

                // user (role SelBranch = 1) --> 1 - Br selection  else 0.  NTS --> 2  org/Br.
                Response.Cookies.Add(new HttpCookie(Globals.COOKIE_LGNSESSION, sessionId));
                Response.Cookies.Add(new HttpCookie(Globals.COOKIE_DEFAULTSESSION, sessionId));
                Response.Cookies.Add(new HttpCookie(Globals.COOKIE_LGNEMPID, dt.Rows[0]["EmpId"].ToString()));
                Response.Cookies.Add(new HttpCookie(Globals.COOKIE_LGNEMPDESC, dt.Rows[0]["EmpName"].ToString()));
                Response.Cookies.Add(new HttpCookie(Globals.COOKIE_LGNEMAIL, dt.Rows[0]["Email"].ToString()));
                Response.Cookies.Add(new HttpCookie(Globals.COOKIE_LGNUSERID, data.UserId));
                return JsonRspMsg(1, 1, MTHDNAME, 1, Globals.LOGIN_SUCCESS, null);
            }
            catch (Exception Ex)
            {
                return JsonRspException(PAGEDESC, Ex.Message);
            }
        }


        #region Load Menus

        [HttpGet]
        [Route("Home/LoadMenus")]
        public ActionResult LoadMenus()
        {
            string PAGEDESC = "LoadMenus";
            try
            {
                string LgnUsr, EmpId, buf;
                DataTable dt = new DataTable();
                List<Menus> AppMnus = new List<Menus>();

                LgnUsr = EmpId = buf = "";


                if (Request.Cookies.Get(Globals.COOKIE_LGNSESSION) == null || Request.Cookies.Get(Globals.COOKIE_LGNSESSION).Value.Length == 0 ||
                        Request.Cookies.Get(Globals.COOKIE_LGNUSERID) == null || Request.Cookies.Get(Globals.COOKIE_LGNEMPID) == null ||
                        Request.Cookies.Get(Globals.COOKIE_LGNUSERID).Value.Length == 0 || Request.Cookies.Get(Globals.COOKIE_LGNEMPID).Value.Length == 0)
                    return RedirectToAction(Globals.CNTRLMETHOD_LOGIN, Globals.CONTROLLER_LOGIN);

                if (ChkLgnSession(Request.Cookies) != 1)
                    return RedirectToAction(Globals.CNTRLMETHOD_LOGIN, Globals.CONTROLLER_LOGIN);

                LgnUsr = Request.Cookies.Get(Globals.COOKIE_LGNUSERID).Value;
                EmpId = Request.Cookies.Get(Globals.COOKIE_LGNEMPID).Value;

                if (EmpId.Length == 0 || EmpId == "0")
                    return RedirectToAction("Index", Globals.CONTROLLER_LOGIN);

                if (EmpId.Length == 0 || LgnUsr.Length == 0)
                {
                    objMas.PrintLog(PAGEDESC, "[2] Menu Mapping Fetch Failed");
                    return RedirectToAction("Index", Globals.CONTROLLER_LOGIN);
                }

                if (LgnUsr.CompareTo(Globals.SUPERADMIN_ID) == 0)
                {
                    buf = "select MenuType,MenuId from AppMenus where Status=1 order by MenuType,MenuId";
                }
                else
                {
                    buf = "select am.MenuType,am.MenuId from AppMenus am,MenuMapping mm,Employees em where am.Status=1 and am.MenuId=mm.MenuId and " +
                            " SUBSTRING(mm.PgAction,4,1)='1' and mm.EmpId = em.EmpId and mm.EmpId = " + EmpId;
                }

                AppMnus = objMas.GetDataList<Menus>(buf);
                if (AppMnus == null)
                    return JsonRspMsg(1, 0, PAGEDESC, 1, "Menus Fetch Failed", AppMnus);

                return JsonRspMsg(0, 1, PAGEDESC, 0, "", AppMnus);
            }
            catch (Exception Ex)
            {
                return JsonRspException(PAGEDESC, Ex.Message);
            }
        }

        #endregion


        #region Session

        public int ChkLgnSession(HttpCookieCollection hccLgn)
        {
            try
            {
                string session = "";
                Int64 Ret = 0;

                if (hccLgn.Get(Globals.COOKIE_LGNSESSION) == null || hccLgn.Get(Globals.COOKIE_LGNEMPID) == null || hccLgn.Get(Globals.COOKIE_DEFAULTSESSION) == null)
                    return 0;

                Ret = objMas.FetchSingleColumn("Select SessionId from Employees Where EmpId = " + hccLgn.Get(Globals.COOKIE_LGNEMPID).Value, ref session);

                if (Ret == -1)
                {
                    objMas.PrintLog("ChkLgnSession", objMas.DBErrBuf);
                    return 0;
                }
                else if (session.CompareTo(hccLgn.Get(Globals.COOKIE_DEFAULTSESSION).Value.ToString()) != 0)
                    return 0;

                return 1;
            }
            catch (Exception ex)
            {
                objMas.PrintLog("ChkLgnSession", ex.Message);
                return 0;
            }
        }



        public string GetPageAction(int MenuId, HttpCookieCollection hccLgn)
        {
            // Return --> PageAction --> 0000  [Add, Edit, Delete, View]
            DBErrBuf = "";
            try
            {
                string buf, tmp, EmpId, UserId;
                int ret = 0;
                buf = tmp = EmpId = UserId = "";

                EmpId = hccLgn.Get(Globals.COOKIE_LGNEMPID).Value;
                UserId = hccLgn.Get(Globals.COOKIE_LGNUSERID).Value;

                if (EmpId.Length == 0 || UserId.Length == 0)
                    return "0000";

                if (UserId.ToUpper() == Globals.SUPERADMIN_ID.ToUpper())
                    buf = "select PgAction from AppMenus where MenuId=" + MenuId;
                else
                    buf = "select PgAction from MenuMapping mm,Employees em where mm.MenuId=" + MenuId + " and mm.EmpId=em.EmpId and mm.EmpId = " + EmpId;

                ret = objMas.FetchSingleColumn(buf, ref tmp);
                if (ret == -1)
                    return "0000";

                return tmp;
            }
            catch (Exception Ex)
            {
                DBErrBuf = "MC[49]: " + Ex.Message;
                objMas.PrintLog("GetPageAction", DBErrBuf);
                return "0000";
            }
        }
        #endregion
    }
}