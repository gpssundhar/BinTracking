using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Globals
{

    public const string APPVER = "1.0.0 Realeased on 09.06.2025";//Started -> 16.05.2025"

    public static string IMGPATH = "Upload";// Organization logo folder

    public static string RPTPATH = "Reports";

    // Global Variables
    // App Admin UserId 
    public static string SUPERADMIN_ID = "admin@otaindia.com";
    public static string gsAppName = "Bin Tracking";

    // Cookie
    
    public const string COOKIE_LGNUSERID = "LngUsrId";
    public const string COOKIE_LGNEMPID = "EmpId";
    public const string COOKIE_LGNEMPDESC = "EmpName";
    public const string COOKIE_LGNEMAIL = "Email";
    public const string COOKIE_LGNEROLE = "Role";
    public const string COOKIE_LGNSESSION = "SessionId";
    public const string COOKIE_DEFAULTSESSION = "DEF_SessionId";

    //Display Messages
    public const string SERVER_ERROR = "Server Error.\n Contact System Admin";
    public const string SAVE_SUCCESS = "Save Success";
    public const string SAVE_FAILED = "Save Failed";
    public const string DELETE_SUCCESS = "Delete Success";
    public const string FETCH_FAILED = "Data Fetch Failed";
    public const string INVALID_PARAMS = "Invalid Input Parameters";
    public const string SESSION_TIMEOUT = "Session Timed Out";
    public const string LOGIN_SUCCESS = "Login Success";
    public const string LOGIN_FAILED = "Login Failed";

    // Rejection Type
    public const int STATUS_ACTIVE = 1;
    public const int STATUS_INACTIVE = 0;
    public const int STATUS_SUPERADMIN = 8;

    public const string STR_ACTIVE = "Active";
    public const string STR_INACTIVE = "Inactive";

    //Controller and Action Methods
    public const string CONTROLLER_LOGIN = "Home";
    public const string CONTROLLER_MASTER = "Master";
    public const string CONTROLLER_DASHBOARD = "DashBoard";
    public const string CONTROLLER_REPORT = "Report";
    public const string CONTROLLER_SERVICE = "Service";
    
    public const string CNTRLMETHOD_LOGIN = "Index";
    public const string CNTRLMETHOD_DASHBOARD = "DashBoard_Branch";
    public const string CNTRLMETHOD_COMMINGSOON = "CommingSoon";

    // Download Format
    public const int DWNLDFMT_EXCEL = 0;
    public const int DWNLDFMT_PDF = 1;


    //Adjust type
    public const int ADJTYPE_NORMAL = 1;
    public const int ADJTYPE_CUSTOMER = 2;


    public static int MNU_AUDIT_PAGE = 901;
    
    //  ****    App Menus   ***

    // DashBoard --> (101-199)
    public static int MNU_DASHBOARD = 101;

    // Masters --> (201-299)
    public static int MNU_MAS_EMPLOYEES = 201;
    public static int MNU_MAS_TRANSPORTER = 202;
    public static int MNU_MAS_SHIFTS = 203;
    public static int MNU_MAS_PRODUCTS = 204;
    public static int MNU_MAS_CUSTOMERS = 205;
    public static int MNU_MAS_REASON = 206;
    public static int MNU_MAS_PAGEACTION = 207;

    // Service --> 301 - 399
    public static int MNU_SRV_INVENTORY = 301;
    public static int MNU_SRV_CUSTINVENTORY = 302;
    public static int MNU_SRV_STOCKADJUST = 303;
    public static int MNU_SRV_CUSTSTOCKADJUST = 304;

    // Report --> 401 - 499
    public static int MNU_RPT_STOCKADJUST = 401;
    public static int MNU_RPT_CUSTSTOCKADJUST = 402;
    public static int MNU_RPT_INVENTORY = 403;
    public static int MNU_RPT_CUSTOMERINVENTORY = 404;

}