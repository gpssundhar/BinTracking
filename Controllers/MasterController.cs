using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BinTracking.Models;

namespace BinTracking.Controllers
{
    public class MasterController : Controller
    {
        HomeController ObjCom = new HomeController();
        readonly MasterLogic objMas = new MasterLogic();
        
        
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
            string MTHDNAME = "Transporter";
            try
            {
                if (ObjCom.ChkLgnSession(Request.Cookies) != 1)
                    return RedirectToAction(Globals.CNTRLMETHOD_LOGIN, Globals.CONTROLLER_LOGIN);

                ViewBag.PgAction = ObjCom.GetPageAction(Globals.MNU_MAS_TRANSPORTER, Request.Cookies);
                if (ViewBag.PgAction[3] != '1')
                    return RedirectToAction(Globals.CNTRLMETHOD_DASHBOARD, Globals.CONTROLLER_DASHBOARD);

                if (ObjCom.ChkLgnSession(Request.Cookies) != 1)
                    return RedirectToAction(Globals.CNTRLMETHOD_LOGIN, Globals.CONTROLLER_LOGIN);
                return View();
            }
            catch (Exception Ex)
            {
                return ObjCom.JsonRspException(MTHDNAME + " C[3 : 1,3]", Ex.Message);
            }
        }


        public ActionResult Transporter_Grid(int Status)
        {
            string MTHDNAME = "Transporter_Grid";
            try
            {
                List<mdlTransporter_Grid> lstItem = objMas.GetDataList<mdlTransporter_Grid>("EXEC Transporter_Grid " + Status);
                if (lstItem == null)
                    return ObjCom.JsonRspMsg(1, 0, MTHDNAME, 1, Globals.SERVER_ERROR, null);

                return ObjCom.JsonRspList(1, MTHDNAME, lstItem.Count(), lstItem);
            }
            catch (Exception Ex)
            {
                return ObjCom.JsonRspException(MTHDNAME + " C[3 : 1,3]", Ex.Message);
            }
        }


        public ActionResult Transporter_Save(mdlTransporter_Save data)
        {
            string MTHDNAME = ": Transporter_Save";
            try
            {
                Int64 Ret = 0;
                DataTable dt = new DataTable();

                if (ObjCom.ChkLgnSession(Request.Cookies) != 1)
                    return RedirectToAction(Globals.CNTRLMETHOD_LOGIN, Globals.CONTROLLER_LOGIN);

                string[] ipdata = { data.TransporterCode, data.TransporterDesc, data.ContactNo };
                if (objMas.Text_ChkInputs(new int[] { 0, 0, 0 }, ref ipdata, new string[] { "Transporter Code", "Transporter Name", "Contact Number" },
                        new int[] { MasterLogic.TXTIP_ALPHA_NOS_SPL_ANY, MasterLogic.TXTIP_ALPHA_NOS_SPL_ANY, MasterLogic.TXTIP_MOBILE },
                        new int[] { 0, 0, 10 }, new int[] { 0, 0, 10 },
                        new int[] { 0, 0, 0 }) != 1)
                    return ObjCom.JsonRspMsg(1, 0, MTHDNAME, 7, objMas.DBErrBuf, null);

                data.TransporterCode = ipdata[0]; data.TransporterDesc = ipdata[1]; data.ContactNo = ipdata[2];

                Ret = objMas.ExecProcedure(1, "EXEC Transporter_Save " + Globals.MNU_MAS_TRANSPORTER + "," + data.TransporterId + ",'" + data.TransporterCode + "','" +
                    data.TransporterDesc + "','" + data.ContactNo + "'," + data.Status + ", " + Request.Cookies.Get(Globals.COOKIE_LGNEMPID).Value);

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


        public ActionResult Transporter_Delete(long Id)
        {
            string MTHDNAME = "Transporter_Delete";
            try
            {
                Int64 Ret = 0;

                if (ObjCom.ChkLgnSession(Request.Cookies) != 1)
                    return RedirectToAction(Globals.CNTRLMETHOD_LOGIN, Globals.CONTROLLER_LOGIN);

                Ret = objMas.ExecProcedure(0, "EXEC Transporter_Delete " + Globals.MNU_MAS_TRANSPORTER + "," + Id + ", " +
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


        public ActionResult TVehicle_Get(int TId)
        {
            string MTHDNAME = "TVehicle_Get";
            try
            {
                List<mdlTVehicle> lstItem = objMas.GetDataList<mdlTVehicle>("EXEC TVehicle_Grid " + Globals.MNU_MAS_TRANSPORTER + "," +
                    Request.Cookies.Get(Globals.COOKIE_LGNEMPID).Value + "," + TId);
                if (lstItem == null)
                    return ObjCom.JsonRspMsg(1, 0, MTHDNAME, 1, Globals.SERVER_ERROR, null);

                return ObjCom.JsonRspList(1, MTHDNAME, lstItem.Count(), lstItem);
            }
            catch (Exception Ex)
            {
                return ObjCom.JsonRspException(MTHDNAME + " C[3 : 1,3]", Ex.Message);
            }
        }


        public ActionResult Vehicle_Save(mdlTVehicle data)
        {
            string MTHDNAME = ": Vehicle_Save";
            try
            {
                if (ObjCom.ChkLgnSession(Request.Cookies) != 1)
                    return RedirectToAction(Globals.CNTRLMETHOD_LOGIN, Globals.CONTROLLER_LOGIN);

                string[] ipdata = { data.VehicleNo, data.VehicleType, data.Remarks };
                if (objMas.Text_ChkInputs(new int[] { 0, 0, 1 }, ref ipdata, new string[] { "Vehicle Number", "Vehicle Type", "Remarks" },
                        new int[] { MasterLogic.TXTIP_ALPHA_NOS_SPL_ANY, MasterLogic.TXTIP_ALPHA_NOS_SPL_ANY, 0 },
                        new int[] { 0, 0, 0 }, new int[] { 0, 0, 0 },
                        new int[] { 0, 0, 0 }) != 1)
                    return ObjCom.JsonRspMsg(1, 0, MTHDNAME, 7, objMas.DBErrBuf, null);

                data.VehicleNo = ipdata[0]; data.VehicleType = ipdata[1]; data.Remarks = ipdata[2];

                List<mdlTVehicle> lstItem = objMas.GetDataList<mdlTVehicle>("EXEC TVehicle_Save " + Globals.MNU_MAS_TRANSPORTER + "," +
                    data.TransporterId + ",'" + data.VehicleNo + "','" + data.VehicleType + "','" + data.Remarks + "'," + data.Status + ", " +
                    Request.Cookies.Get(Globals.COOKIE_LGNEMPID).Value);

                if (lstItem == null)
                    return ObjCom.JsonRspMsg(0, 0, MTHDNAME, 2, Globals.SERVER_ERROR + " " + objMas.DBErrBuf, null);
                
                return ObjCom.JsonRspList(1, MTHDNAME, lstItem.Count(), lstItem);
            }
            catch (Exception Ex)
            {
                return ObjCom.JsonRspException(MTHDNAME, Ex.Message);
            }
        }


        public ActionResult Vehicle_Delete(int Id, string Vehicle)
        {
            string MTHDNAME = ": Vehicle_Delete";
            try
            {
                if (ObjCom.ChkLgnSession(Request.Cookies) != 1)
                    return RedirectToAction(Globals.CNTRLMETHOD_LOGIN, Globals.CONTROLLER_LOGIN);

                List<mdlTVehicle> lstItem = objMas.GetDataList<mdlTVehicle>("EXEC TVehicle_Delete " + Globals.MNU_MAS_TRANSPORTER + "," +
                    Id + ",'" + Vehicle + "', " + Request.Cookies.Get(Globals.COOKIE_LGNEMPID).Value);

                if (lstItem == null)
                    return ObjCom.JsonRspMsg(0, 0, MTHDNAME, 2, Globals.SERVER_ERROR + " " + objMas.DBErrBuf, null);

                return ObjCom.JsonRspList(1, MTHDNAME, lstItem.Count(), lstItem);
            }
            catch (Exception Ex)
            {
                return ObjCom.JsonRspException(MTHDNAME, Ex.Message);
            }
        }


        public ActionResult Shifts()
        {
            string MTHDNAME = "Shifts"; 
            try
            {
                if (ObjCom.ChkLgnSession(Request.Cookies) != 1)
                    return RedirectToAction(Globals.CNTRLMETHOD_LOGIN, Globals.CONTROLLER_LOGIN);

                ViewBag.PgAction = ObjCom.GetPageAction(Globals.MNU_MAS_SHIFTS, Request.Cookies);
                if (ViewBag.PgAction[3] != '1')
                    return RedirectToAction(Globals.CNTRLMETHOD_DASHBOARD, Globals.CONTROLLER_DASHBOARD);

                if (ObjCom.ChkLgnSession(Request.Cookies) != 1)
                    return RedirectToAction(Globals.CNTRLMETHOD_LOGIN, Globals.CONTROLLER_LOGIN);
                return View();
            }
            catch (Exception Ex)
            {
                return ObjCom.JsonRspException(MTHDNAME + " C[3 : 1,3]", Ex.Message);
            }
        }


        public ActionResult Shift_Grid(int Status)
        {
            string MTHDNAME = "Shift_Grid";
            try
            {
                List<mdlShift_Grid> lstItem = objMas.GetDataList<mdlShift_Grid>("EXEC Shift_Grid " + Status);
                if (lstItem == null)
                    return ObjCom.JsonRspMsg(1, 0, MTHDNAME, 1, Globals.SERVER_ERROR, null);

                return ObjCom.JsonRspList(1, MTHDNAME, lstItem.Count(), lstItem);
            }
            catch (Exception Ex)
            {
                return ObjCom.JsonRspException(MTHDNAME + " C[3 : 1,3]", Ex.Message);
            }
        }


        [HttpPost]
        [Route("Master/Shift_Save")]
        public ActionResult Shift_Save(mdlShift_Save data)
        {
            string MTHDNAME = ": Shift_Save";
            try
            {
                Int64 Ret = 0;
                DataTable dt = new DataTable();

                if (ObjCom.ChkLgnSession(Request.Cookies) != 1)
                    return RedirectToAction(Globals.CNTRLMETHOD_LOGIN, Globals.CONTROLLER_LOGIN);

                string[] ipdata = { data.ShiftCode, data.ShiftDesc, data.FromTime, data.ToTime };
                if (objMas.Text_ChkInputs(new int[] { 0, 0, 0, 0 }, ref ipdata, new string[] { "Shift Code", "Shift Name", "From Time", "To Time" },
                        new int[] { MasterLogic.TXTIP_ALPHA_NOS_SPL_ANY, MasterLogic.TXTIP_ALPHA_NOS_SPL_ANY, 0, 0}, 
                        new int[] { 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0 },
                        new int[] { 0, 0, 0, 0 }) != 1)
                    return ObjCom.JsonRspMsg(1, 0, MTHDNAME, 7, objMas.DBErrBuf, null);

                data.ShiftCode = ipdata[0]; data.ShiftDesc = ipdata[1]; data.FromTime = ipdata[2]; data.ToTime = ipdata[3];

                Ret = objMas.ExecProcedure(1, "EXEC Shift_Save " + Globals.MNU_MAS_SHIFTS + "," + data.ShiftId + ",'" + data.ShiftCode + "','" +
                    data.ShiftDesc + "','" + data.FromTime + "','" + data.ToTime + "'," + data.Status + ", " +
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
        [Route("Master/Shift_Delete")]
        public ActionResult Shift_Delete(long Id)
        {
            string MTHDNAME = "Shift_Delete";
            try
            {
                Int64 Ret = 0;

                if (ObjCom.ChkLgnSession(Request.Cookies) != 1)
                    return RedirectToAction(Globals.CNTRLMETHOD_LOGIN, Globals.CONTROLLER_LOGIN);

                Ret = objMas.ExecProcedure(0, "EXEC Shift_Delete " + Globals.MNU_MAS_SHIFTS + "," + Id + ", " +
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


        public ActionResult Products()
        {
            string MTHDNAME = "Products";
            try
            {
                if (ObjCom.ChkLgnSession(Request.Cookies) != 1)
                    return RedirectToAction(Globals.CNTRLMETHOD_LOGIN, Globals.CONTROLLER_LOGIN);

                ViewBag.PgAction = ObjCom.GetPageAction(Globals.MNU_MAS_PRODUCTS, Request.Cookies);
                if (ViewBag.PgAction[3] != '1')
                    return RedirectToAction(Globals.CNTRLMETHOD_DASHBOARD, Globals.CONTROLLER_DASHBOARD);

                if (ObjCom.ChkLgnSession(Request.Cookies) != 1)
                    return RedirectToAction(Globals.CNTRLMETHOD_LOGIN, Globals.CONTROLLER_LOGIN);
                return View();
            }
            catch (Exception Ex)
            {
                return ObjCom.JsonRspException(MTHDNAME + " C[3 : 1,3]", Ex.Message);
            }
        }


        public ActionResult Product_Grid(int Status)
        {
            string MTHDNAME = "Product_Grid";
            try
            {
                List<mdlProduct_Grid> lstItem = objMas.GetDataList<mdlProduct_Grid>("EXEC Product_Grid " + Status);
                if (lstItem == null)
                    return ObjCom.JsonRspMsg(1, 0, MTHDNAME, 1, Globals.SERVER_ERROR, null);

                return ObjCom.JsonRspList(1, MTHDNAME, lstItem.Count(), lstItem);
            }
            catch (Exception Ex)
            {
                return ObjCom.JsonRspException(MTHDNAME + " C[3 : 1,3]", Ex.Message);
            }
        }


        [HttpPost]
        [Route("Master/Product_Save")]
        public ActionResult Product_Save(mdlProduct_Save data)
        {
            string MTHDNAME = ": Product_Save";
            try
            {
                Int64 Ret = 0;
                DataTable dt = new DataTable();

                if (ObjCom.ChkLgnSession(Request.Cookies) != 1)
                    return RedirectToAction(Globals.CNTRLMETHOD_LOGIN, Globals.CONTROLLER_LOGIN);

                string[] ipdata = { data.ProductCode, data.ProductDesc};
                if (objMas.Text_ChkInputs(new int[] { 0, 0 }, ref ipdata, new string[] { "Product Code", "Product Name" },
                        new int[] { MasterLogic.TXTIP_ALPHA_NOS_SPL_ANY, MasterLogic.TXTIP_ALPHA_NOS_SPL_ANY },
                        new int[] { 0, 0 }, new int[] { 0, 0 },
                        new int[] { 0, 0 }) != 1)
                    return ObjCom.JsonRspMsg(1, 0, MTHDNAME, 7, objMas.DBErrBuf, null);

                data.ProductCode = ipdata[0]; data.ProductDesc = ipdata[1];

                Ret = objMas.ExecProcedure(1, "EXEC Product_Save " + Globals.MNU_MAS_PRODUCTS + "," + data.ProductId + ",'" + data.ProductCode + "','" +
                    data.ProductDesc + "'," + data.Stock + "," + data.Status + ", " + Request.Cookies.Get(Globals.COOKIE_LGNEMPID).Value);

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
        [Route("Master/Product_Delete")]
        public ActionResult Product_Delete(long Id)
        {
            string MTHDNAME = "Product_Delete";
            try
            {
                Int64 Ret = 0;

                if (ObjCom.ChkLgnSession(Request.Cookies) != 1)
                    return RedirectToAction(Globals.CNTRLMETHOD_LOGIN, Globals.CONTROLLER_LOGIN);

                Ret = objMas.ExecProcedure(0, "EXEC Product_Delete " + Globals.MNU_MAS_PRODUCTS + "," + Id + ", " +
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


        public ActionResult Customers()
        {
            string MTHDNAME = "Customers";
            try
            {
                if (ObjCom.ChkLgnSession(Request.Cookies) != 1)
                    return RedirectToAction(Globals.CNTRLMETHOD_LOGIN, Globals.CONTROLLER_LOGIN);

                ViewBag.PgAction = ObjCom.GetPageAction(Globals.MNU_MAS_CUSTOMERS, Request.Cookies);
                if (ViewBag.PgAction[3] != '1')
                    return RedirectToAction(Globals.CNTRLMETHOD_DASHBOARD, Globals.CONTROLLER_DASHBOARD);

                if (ObjCom.ChkLgnSession(Request.Cookies) != 1)
                    return RedirectToAction(Globals.CNTRLMETHOD_LOGIN, Globals.CONTROLLER_LOGIN);
                return View();
            }
            catch (Exception Ex)
            {
                return ObjCom.JsonRspException(MTHDNAME + " C[3 : 1,3]", Ex.Message);
            }
        }


        public ActionResult Customer_Grid(int Status)
        {
            string MTHDNAME = "Customer_Grid";
            try
            {
                List<mdlCustomer_Grid> lstItem = objMas.GetDataList<mdlCustomer_Grid>("EXEC Customer_Grid " + Status);
                if (lstItem == null)
                    return ObjCom.JsonRspMsg(1, 0, MTHDNAME, 1, Globals.SERVER_ERROR, null);

                return ObjCom.JsonRspList(1, MTHDNAME, lstItem.Count(), lstItem);
            }
            catch (Exception Ex)
            {
                return ObjCom.JsonRspException(MTHDNAME + " C[3 : 1,3]", Ex.Message);
            }
        }


        [HttpPost]
        [Route("Master/Customer_Save")]
        public ActionResult Customer_Save(mdlCustomer_Save data)
        {
            string MTHDNAME = ": Customer_Save";
            try
            {
                Int64 Ret = 0;
                DataTable dt = new DataTable();

                if (ObjCom.ChkLgnSession(Request.Cookies) != 1)
                    return RedirectToAction(Globals.CNTRLMETHOD_LOGIN, Globals.CONTROLLER_LOGIN);

                string[] ipdata = { data.CustCode, data.CustDesc, data.Email, data.ContactNo };
                if (objMas.Text_ChkInputs(new int[] { 0, 0, 0, 0 }, ref ipdata, new string[] { "Customer Code", "Customer Name", "Email", "Contact Number" },
                        new int[] { MasterLogic.TXTIP_ALPHA_NOS_SPL_ANY, MasterLogic.TXTIP_ALPHA_NOS_SPL_ANY, MasterLogic.TXTIP_EMAIL, MasterLogic.TXTIP_MOBILE },
                        new int[] { 0, 0, 0, 10 }, new int[] { 0, 0, 0, 10 },
                        new int[] { 0, 0, 0, 0 }) != 1)
                    return ObjCom.JsonRspMsg(1, 0, MTHDNAME, 7, objMas.DBErrBuf, null);

                data.CustCode = ipdata[0]; data.CustDesc = ipdata[1];data.Email = ipdata[2];data.ContactNo = ipdata[3];

                Ret = objMas.ExecProcedure(1, "EXEC Customer_Save " + Globals.MNU_MAS_CUSTOMERS + "," + data.CustId + ",'" + data.CustCode + "','" +
                    data.CustDesc + "','" + data.Email + "','" + data.ContactNo + "'," + data.Status + ", " + Request.Cookies.Get(Globals.COOKIE_LGNEMPID).Value);

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
        [Route("Master/Customer_Delete")]
        public ActionResult Customer_Delete(long Id)
        {
            string MTHDNAME = "Customer_Delete";
            try
            {
                Int64 Ret = 0;

                if (ObjCom.ChkLgnSession(Request.Cookies) != 1)
                    return RedirectToAction(Globals.CNTRLMETHOD_LOGIN, Globals.CONTROLLER_LOGIN);

                Ret = objMas.ExecProcedure(0, "EXEC Customer_Delete " + Globals.MNU_MAS_CUSTOMERS + "," + Id + ", " +
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


        public ActionResult Reason()
        {
            string MTHDNAME = "Reason";
            try
            {
                if (ObjCom.ChkLgnSession(Request.Cookies) != 1)
                    return RedirectToAction(Globals.CNTRLMETHOD_LOGIN, Globals.CONTROLLER_LOGIN);

                ViewBag.PgAction = ObjCom.GetPageAction(Globals.MNU_MAS_REASON, Request.Cookies);
                if (ViewBag.PgAction[3] != '1')
                    return RedirectToAction(Globals.CNTRLMETHOD_DASHBOARD, Globals.CONTROLLER_DASHBOARD);

                if (ObjCom.ChkLgnSession(Request.Cookies) != 1)
                    return RedirectToAction(Globals.CNTRLMETHOD_LOGIN, Globals.CONTROLLER_LOGIN);
                return View();
            }
            catch (Exception Ex)
            {
                return ObjCom.JsonRspException(MTHDNAME + " C[3 : 1,3]", Ex.Message);
            }
        }


        public ActionResult Reason_Grid(int Status)
        {
            string MTHDNAME = "Reason_Grid";
            try
            {
                List<mdlReason_Grid> lstItem = objMas.GetDataList<mdlReason_Grid>("EXEC Reason_Grid " + Status);
                if (lstItem == null)
                    return ObjCom.JsonRspMsg(1, 0, MTHDNAME, 1, Globals.SERVER_ERROR, null);

                return ObjCom.JsonRspList(1, MTHDNAME, lstItem.Count(), lstItem);
            }
            catch (Exception Ex)
            {
                return ObjCom.JsonRspException(MTHDNAME + " C[3 : 1,3]", Ex.Message);
            }
        }

        public ActionResult Reason_Save(mdlReason_Save data)
        {
            string MTHDNAME = ": Reason_Save";
            try
            {
                Int64 Ret = 0;
                DataTable dt = new DataTable();

                if (ObjCom.ChkLgnSession(Request.Cookies) != 1)
                    return RedirectToAction(Globals.CNTRLMETHOD_LOGIN, Globals.CONTROLLER_LOGIN);

                string[] ipdata = { data.ReasonDesc};
                if (objMas.Text_ChkInputs(new int[] { 0}, ref ipdata, new string[] { "Reason Description" },
                        new int[] { MasterLogic.TXTIP_ALPHA_NOS_SPL_ANY },
                        new int[] { 0 }, new int[] { 0 },
                        new int[] { 0 }) != 1)
                    return ObjCom.JsonRspMsg(1, 0, MTHDNAME, 7, objMas.DBErrBuf, null);

                data.ReasonDesc = ipdata[0];

                Ret = objMas.ExecProcedure(1, "EXEC Reason_Save " + Globals.MNU_MAS_REASON + ",'" + data.ReasonDesc + "'," + data.Status + ", " + 
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


        public ActionResult Reason_Delete(long Id)
        {
            string MTHDNAME = "Reason_Delete";
            try
            {
                Int64 Ret = 0;

                if (ObjCom.ChkLgnSession(Request.Cookies) != 1)
                    return RedirectToAction(Globals.CNTRLMETHOD_LOGIN, Globals.CONTROLLER_LOGIN);

                Ret = objMas.ExecProcedure(0, "EXEC Reason_Delete " + Globals.MNU_MAS_REASON + "," + Id + ", " +
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

                model.ddlRole = objMas.GetDataList<ddlValues>("Select EmpId As ddlId,EmpName As ddlDesc From Employees Where Status=1 and EmpId != " +
                    Request.Cookies.Get(Globals.COOKIE_LGNEMPID).Value + " order by EmpName");
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