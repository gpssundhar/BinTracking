using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;
using log4net;
using System.Text;
using System.Data.SqlClient;
using System.Drawing;
using System.Collections.Generic;
using System.Reflection;
using System.Collections;
using System.Web.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Net.Mail;
using System.Net;
using SakthiAutomotive;


public class MasterLogic
{
    // Page : M --> A [ 52 ]  C [ 37 ]
    
    SqlConnection DbCon;
    ILog logwriter = LogManager.GetLogger("FILE");

    public string DBErrBuf = "";


    // Pdf Font Size
    public const int PDF_FONT_SMALL = 1;
    public const int PDF_FONT_SMALL_BOLD = 1;
    public const int PDF_FONT_NORMAL = 3;
    public const int PDF_FONT_NORMAL_BOLD = 4;
    public const int PDF_FONT_HIGH = 5;
    public const int PDF_FONT_HIGH_BOLD = 6;

    // Report Data Styles [Pdf / Excel]
    public const int RPTSTYLE_STRING = 0;
    public const int RPTSTYLE_STRING_SHORT = 1;
    public const int RPTSTYLE_INT = 2;
    public const int RPTSTYLE_DECIMAL = 3;
    public const int RPTSTYLE_DECIMAL_ROUND = 4;
    public const int RPTSTYLE_DATE = 5;

    // Excel Read Data Types
    public const int EXCLRD_STRING = 0;
    public const int EXCLRD_INT = 1;
    public const int EXCLRD_DECIMAL = 2;
    public const int EXCLRD_DATE = 3;
    public const int EXCLRD_TIME = 4;
    public const int EXCLRD_DATETIME = 5;

    // Date Format
    public const int DTFMT_YYMD_TO_DMYY = 1;    // YYYYMMDD to DDMMYYYY
    public const int DTFMT_YYMD_TO_DMY = 2;     // YYYYMMDD to DDMMYY
    public const int DTFMT_DMYY_TO_YYMD = 3;    // DDMMYYYY to YYYYMMDD
    public const int DTFMT_DMYY_TO_YMD = 4;     // DDMMYYYY to YYMMDD
    public const int DTFMT_MMMYY_TO_YM = 5;   // MMMMYYYY to YYMM (January 2022 as 2022-01


    // Chk Date Range Params
    public const int DTRNG_DONT = 0;
    public const int DTRNG_LESS_EQUAL_TODAY = 1;    // SelDate <= Today
    public const int DTRNG_HIGH_EQUAL_TODAY = 2;    // SelDate >= Today
    public const int DTRNG_LESS_TODAY = 3;          // SelDate <= Yesterday
    public const int DTRNG_HIGH_TODAY = 4;          // SelDate >= Tommorrow
    public const int DTRNG_ANY = 5;                 // Any Date


    public const int DTRNG_FMT_DDMMYY = 1;          // Date in dd-mm-yyyy format
    public const int DTRNG_FMT_YYMMDD = 2;          // Date in yyyy-mm-dd format



    //******************************************      Input Validation    *************

    // Text Input - Chk Valid Data & Trim
    public const int TXTIP_NONE = 0;                        // Dont Check
    public const int TXTIP_NOS_ZERO = 1;                    // Mandatory;  Only Nos;  All Zeros - Allowed
    public const int TXTIP_NOS = 2;                         // Mandatory;  Only Nos;  All Zeros - Not Allowed
    public const int TXTIP_IFAVL_NOS = 3;                   // If Avaialable;  Only Nos;  All Zeros - Not Allowed
    public const int TXTIP_HEX = 4;                         // Mandatory;  Only A-F, 0-9; Dont check lenth as * of 2
    public const int TXTIP_IFAVL_HEX = 5;                   // If Avaialable;  Only A-F, 0-9;   Dont check lenth as * of 2
    public const int TXTIP_HEX_LEN = 6;                     // Mandatory;  Only A-F, 0-9;   Check lenth as * of 2
    public const int TXTIP_IFAVL_HEX_LEN = 7;               // If Avaialable;  Only A-F, 0-9;  Check lenth as * of 2
    public const int TXTIP_ALPHA = 8;                       // Mandatory;  Only Alphabets
    public const int TXTIP_IFAVL_ALPHA = 9;                 // If Avaialable;  Only Alphabets
    public const int TXTIP_ALPHA_NOS = 10;                  // Mandatory;  Mixed Nos & Alphabets;  All Zeros - Not Allowed
    public const int TXTIP_IFAVL_ALPHA_NOS = 11;            // If Avaialable;  Mixed Nos & Alphabets;  All Zeros - Not Allowed
    public const int TXTIP_ALPHA_NOS_ANY = 12;              // Mandatory;   May Contain as all Alpha or all Nos;  All Zeros - Not Allowed
    public const int TXTIP_IFAVL_ALPHA_NOS_ANY = 13;        // If Available;  May Contain as all Alpha or all Nos;  All Zeros - Not Allowed
    public const int TXTIP_ALPHA_NOS_SPL = 14;              // Mandatory;  Mixed Nos, Alphabets, Spl Char;  All Zeros / Nos / Special - Not Allowed
    public const int TXTIP_IFAVL_ALPHA_NOS_SPL = 15;        // If Available;  Mixed Nos, Alphabets, Spl Char;  All Zeros / Nos / Special - Not Allowed
    public const int TXTIP_ALPHA_NOS_SPL_ANY = 16;          // Mandatory;  May Contain Mixed or any of Nos/Alphabets with Spl Char;  All Zeros / Special - Not Allowed
    public const int TXTIP_IFAVL_ALPHA_NOS_SPL_ANY = 17;    // If Available; May Contain Mixed or any of Nos/Alphabets with Spl Char;  All Zeros / Special - Not Allowed
    public const int TXTIP_ALPHA_SPC = 18;                  // Mandatory;  Alphabets & Space
    public const int TXTIP_IFAVL_ALPHA_SPC = 19;            // If Avaialable;  Alphabets & Space
    public const int TXTIP_NOS_SPL = 20;                    // Mandatory;  Mixed Nos, Spl Char;  All Zeros / Special - Not Allowed
    public const int TXTIP_IFAVL_NOS_SPL = 21;              // If Available;  Mixed Nos, Spl Char;  All Zeros / Special - Not Allowed
    public const int TXTIP_DECIMAL = 22;                    // Mandatory;  Only Decimal Nos;  All Zeros - Not Allowed
    public const int TXTIP_IFAVL_DECIMAL = 23;              // If Avaialable;  Only Decimal Nos;  All Zeros - Not Allowed
    public const int TXTIP_DECIMAL_ZERO = 24;               // Mandatory;  Only Decimal Nos;  Zeros - Allowed
    public const int TXTIP_EMAIL = 25;                      // Mandatory;  Email 
    public const int TXTIP_IFAVL_EMAIL = 26;                // If Available;  Email 
    public const int TXTIP_MOBILE = 27;                     // Mandatory;  Only Nos;  All Zeros - Not Allowed
    public const int TXTIP_IFAVL_MOBILE = 28;               // If Avaialable;  Only Nos;  All Zeros - Not Allowed
    public const int TXTIP_MOBILE_DIALCODE = 29;            // Mobile with Dial Code. Mandatory;  Only Nos;  All Zeros - Not Allowed
    public const int TXTIP_IFAVL_MOBILE_DIALCODE = 30;      // Mobile with Dial Code. If Avaialable;  Only Nos;  All Zeros - Not Allowed
    public const int TXTIP_PHONENO = 31;                    // Mandatory;  Nos, Hyphen, Space
    public const int TXTIP_IFAVL_PHONENO = 32;              // If Avaialable;  Nos, Hyphen, Space
    public const int TXTIP_PHONENO_MULTI = 33;              // Mandatory; Mulitple Phone Nos separated by Comma;  Nos, Hyphen, Space, Comma
    public const int TXTIP_IFAVL_PHONENO_MULTI = 34;        // If Avaialable; Mulitple Phone Nos separated by Comma;  Nos, Hyphen, Space, Comma
    public const int TXTIP_NAME = 35;                       // Mandatory;  Alphabets & Dot & Space
    public const int TXTIP_IFAVL_NAME = 36;                 // If Avaialable;  Alphabets & Dot & Space
    public const int TXTIP_MASTER_DESC = 37;                // Mandatory;  Alphabets, Numbers, Space, Specials
    public const int TXTIP_IFAVL_MASTER_DESC = 38;          // If Avaialable;  Alphabets, Numbers, Space, Specials
    public const int TXTIP_MASTER_CODE = 39;                // Mandatory;  Alphabets, Numbers, Space, Dot, Hyphen, Slash
    public const int TXTIP_IFAVL_MASTER_CODE = 40;          // If Avaialable;  Alphabets, Numbers, Space, Dot, Hyphen, Slash
    public const int TXTIP_MACID = 41;                      // 0-9 A-F (Hex value) & colon alone
    public const int TXTIP_IFAVL_MACID = 42;                // If Avaialable;  0-9 A-F (Hex value) & colon alone





    // ******************************* DB Related Functions *********************
    public MasterLogic()
    {
        GetDbConnStr();
    }


    private void GetDbConnStr()
    {
        try
        {
            int i, j;
            string Pwd, tmp;

            i = j = 0;
            Pwd = tmp = "";

            tmp = ConfigurationManager.ConnectionStrings["SqlConn"].ToString();
            string[] sCon = tmp.Split(new char[] { ';', }, StringSplitOptions.RemoveEmptyEntries);
            i = Array.FindIndex(sCon, m => m.StartsWith("Password="));
            if (i != -1)
            {
                j = sCon[i].IndexOf("=");
                if (j == -1)
                {
                    PrintLog("Master", "DB Connection String Split Failed");
                    return;
                }

                Pwd = sCon[i].Substring(j + 1);
                Pwd = DecodeData(Pwd);
                sCon[i] = sCon[i].Substring(0, j + 1) + Pwd;
                tmp = "";
                foreach (string s in sCon)
                    tmp += s + ";";
            }

            DbCon = new SqlConnection(tmp);
        }
        catch { }
    }



    public void PrintLog(string FnName, string Data)
    {
        try
        {
            logwriter.Debug(FnName + " : " + Data);
        }

        catch { }
    }


    public int ChkDBConnection()
    {
        // Return --> 1 Sucess, 0 Failed
        DBErrBuf = "";
        try
        {
            DbCon.Open();
            return 1;
        }
        catch (Exception Ex)
        {
            DBErrBuf = "MC[1]: Database Connection Failed. " + Ex.Message;
            PrintLog("ChkDBCon", DBErrBuf);
            return 0;
        }
        finally
        {
            DbCon.Close();
        }
    }


    public int SessionUpdate(string Qry)
    {
        // Return --> -1 Catch Error, 0 SessionId
        DBErrBuf = "";
        try
        {
            int ret = 0;
            SqlCommand cmd;

            if (DbCon == null)
                return -1;
            if (DbCon.State == ConnectionState.Closed)
                DbCon.Open();

            Qry += ", @Session OUTPUT";
            cmd = new SqlCommand(Qry, DbCon);
            cmd.Parameters.Add("@Session", SqlDbType.VarChar, 5000);
            cmd.Parameters["@Session"].Direction = ParameterDirection.Output;
            ret = cmd.ExecuteNonQuery();
            DBErrBuf = Convert.ToString(cmd.Parameters["@Session"].Value);
            ret = ret == -1 ? 0 : 1;
            if (ret == -1)
                PrintLog("SESSUPD", " Rsp Return [" + ret + "] ErrMsg [" + DBErrBuf + "]");
            return ret;
        }
        catch (Exception Ex)
        {
            DBErrBuf = "Server Error";
            PrintLog("SESSUPD Exception", Ex.Message);
            return -1;
        }
        finally
        {
            if (DbCon.State == ConnectionState.Open)
                DbCon.Close();
        }
    }


    public int GetIntValue(string sQry, ref int iDstVal)
    {
        // Return --> -1 Error, 1 Success
        DBErrBuf = "";
        try
        {
            iDstVal = 0;
            if (DbCon == null)
                return -1;
            if (DbCon.State == ConnectionState.Closed)
                DbCon.Open();
            SqlCommand cmd = new SqlCommand(sQry, DbCon);
            iDstVal = Convert.ToInt32(cmd.ExecuteScalar());
            return 1;
        }
        catch (Exception Ex)
        {
            DBErrBuf = "DC[8]: " + Ex.Message;
            PrintLog("GIVI Exception", DBErrBuf);
            return -1;
        }
        finally
        {
            if (DbCon.State == ConnectionState.Open)
                DbCon.Close();
        }
    }


    public DataTable GetDataTable(string Qry)
    {
        // Retrun --> null Failed, dt Success
        DBErrBuf = "";
        try
        {
            DataTable dt = new DataTable();

            if (Qry.Length == 0)
                return null;

            PrintLog("GDT", Qry);
            DbCon.Open();
            SqlCommand Cmd = new SqlCommand(Qry, DbCon);
            SqlDataAdapter Da = new SqlDataAdapter(Cmd);
            Da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            DBErrBuf = "MC[2]: " + ex.Message;
            PrintLog("GDT Exception", "Qry [" + Qry + "] Err [" + DBErrBuf);
            return null;
        }
        finally
        {
            if (DbCon.State == ConnectionState.Open)
                DbCon.Close();
        }
    }


    public DataTable GetDataTable(string AutoSNoDesc, string Qry)
    {
        // AutoSNoDesc - AutoSlNo added to DataTable. Caption for AutoSlNo
        // Retrun --> null Failed, dt Success
        DBErrBuf = "";
        try
        {
            DataTable dt = new DataTable();
            DataTable mdt = new DataTable();

            if (Qry.Length == 0)
                return null;

            PrintLog("GDT", Qry);
            DbCon.Open();
            SqlCommand Cmd = new SqlCommand(Qry, DbCon);
            SqlDataAdapter Da = new SqlDataAdapter(Cmd);
            Da.Fill(dt);

            DataColumn dc = mdt.Columns.Add();
            dc.ColumnName = AutoSNoDesc;
            dc.DataType = typeof(int);
            dc.AutoIncrement = true;
            dc.AutoIncrementSeed = 1;
            dc.AutoIncrementStep = 1;
            mdt.Merge(dt);

            return mdt;
        }
        catch (Exception ex)
        {
            DBErrBuf = "MC[3]: " + ex.Message;
            PrintLog("GDT Exception", "Qry [" + Qry + "] Err [" + DBErrBuf);
            return null;
        }
        finally
        {
            if (DbCon.State == ConnectionState.Open)
                DbCon.Close();
        }
    }


    public int FetchSingleColumn(string Qry, ref string Data)
    {
        // Return --> -1 Error, 0 Nil Output, 1 Sucess
        DBErrBuf = "";
        try
        {
            int ret = 0;

            if (Qry.Length == 0)
                return -1;

            Data = "";
            PrintLog("FSC", Qry);

            DbCon.Open();
            SqlCommand Cmd = new SqlCommand(Qry, DbCon);
            SqlDataReader Reader = Cmd.ExecuteReader();
            if (Reader.Read())
            {
                ret = 1;
                Reader.Close();
                Data = Convert.ToString(Cmd.ExecuteScalar());
                PrintLog("FSC O/p", Data);
            }
            else
            {
                ret = 0;
                Reader.Close();
            }
            return ret;
        }
        catch (Exception ex)
        {
            DBErrBuf = "MC[4]: " + ex.Message;
            PrintLog("FSC Exception", "Qry [" + Qry + "] Err [" + DBErrBuf);
            return -1;
        }
        finally
        {
            if (DbCon.State == ConnectionState.Open)
                DbCon.Close();
        }
    }


    public long GetTableCount(string Qry)
    {
        // Return --> -1 Error, x Rows Count
        DBErrBuf = "";
        try
        {
            int ret = 0;
            string buf = "";

            if (Qry.Length == 0)
                return -1;

            Qry = "SELECT COUNT(1) FROM " + Qry;
            ret = FetchSingleColumn(Qry, ref buf);
            if (ret == -1)
                return -1;
            else if (ret == 0)
                return 0;

            return Convert.ToInt64(buf);
        }
        catch (Exception Ex)
        {
            DBErrBuf = "MC[5]: " + Ex.Message;
            PrintLog("GTC Exception", DBErrBuf);
            return -1;
        }
    }


    public int GetId(string Qry, ref string sDstVal)
    {
        // Return --> Error : -1, Success : 1
        DBErrBuf = "";
        sDstVal = "";
        try
        {
            int ret = 0;

            ret = FetchSingleColumn(Qry, ref sDstVal);
            if (ret == -1)
                return -1;

            return 1;
        }
        catch (Exception Ex)
        {
            DBErrBuf = "MC[6]: " + Ex.Message;
            PrintLog("GIDS Exception", "Qry [" + Qry + "] Err [" + DBErrBuf);
            return -1;
        }
    }


    public int GetId(string Qry, ref long iDstVal)
    {
        // Return --> Error : -1, No rows : 0, Success : 1
        DBErrBuf = "";
        iDstVal = 0;
        try
        {
            int ret = 0;
            string buf = "";

            ret = FetchSingleColumn(Qry, ref buf);
            if (ret == -1)
                return -1;
            else if (buf.Length == 0)
                return 0;

            iDstVal = Convert.ToInt64(buf);
            return 1;
        }
        catch (Exception Ex)
        {
            DBErrBuf = "MC[7]: " + Ex.Message;
            PrintLog("GIDI64 Exception", "Qry [" + Qry + "] Err [" + DBErrBuf);
            return -1;
        }
    }


    public int GetId(string Qry, ref int iDstVal)
    {
        // Return --> Error : -1, No rows : 0,  Success : 1
        DBErrBuf = "";
        iDstVal = 0;
        try
        {
            int ret = 0;
            string buf = "";

            ret = FetchSingleColumn(Qry, ref buf);
            if (ret == -1)
                return -1;
            else if (buf.Length == 0)
                return 0;

            iDstVal = Convert.ToInt16(buf);
            return 1;
        }
        catch (Exception Ex)
        {
            DBErrBuf = "MC[8]: " + Ex.Message;
            PrintLog("GIDI Exception", "Qry [" + Qry + "] Err [" + DBErrBuf);
            return -1;
        }
    }


    public int ExecQry(string Qry)
    {
        // Return --> -1 Error, x Rows Affected
        DBErrBuf = "";
        try
        {
            int ret = 0;
            PrintLog("ExeQry", Qry);
            DbCon.Open();
            SqlCommand cmd = new SqlCommand(Qry, DbCon);
            ret = cmd.ExecuteNonQuery();
            return ret;
        }
        catch (Exception Ex)
        {
            DBErrBuf = "MC[9]: " + Ex.Message;
            PrintLog("ExeQry", DBErrBuf);
            return -1;
        }
        finally
        {
            if (DbCon.State == ConnectionState.Open)
                DbCon.Close();
        }
    }


    public int ExecMultipleQry(int IsForUpd, string Qry)
    {
        // IsForUpd --> 0 Only with Begin/Commit, 1 With Begin/commit For update
        // Return ---> -1 Error, X Rows Affected
        DBErrBuf = "";
        DbCon.Open();
        SqlTransaction objSqlT;

        if (IsForUpd == 1)
            objSqlT = DbCon.BeginTransaction(IsolationLevel.Serializable);
        else
            objSqlT = DbCon.BeginTransaction();
        try
        {
            int AffRows = 0;
            SqlCommand cmd;

            PrintLog("EMQAL ", Qry);
            if (Qry.Length == 0)
                return -1;

            cmd = new SqlCommand(Qry, DbCon, objSqlT);
            AffRows += cmd.ExecuteNonQuery();
            cmd.Dispose();
            objSqlT.Commit();
            return AffRows;
        }
        catch (Exception ex)
        {
            DBErrBuf = "MC[10]: " + ex.Message;
            PrintLog("EMQAL Exception", DBErrBuf);
            if (DbCon.State == ConnectionState.Open)
                objSqlT.Rollback();
            return -1;
        }
        finally
        {
            if (DbCon.State == ConnectionState.Open)
                DbCon.Close();
        }
    }

    public int ExecMultipleQry(ArrayList qryLst)
    {
        // Return ---> -1 Error, X Rows Affected
        if (DbCon == null)
            return -1;
        if (DbCon.State == ConnectionState.Closed)
            DbCon.Open();
        SqlTransaction objSqlT = DbCon.BeginTransaction();
        try
        {
            int AffRows = 0, i = 0;
            string sQry = "";

            SqlCommand cmd;
            for (i = 0; i < qryLst.Count; i++)
            {
                sQry = "";
                sQry = qryLst[i].ToString();
                if (sQry.Length == 0)
                    continue;
                PrintLog("EMQ [" + i + "]", sQry);
                cmd = new SqlCommand(sQry, DbCon, objSqlT);
                AffRows += cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            objSqlT.Commit();
            return AffRows;
        }
        catch (Exception ex)
        {
            PrintLog("EMQ Exception", ex.Message);
            objSqlT.Rollback();
            return -1;
        }
        finally
        {
            if (DbCon.State == ConnectionState.Open)
                DbCon.Close();
        }
    }


    public int ExecMultipleQry(int IsForUpd, ArrayList qryLst)
    {
        // IsForUpd --> 0 Only with Begin/Commit, 1 With Begin/commit For update
        // Return ---> -1 Error, X Rows Affected
        DBErrBuf = "";
        DbCon.Open();

        SqlTransaction objSqlT;
        if (IsForUpd == 1)
            objSqlT = DbCon.BeginTransaction(IsolationLevel.Serializable);
        else
            objSqlT = DbCon.BeginTransaction();
        try
        {
            int AffRows = 0, i = 0;
            string sQry = "";

            SqlCommand cmd;
            for (i = 0; i < qryLst.Count; i++)
            {
                sQry = "";
                sQry = qryLst[i].ToString();
                if (sQry.Length == 0)
                    continue;
                PrintLog("EMQAL [" + i + "]", sQry);
                cmd = new SqlCommand(sQry, DbCon, objSqlT);
                AffRows += cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            objSqlT.Commit();
            return AffRows;
        }
        catch (Exception ex)
        {
            DBErrBuf = "MC[11]: " + ex.Message;
            PrintLog("EMQAL Exception", DBErrBuf);
            if (DbCon.State == ConnectionState.Open)
                objSqlT.Rollback();
            return -1;
        }
        finally
        {
            if (DbCon.State == ConnectionState.Open)
                DbCon.Close();
        }
    }


    public int ExecMultipleQry(int IsForUpd, int Cnt, string[] sQry)
    {
        // IsForUpd --> 0 Only with Begin/Commit, 1 With Begin/commit For update
        // Return ---> -1 Error, X Rows Affected
        DBErrBuf = "";
        DbCon.Open();
        SqlTransaction objSqlT;
        if (IsForUpd == 1)
            objSqlT = DbCon.BeginTransaction(IsolationLevel.Serializable);
        else
            objSqlT = DbCon.BeginTransaction();
        try
        {
            int AffRows = 0, i = 0;
            SqlCommand cmd;

            for (i = 0; i < Cnt; i++)
            {
                if (sQry[i].Length == 0)
                    continue;
                PrintLog("EMQA [" + i + "]", sQry[i]);
                cmd = new SqlCommand(sQry[i], DbCon, objSqlT);
                AffRows += cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            objSqlT.Commit();
            return AffRows;
        }
        catch (Exception ex)
        {
            DBErrBuf = "MC[12]: " + ex.Message;
            PrintLog("EMQA Exception", DBErrBuf);
            if (DbCon.State == ConnectionState.Open)
                objSqlT.Rollback();
            return -1;
        }
        finally
        {
            if (DbCon.State == ConnectionState.Open)
                DbCon.Close();
        }
    }


    public int ExecMasters(int IsForUpd, string QryId, ref string QryIdOp, ArrayList QryHead, ArrayList QryTail)
    {
        // IsForUpd --> 0 Only with Begin/Commit, 1 With Begin/commit For update
        // QryId will be executed only once and output copied to QryIdOp
        // If QryTail Avail, then QryId o/p will be appended to QryHead
        // Return --> Error : 0, Success : 1
        DBErrBuf = "";
        DbCon.Open();
        SqlTransaction objSqlT;
        if (IsForUpd == 1)
            objSqlT = DbCon.BeginTransaction(IsolationLevel.Serializable);
        else
            objSqlT = DbCon.BeginTransaction();
        try
        {
            int i = 0;
            string qry = "";
            SqlCommand cmdQry;

            if (QryHead.Count != QryTail.Count || QryId.Length == 0)
            {
                DBErrBuf = "MA[1]: Invalid Qry";
                PrintLog("EXMASAL-OP", "Cnt [" + QryHead.Count + "] [" + QryTail.Count + "] IdQry: " + QryId);
                objSqlT.Rollback();
                return 0;
            }

            SqlCommand cmdId = new SqlCommand(QryId, DbCon, objSqlT);
            PrintLog("EXMASAL-OP", QryId);
            QryIdOp = Convert.ToString(cmdId.ExecuteScalar());
            cmdId.Dispose();
            if (QryIdOp.Length == 0)
            {
                DBErrBuf = "MA[2]: Failed to Fetch Id";
                PrintLog("EXMASAL-OP", DBErrBuf);
                objSqlT.Rollback();
                return 0;
            }

            for (i = 0; i < QryHead.Count; i++)
            {
                qry = "";
                if (QryTail[i].ToString().Length == 0)
                    qry = QryHead[i].ToString();
                else
                    qry = QryHead[i].ToString() + QryIdOp + QryTail[i].ToString();
                if (qry.Length == 0)
                    continue;

                PrintLog("EXMASAL-OP", "i[" + i + "]: " + qry);
                cmdQry = new SqlCommand(qry, DbCon, objSqlT);
                cmdQry.ExecuteNonQuery();
                cmdQry.Dispose();
            }
            objSqlT.Commit();
            return 1;
        }
        catch (Exception ex)
        {
            DBErrBuf = "MC[13]: " + ex.Message;
            PrintLog("EXMASAL-OP Exception", DBErrBuf);
            if (DbCon.State == ConnectionState.Open)
                objSqlT.Rollback();
            return 0;
        }
        finally
        {
            if (DbCon.State == ConnectionState.Open)
                DbCon.Close();
        }
    }


    public int ExecMasters(int IsForUpd, string QryId, ArrayList QryHead, ArrayList QryTail)
    {
        // IsForUpd --> 0 Only with Begin/Commit, 1 With Begin/commit For update
        // QryId will be executed only once and output copied to QryIdOp
        // If QryTail Avail, then QryId o/p will be appended to QryHead
        // Return --> Error : 0, Success : 1
        DBErrBuf = "";
        try
        {
            string buf = "";

            if (QryHead.Count != QryTail.Count || QryId.Length == 0)
            {
                DBErrBuf = "MA[3]: Invalid Qry";
                PrintLog("EXMASAL", "Cnt [" + QryHead.Count + "] [" + QryTail.Count + "] IdQry: " + QryId);
                return 0;
            }

            return ExecMasters(IsForUpd, QryId, ref buf, QryHead, QryTail);
        }
        catch (Exception ex)
        {
            DBErrBuf = "MC[14]: " + ex.Message;
            PrintLog("EXMASAL Exception", DBErrBuf);
            return 0;
        }
    }


    public int ExecMasters(int IsForUpd, string QryId, ref string QryIdOp, string[] QryHead, string[] QryTail)
    {
        // IsForUpd --> 0 Only with Begin/Commit, 1 With Begin/commit For update
        // QryId will be executed only once and output copied to QryIdOp
        // If QryTail Avail, then QryId o/p will be appended to QryHead
        // Return --> Error : 0, Success : 1
        DBErrBuf = "";
        DbCon.Open();
        SqlTransaction objSqlT;
        if (IsForUpd == 1)
            objSqlT = DbCon.BeginTransaction(IsolationLevel.Serializable);
        else
            objSqlT = DbCon.BeginTransaction();
        try
        {
            int i = 0;
            string qry = "";
            SqlCommand cmdQry;

            if (QryHead.Length != QryTail.Length || QryId.Length == 0)
            {
                DBErrBuf = "MA[4]: Invalid Qry";
                PrintLog("EXMASARR-OP", "Cnt [" + QryHead.Length + "] [" + QryTail.Length + "] IdQry: " + QryId);
                objSqlT.Rollback();
                return 0;
            }

            SqlCommand cmdId = new SqlCommand(QryId, DbCon, objSqlT);
            PrintLog("EXMASARR-OP", QryId);
            QryIdOp = Convert.ToString(cmdId.ExecuteScalar());
            cmdId.Dispose();
            if (QryIdOp.Length == 0)
            {
                DBErrBuf = "MA[5]: Failed to Fetch Id";
                PrintLog("EXMASARR-OP", DBErrBuf);
                objSqlT.Rollback();
                return 0;
            }

            for (i = 0; i < QryHead.Length; i++)
            {
                qry = "";
                if (QryTail[i].ToString().Length == 0)
                    qry = QryHead[i].ToString();
                else
                    qry = QryHead[i].ToString() + QryIdOp + QryTail[i].ToString();
                if (qry.Length == 0)
                    continue;

                PrintLog("EXMASARR-OP", "i[" + i + "]: " + qry);
                cmdQry = new SqlCommand(qry, DbCon, objSqlT);
                cmdQry.ExecuteNonQuery();
                cmdQry.Dispose();
            }
            objSqlT.Commit();
            return 1;
        }
        catch (Exception ex)
        {
            DBErrBuf = "MC[15]: " + ex.Message;
            PrintLog("EXMASARR-OP Exception", DBErrBuf);
            if (DbCon.State == ConnectionState.Open)
                objSqlT.Rollback();
            return 0;
        }
        finally
        {
            if (DbCon.State == ConnectionState.Open)
                DbCon.Close();
        }
    }


    public int ExecMasters(int IsForUpd, string QryId, string[] QryHead, string[] QryTail)
    {
        // IsForUpd --> 0 Only with Begin/Commit, 1 With Begin/commit For update
        // QryId will be executed only once and output copied to QryIdOp
        // If QryTail Avail, then QryId o/p will be appended to QryHead
        // Return --> Error : 0, Success : 1
        DBErrBuf = "";
        try
        {
            string buf = "";

            if (QryHead.Length != QryTail.Length || QryId.Length == 0)
            {
                DBErrBuf = "MA[6]: Invalid Qry";
                PrintLog("EXMASARR", "Cnt [" + QryHead.Length + "] [" + QryTail.Length + "] IdQry: " + QryId);
                return 0;
            }
            return ExecMasters(IsForUpd, QryId, ref buf, QryHead, QryTail);
        }
        catch (Exception ex)
        {
            DBErrBuf = "MC[16]: " + ex.Message;
            PrintLog("EXMASARR Exception", DBErrBuf);
            return 0;
        }
    }


    public int ExecMasters(int IsForUpd, string QryId, ref string QryIdOp, string QryHead, string QryTail)
    {
        // IsForUpd --> 0 Only with Begin/Commit, 1 With Begin/commit For update
        // QryId will be executed only once and output copied to QryIdOp
        // If QryTail Avail, then QryId o/p will be appended to QryHead
        // Return --> Error : 0, Success : 1
        DBErrBuf = "";
        try
        {
            if (QryHead.Length == 0 || QryId.Length == 0)
            {
                DBErrBuf = "MA[7]: Invalid Qry";
                PrintLog("EXMAS-OP", "Invalid Qry Id[" + QryId.Length + "] Head [" + QryHead.Length + "]");
                return 0;
            }

            return ExecMasters(IsForUpd, QryId, ref QryIdOp, new string[] { QryHead }, new string[] { QryTail });
        }
        catch (Exception ex)
        {
            DBErrBuf = "MC[17]: " + ex.Message;
            PrintLog("EXMAS-OP Exception", DBErrBuf);
            return 0;
        }
    }


    public int ExecMasters(int IsForUpd, string QryId, string QryHead, string QryTail)
    {
        // IsForUpd --> 0 Only with Begin/Commit, 1 With Begin/commit For update
        // QryId will be executed only once and output copied to QryIdOp
        // If QryTail Avail, then QryId o/p will be appended to QryHead
        // Return --> Error : 0, Success : 1
        DBErrBuf = "";
        try
        {
            string buf = "";
            if (QryHead.Length == 0 || QryId.Length == 0)
            {
                DBErrBuf = "MA[8]: Invalid Qry";
                PrintLog("EXMAS", "Invalid Qry Id[" + QryId.Length + "] Head [" + QryHead.Length + "]");
                return 0;
            }

            return ExecMasters(IsForUpd, QryId, ref buf, new string[] { QryHead }, new string[] { QryTail });
        }
        catch (Exception ex)
        {
            DBErrBuf = "MC[18]: " + ex.Message;
            PrintLog("EXMAS Exception", DBErrBuf);
            return 0;
        }
    }



    public int ExecProcedure(int ErrOp, string Qry)
    {
        // ErrOp --> 0 NA;  1 @ErrorMsg added to Procedure call
        // Return --> 0 Error, 1/2 Success [1 No Rows Affected, 2 Rows Affected]
        DBErrBuf = "";
        try
        {
            int ret = 0;
            SqlCommand cmd;

            if (Qry.Length == 0)
            {
                DBErrBuf = "MA[9]: Invalid Qry";
                PrintLog("EPROC-E", DBErrBuf);
                return 0;
            }

            if (DbCon.State == ConnectionState.Closed)
                DbCon.Open();

            if (ErrOp == 1)
                Qry += ", @ErrorMsg OUTPUT";

            PrintLog("EPROC-E", Qry);
            cmd = new SqlCommand(Qry, DbCon);
            if (ErrOp == 1)
            {
                cmd.Parameters.Add("@ErrorMsg", SqlDbType.VarChar, 5000);
                cmd.Parameters["@ErrorMsg"].Direction = ParameterDirection.Output;
            }

            ret = cmd.ExecuteNonQuery();
            ret = ret == 0 ? 1 : 2;
            if (ErrOp == 1)
                DBErrBuf = Convert.ToString(cmd.Parameters["@ErrorMsg"].Value);
            PrintLog("EPROC-E", "Rsp Return [" + ret + "] Msg [" + DBErrBuf + "]");
            return ret;
        }
        catch (Exception Ex)
        {
            DBErrBuf = "MC[19]: " + Ex.Message;
            PrintLog("EPROC-E", Ex.Message);
            return 0;
        }
        finally
        {
            if (DbCon.State == ConnectionState.Open)
                DbCon.Close();
        }
    }


    public int ExecProcedure(string Qry, ref string rsp)
    {
        DBErrBuf = "";
        try
        {
            int ret = 0;
            if (DbCon == null)
                return -1;
            if (DbCon.State == ConnectionState.Closed)
                DbCon.Open();

            PrintLog("EPROC", Qry);
            SqlCommand cmd = new SqlCommand(Qry, DbCon);
            cmd.Parameters.Add("@ErrorMsg", SqlDbType.VarChar, 150);
            cmd.Parameters["@ErrorMsg"].Direction = ParameterDirection.Output;
            ret = cmd.ExecuteNonQuery();
            rsp = Convert.ToString(cmd.Parameters["@ErrorMsg"].Value);
            return ret;
        }
        catch (Exception Ex)
        {
            DBErrBuf = "DC[10]: " + Ex.Message;
            PrintLog("EPROC Exception", "Qry [" + Qry + "] Error [" + DBErrBuf);
            return -1;
        }
        finally
        {
            if (DbCon.State == ConnectionState.Open)
                DbCon.Close();
        }
    }


    public int ExecProcedure(string Qry)
    {
        // Return --> 0 Error, 1/2 Success [1 No Rows Affected, 2 Rows Affected]
        DBErrBuf = "";
        try
        {
            return ExecProcedure(0, Qry);
        }
        catch (Exception Ex)
        {
            DBErrBuf = "MC[20]: " + Ex.Message;
            PrintLog("EPROC", Ex.Message);
            return 0;
        }
    }


    public List<SelectListItem> GetDataDDL(string Qry)
    {
        // Return --> Error - Null; Success - List
        try
        {
            DataTable dt = new DataTable();
            dt = GetDataTable(Qry);
            if (dt == null)
                return null;

            var slItem = (from DataRow a in dt.Rows
                          select new SelectListItem { Text = a["ddlDesc"].ToString(), Value = a["ddlId"].ToString() }).ToList();
            return slItem;
        }
        catch (Exception Ex)
        {
            DBErrBuf = "MC[21]: " + Ex.Message;
            PrintLog("GDDL Exception", DBErrBuf);
            return null;
        }
    }


    public List<T> GetDataList<T>(string Qry)
    {
        // Return --> Error - Null; Success - List
        DBErrBuf = "";
        try
        {
            DataTable dt = new DataTable();

            PrintLog("GDLst", Qry);

            dt = GetDataTable(Qry);
            if (dt == null)
                return null;

            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
            var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToList();
            var objectProperties = typeof(T).GetProperties(flags);
            var targetList = dt.AsEnumerable().Select(dataRow =>
            {
                var instanceOfT = Activator.CreateInstance<T>();

                foreach (var properties in objectProperties.Where(properties => columnNames.Contains(properties.Name) && dataRow[properties.Name] != DBNull.Value))
                    properties.SetValue(instanceOfT, dataRow[properties.Name], null);
                return instanceOfT;
            }).ToList();

            return targetList;
        }
        catch (Exception ex)
        {
            DBErrBuf = "MC[22]: " + ex.Message;
            PrintLog("GDLst Exception", "Qry [" + Qry + "] Err [" + DBErrBuf);
            return null;
        }
    }


    public List<T> ConvertToList<T>(DataTable dt)
    {
        // Return --> Error - Null; Success - List
        try
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
            var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToList();
            var objectProperties = typeof(T).GetProperties(flags);
            var targetList = dt.AsEnumerable().Select(dataRow =>
            {
                var instanceOfT = Activator.CreateInstance<T>();

                foreach (var properties in objectProperties.Where(properties => columnNames.Contains(properties.Name) && dataRow[properties.Name] != DBNull.Value))
                    properties.SetValue(instanceOfT, dataRow[properties.Name], null);
                return instanceOfT;
            }).ToList();

            return targetList;
        }
        catch (Exception Ex)
        {
            DBErrBuf = "MC[23]: " + Ex.Message;
            PrintLog("CDTLST Exception : ", DBErrBuf);
            return null;
        }
    }


    public DataTable ConvListToDataTable<T>(List<T> items)
    {
        // Return --> Error - Null; Success - Datatable
        try
        {
            int i = 0;
            DataTable dt = new DataTable(typeof(T).Name);

            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
                dt.Columns.Add(prop.Name);

            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (i = 0; i < Props.Length; i++)
                    values[i] = Props[i].GetValue(item, null);
                dt.Rows.Add(values);
            }
            return dt;
        }
        catch (Exception Ex)
        {
            DBErrBuf = "MC[24]: " + Ex.Message;
            PrintLog("CLSTDT Exception : ", DBErrBuf);
            return null;
        }
    }


    // **********************************  General *************************************
    public string EncodeData(string src)
    {
        DBErrBuf = "";
        try
        {
            string buf = "";
            buf = Convert.ToBase64String(Encoding.ASCII.GetBytes(src));
            return buf;
        }
        catch (Exception Ex)
        {
            DBErrBuf = "MC[25]: " + Ex.Message;
            PrintLog("ECDATA", DBErrBuf);
            return "";
        }
    }



    public string DecodeData(string src)
    {
        DBErrBuf = "";
        try
        {
            string buf = "";
            buf = Encoding.ASCII.GetString(Convert.FromBase64String(src));
            return buf;
        }
        catch (Exception Ex)
        {
            DBErrBuf = "MC[26]: " + Ex.Message;
            PrintLog("DCDATA", DBErrBuf);
            return "";
        }
    }


    public string ModifyDateFmt(string SrcDate, int Fmt)
    {
        DBErrBuf = "";
        try
        {
            string buf = "";

            if (Fmt == DTFMT_DMYY_TO_YYMD)
                buf = SrcDate.Substring(6, 4) + SrcDate.Substring(2, 4) + SrcDate.Substring(0, 2);
            else if (Fmt == DTFMT_DMYY_TO_YMD)
                buf = SrcDate.Substring(8, 2) + SrcDate.Substring(2, 4) + SrcDate.Substring(0, 2);
            else if (Fmt == DTFMT_YYMD_TO_DMYY)
                buf = SrcDate.Substring(8, 2) + SrcDate.Substring(4, 4) + SrcDate.Substring(0, 4);
            else if (Fmt == DTFMT_YYMD_TO_DMY)
                buf = SrcDate.Substring(8, 2) + SrcDate.Substring(4, 4) + SrcDate.Substring(2, 2);
            else if (Fmt == DTFMT_MMMYY_TO_YM)
            {
                DateTime dt = DateTime.ParseExact(SrcDate, "MMMM-yyyy", CultureInfo.InvariantCulture);
                buf = dt.ToString("yyyy-MM");
            }

            return buf;
        }
        catch (Exception Ex)
        {
            DBErrBuf = "MC[27]: " + Ex.Message;
            PrintLog("ModDT Exception", DBErrBuf);
            return "";
        }
    }



    public int ChkDateRange(int ChkFromDt, int ChkToDt, int ChkFrmToLess, int DTFmt, string FromTitle, string ToTitle, string FromDate, string ToDate)
    {
        // Return --> Error : 0, Success : 1
        // DTFmt --> Input Date Format [ DD-MM-YYYY  or YYYY-MM-DD ]
        // ChkFromDt --> 1 : <= CtDate, 2 : >= CtDate
        // ChkToDt   --> 0 : Dont Chk, 1 : <= CtDate, 2 : >= CtDate
        // ChkFrmToLess --> 0 Dont Chk, 1 Chk For FromDate <= ToDate, 2 Chk For From<=To & in Same Month, 3 Chk For From <= To & in Same Year
        DBErrBuf = "";
        try
        {
            int ret = 0;
            string buf = "";
            DateTime dtFromDate, dtToDate, dtCtDate;

            if (FromDate.Length != 10)
            {
                DBErrBuf = "MA[10]: Select Valid " + FromTitle + " Date";
                return 0;
            }

            buf = DateTime.Now.ToString("dd-MM-yyyy");
            dtCtDate = DateTime.Parse(buf.Substring(6, 4) + buf.Substring(2, 4) + buf.Substring(0, 2));
            dtFromDate = DateTime.Parse(FromDate.Substring(6, 4) + FromDate.Substring(2, 4) + FromDate.Substring(0, 2));

            if (ChkFromDt == DTRNG_LESS_EQUAL_TODAY && dtCtDate.CompareTo(dtFromDate) <= -1)
            {
                DBErrBuf = "MA[11]: Select Valid " + FromTitle + " Date";
                return 0;
            }
            else if (ChkFromDt == DTRNG_HIGH_EQUAL_TODAY && dtFromDate.CompareTo(dtCtDate) <= -1)
            {
                DBErrBuf = "MA[12]: Select Valid " + FromTitle + " Date";
                return 0;
            }
            else if (ChkFromDt == DTRNG_LESS_TODAY && dtCtDate.CompareTo(dtFromDate) <= 0)
            {
                DBErrBuf = "MA[13]: Select Valid " + FromTitle + " Date";
                return 0;
            }
            else if (ChkFromDt == DTRNG_HIGH_TODAY && dtFromDate.CompareTo(dtCtDate) <= 0)
            {
                DBErrBuf = "MA[14]: Select Valid " + FromTitle + " Date";
                return 0;
            }

            if (ChkToDt == DTRNG_DONT)
                return 1;

            if (ToDate.Length != 10)
            {
                DBErrBuf = "MA[15]: Select Valid " + ToTitle + " Date";
                return 0;
            }

            if (DTFmt == DTRNG_FMT_DDMMYY)
                dtToDate = DateTime.Parse(ToDate.Substring(6, 4) + ToDate.Substring(2, 4) + ToDate.Substring(0, 2));
            else
                dtToDate = DateTime.Parse(ToDate.Substring(0, 2) + ToDate.Substring(2, 4) + ToDate.Substring(6, 4));
            if (ChkToDt == DTRNG_LESS_EQUAL_TODAY && dtCtDate.CompareTo(dtToDate) <= -1)
            {
                DBErrBuf = "MA[16]: " + ToTitle + " Date Cannot be Higher than Current Date";
                return 0;
            }
            else if (ChkToDt == DTRNG_HIGH_EQUAL_TODAY && dtToDate.CompareTo(dtCtDate) <= -1)
            {
                DBErrBuf = "MA[17]: " + ToTitle + " Date Must be Higher than Current Date";
                return 0;
            }
            else if (ChkToDt == DTRNG_LESS_TODAY && dtCtDate.CompareTo(dtToDate) <= 0)
            {
                DBErrBuf = "MA[18]: " + ToTitle + " Date Cannot be Higher than Current Date";
                return 0;
            }
            else if (ChkToDt == DTRNG_HIGH_TODAY && dtToDate.CompareTo(dtCtDate) <= 0)
            {
                DBErrBuf = "MA[19]: " + ToTitle + " Date Must be Higher than Current Date";
                return 0;
            }

            if (ChkFrmToLess == 0)
                return 1;

            ret = dtToDate.CompareTo(dtFromDate);
            if (ret <= -1)
            {
                DBErrBuf = "MA[20]: " + FromTitle + " Date Cannot be Higher Than " + ToTitle + " Date";
                return 0;
            }

            if (ChkFrmToLess == 2)  // 2 Same Month
            {
                if (!(dtFromDate.Year == dtToDate.Year && dtFromDate.Month == dtToDate.Month))
                {
                    DBErrBuf = "MA[21]: Date Must be With in a Month ...";
                    return 0;
                }
            }
            else if (ChkFrmToLess == 3)  // 3 Same Year
            {
                if (!(dtFromDate.Year == dtToDate.Year))
                {
                    DBErrBuf = "MA[22]: Date Must be With in a Year ...";
                    return 0;
                }
            }

            return 1;
        }
        catch (Exception Ex)
        {
            DBErrBuf = "MC[28]: " + Ex.Message;
            PrintLog("DtRng Exception", DBErrBuf);
            return 0;
        }
    }




    //*********************************************************Read Data from Excel File ***************************************

    public DataSet Excel_GetDataFromFile(HttpPostedFileBase ExlFile, int[] SheetNos, int[] SkipRows, bool[] HasHeader, string[][] ReqCols)
    {
        /* Read data from Excel - Single/Multiple Sheets
             * Return --> Error NULL, Success Valid DataSet
             * ExlFile -- Uploaded Excel File Stream
             * SheetNos -- No of Sheets To be read. [Excel Sheet starts with 1] [Can read multiple sheet at a strech]
             * SkipRows -- No of Rows to be skipped before reading data at each sheet
             * HasHeaders -- Whether Sheet has header row. True/False
             * ReqCols -- ColumnDesc to be read from each sheet. [If sheet1 has 10 columns, specify column desc which are to be read. can skip unwanted columns]
             * SheetNos / SkipRows / HasHeaders / ReqCols --> Length must match
         */

        DBErrBuf = "";
        var rowNum = 0;
        try
        {
            int ret, i, k, Row;
            int[] Cols = new int[1];
            DataSet ds = new DataSet();

            ret = i = k = Row = 0;

            if (SheetNos == null || SheetNos.Length == 0)
                SheetNos = new int[] { 1 };

            if (SkipRows == null || SkipRows.Length == 0)
                SkipRows = Enumerable.Repeat(0, SheetNos.Length).ToArray();

            if (SheetNos.Length != SkipRows.Length || SheetNos.Length != HasHeader.Length || (ReqCols != null && ReqCols.Length != SheetNos.Length))
            {
                DBErrBuf = "MA[23]: Sheets / Skip Rows / Header Column Mismatch";
                return null;
            }

            using (var pck = new ExcelPackage(ExlFile.InputStream))
            {
                for (k = 0; k < SheetNos.Length; k++)
                {
                    DataTable dtCols = new DataTable();
                    DataTable dt = new DataTable();
                    i = SheetNos[k] == 0 ? 1 : SheetNos[k];
                    var ws = pck.Workbook.Worksheets[i];

                    Row = 1 + SkipRows[k];
                    Cols = Enumerable.Repeat(0, ws.Dimension.End.Column).ToArray();
                    foreach (var HdrCell in ws.Cells[Row, 1, Row, ws.Dimension.End.Column])
                        dtCols.Columns.Add(HasHeader[k] ? HdrCell.Text.Trim() : string.Format("Column {0}", HdrCell.Start.Column).Trim());

                    if (HasHeader[k])
                        Row++;

                    if (ReqCols != null)
                    {
                        for (i = 0; i < ReqCols[k].Length; i++)
                        {
                            ret = dtCols.Columns.IndexOf(ReqCols[k][i].Trim());
                            if (ret == -1)
                            {
                                DBErrBuf = "MA[24]: '" + ReqCols[k][i] + "' Not Available in Sheet [" + pck.Workbook.Worksheets[SheetNos[k]].Name;
                                return null;
                            }

                            dt.Columns.Add(dtCols.Columns[ret].ColumnName, dtCols.Columns[ret].DataType);
                            Cols[i] = ret + 1;
                        }

                        Cols = Cols.Where(x => x != 0).ToArray();
                        if (ReqCols[k].Length != dt.Columns.Count || ReqCols[k].Length != Cols.Length)
                        {
                            DBErrBuf = "MA[25]: Required Columns Mismatch in Sheet [" + pck.Workbook.Worksheets[SheetNos[k]].Name;
                            return null;
                        }
                    }
                    else
                    {
                        Cols = Enumerable.Range(1, ws.Dimension.End.Column).ToArray();
                        dt = dtCols;
                    }

                    for (; Row <= ws.Dimension.End.Row; Row++)
                    {
                        var wsRow = ws.Cells[Row, 1, Row, ws.Dimension.End.Column];
                        DataRow dr = dt.NewRow();
                        for (i = 0; i < Cols.Length; i++)
                            dr[i] = ws.Cells[Row, Cols[i]].Value;
                        dt.Rows.Add(dr);
                    }

                    ds.Tables.Add(dt);
                } // end for
            }

            if (ds.Tables.Count == 0)
            {
                DBErrBuf = "MA[26]: No Data Found";
                return null;
            }

            return ds;
        }
        catch (Exception Ex)
        {
            DBErrBuf = "MC[29]: AT Row Number : " + rowNum + ", Error :" + Ex.Message;
            PrintLog("EXELGET", DBErrBuf);
            return null;
        }
    }



    public ArrayList Excel_GetDataFromFile(HttpPostedFileBase ExlFile, int[] SheetNos, int[] SkipRows, bool[] HasHeader, bool[] ReqRowNos, int[][] ColDataType,
                            string[][] ReqCols, string[] InsQrys)
    {
        /* Read data from Excel - Single/Multiple Sheets -- generate as querry output
           * Return --> Error NULL, Success Valid Arraylist
           * ExlFile -- Uploaded Excel File Stream
           * SheetNos -- No of Sheets To be read. [Excel Sheet starts with 1] [Can read multiple sheet at a strech]
           * SkipRows -- No of Rows to be skipped before reading data at each sheet
           * HasHeaders -- Whether Sheet has header row. True/False
           * ReqRowNos -- Whether Excel Data Row no required. [So that we can show error message with @Rowno for end user]
           * ColDataType -- Columns Data Type for each sheet.
           * ReqCols -- ColumnDesc to be read from each sheet. [If sheet1 has 10 columns, specify column desc which are to be read. can skip unwanted columns]
           * InsQrys -- Insert querry against read excel data for each sheet.
           * SheetNos / SkipRows / HasHeaders / ReqCols / InsQrys --> Length must match
       */
        DBErrBuf = "";
        int Row = 0;
        try
        {
            int ret, i, k;
            int[] Cols = new int[1];
            DateTime dtDateVal;
            string buf, tmp;
            ArrayList qryLst = new ArrayList();

            ret = i = k = Row = 0;
            buf = tmp = "";
            qryLst.Clear();

            if (SheetNos == null || SheetNos.Length == 0)
                SheetNos = new int[] { 1 };

            if (SkipRows == null || SkipRows.Length == 0)
                SkipRows = Enumerable.Repeat(0, SheetNos.Length).ToArray();

            if (SheetNos.Length != SkipRows.Length || SheetNos.Length != HasHeader.Length || (ReqCols != null && ReqCols.Length != SheetNos.Length) ||
                    (ReqCols != null && ColDataType != null && ColDataType.Length != SheetNos.Length))
            {
                DBErrBuf = "MA[27]: Sheets / Skip Rows / Header Column Mismatch";
                return null;
            }

            if (ReqCols != null && ColDataType != null)
            {
                for (i = 0; i < ReqCols.Length; i++)
                    if (ReqCols[i].Length != ColDataType[i].Length)
                    {
                        DBErrBuf = "MA[28]: Columns & Data Types Mismatch";
                        return null;
                    }
            }

            using (var pck = new ExcelPackage(ExlFile.InputStream))
            {
                for (k = 0; k < SheetNos.Length; k++)
                {
                    DataTable dtCols = new DataTable();
                    i = SheetNos[k] == 0 ? 1 : SheetNos[k];
                    var ws = pck.Workbook.Worksheets[i];

                    Row = 1 + SkipRows[k];
                    Cols = Enumerable.Repeat(0, ws.Dimension.End.Column).ToArray();
                    foreach (var HdrCell in ws.Cells[Row, 1, Row, ws.Dimension.End.Column])
                        dtCols.Columns.Add(HasHeader[k] ? HdrCell.Text.Trim() : string.Format("Column {0}", HdrCell.Start.Column).Trim());

                    if (HasHeader[k])
                        Row++;

                    if (ReqCols != null)
                    {
                        for (i = 0; i < ReqCols[k].Length; i++)
                        {
                            ret = dtCols.Columns.IndexOf(ReqCols[k][i]);
                            if (ret == -1)
                            {
                                DBErrBuf = "MA[29]: '" + ReqCols[k][i] + "' Not Available in Sheet [" + pck.Workbook.Worksheets[SheetNos[k]].Name;
                                return null;
                            }

                            Cols[i] = ret + 1;
                        }

                        Cols = Cols.Where(x => x != 0).ToArray();
                        if (ReqCols[k].Length != Cols.Length)
                        {
                            DBErrBuf = "MA[30]: Required Columns Mismatch in Sheet [" + pck.Workbook.Worksheets[SheetNos[k]].Name;
                            return null;
                        }
                    }
                    else
                        Cols = Enumerable.Range(1, ws.Dimension.End.Column).ToArray();

                    for (; Row <= ws.Dimension.End.Row; Row++)
                    {
                        buf = tmp = "";
                        var wsRow = ws.Cells[Row, 1, Row, ws.Dimension.End.Column];
                        buf = InsQrys[k] + (ReqRowNos[k] ? Row + "," : "");
                        for (i = 0; i < Cols.Length; i++)
                        {
                            tmp = "";
                            tmp = ws.Cells[Row, Cols[i]].Value == null ? "" : ws.Cells[Row, Cols[i]].Value.ToString().Trim();
                            if (ReqCols != null & ColDataType != null && ColDataType[k][i] != EXCLRD_STRING)
                            {
                                if (ColDataType[k][i] == EXCLRD_INT || ColDataType[k][i] == EXCLRD_DECIMAL)
                                    tmp = tmp.Length == 0 ? "0" : tmp;
                                else if (ColDataType[k][i] == EXCLRD_DATE || ColDataType[k][i] == EXCLRD_TIME || ColDataType[k][i] == EXCLRD_DATETIME)
                                {
                                    if (tmp.Length == 0)
                                        tmp = "1900-01-01";
                                    dtDateVal = DateTime.Parse(tmp);
                                    tmp = ColDataType[k][i] == EXCLRD_DATE ? dtDateVal.ToString("yyyy-MM-dd") : ColDataType[k][i] == EXCLRD_TIME ?
                                                dtDateVal.ToString("HH:mm:ss") : dtDateVal.ToString("yyyy-MM-dd HH:mm:ss");
                                }
                            }
                            else
                                tmp = TrimExtraSpace(0, tmp);
                            buf += "'" + tmp.Replace("'", "''") + "',";
                        }
                        buf = buf.Substring(0, buf.Length - 1) + ")";
                        qryLst.Add(buf);
                    }

                } // end for
            }

            if (qryLst.Count == 0)
            {
                DBErrBuf = "MA[31]: No Data Found";
                return null;
            }

            return qryLst;
        }
        catch (Exception Ex)
        {
            DBErrBuf = "MC[30]: AT Row Number : " + Row + ", Error :" + Ex.Message;
            PrintLog("EXELGET", DBErrBuf);
            return null;
        }
    }


    //********************************************************* Excel Report*******************************************************

    private void Excel_AddData(ExcelWorksheet ws, int IncrementRow, ref int Row, int FCol, int TCol, bool FontBold, int FontSize,
                    ExcelHorizontalAlignment EAlign, int IsBorder, Color BgClr, int DataType, string Data, string Formulaqry)
    {
        try
        {
            var cell = ws.Cells[Row, FCol];

            if (Formulaqry.Length != 0)
                cell.Formula = Formulaqry;
            else if (Data.Length != 0 && FCol == TCol)
            {
                if (DataType == RPTSTYLE_INT)
                {
                    cell.Style.Numberformat.Format = "######0";
                    cell.Value = Convert.ToInt32(Data);
                }
                else if (DataType == RPTSTYLE_DECIMAL)
                {
                    cell.Style.Numberformat.Format = "##,##,##0.00";
                    cell.Value = Convert.ToDecimal(Data);
                }
                else if (DataType == RPTSTYLE_DECIMAL_ROUND)
                {
                    cell.Style.Numberformat.Format = "##,##,##0";
                    cell.Value = Convert.ToDecimal(Data);
                }
                else if (DataType == RPTSTYLE_DATE)
                {
                    cell.Style.Numberformat.Format = "DD-MM-YYYY";
                    cell.Value = Convert.ToDateTime(Data);
                }
                else
                    cell.Value = Convert.ToString(Data);
            }
            else
                cell.Value = Data;

            if (IsBorder == 1)
                if (FCol == TCol)
                    cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                else
                    ws.Cells[Row, FCol, Row, TCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);

            if (FCol != TCol)
                ws.Cells[Row, FCol, Row, TCol].Merge = true;
            if (FontBold == true)
                ws.Cells[Row, FCol, Row, TCol].Style.Font.Bold = true;
            if (FCol != TCol)
                ws.Cells[Row, FCol, Row, TCol].Style.HorizontalAlignment = EAlign;
            if (FontSize != 0)
                ws.Cells[Row, FCol, Row, TCol].Style.Font.Size = FontSize;

            if (BgClr != Color.Empty)
            {
                cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                cell.Style.Fill.BackgroundColor.SetColor(BgClr);
            }

            if (IncrementRow == 1)
                Row++;
        }
        catch { }
    }

    private int GenDynamicExcel(string RptName, string Heading, int BorderReq, int AutoSlNo, int[] ColDataType, string[] ColDesc, int[] RptSubTotCols,
                        int[] RptTotalCols, string FromDate, string ToDate, string[] AddlnData, int[] Row1MergeCols, string[] Row1ColDesc, DataTable dtRpt)
    {
        /* Return --> 0 Error, 1 Success  [  sUtilErrBuf --> Error - Msg, Success - Report Full Path  ]
         * RptName --> Name of the report. YYYYMMDDhhmmss + .xls/.pdf added with it
         * Heading --> Text to be printed as First Row
         * BorderReq --> If Excel ==> Border Required for Col Header, Data, Totals [ 0 Not Req,  1 Req ]
         * AutoSlNo --> Show # in all Data Row Columns. [1 - Yes, 0 - NA]
         * ColDataType --> Column's Data Type ==> Short String / String / Date [Left Align];   Int / Decimal [Right Align]
         * ColDesc --> Col Names in Document. If Null, Datatable column name will be used
         * RptSubTotCols --> Based on these columns, calculate sub total. [0 Not Req, 1 Group by this Col] / null --> Dont calculate
         * RptTotalCols --> Columns for which Total Need to be calculated [0 Not Req, 1 Total Req]   / null --> Dont Calculate
         * From/To Date --> Both Avail  : Report Period XX to XX
         *                  Only ToDate : Report Of : XX
         *                  Both Empty  : No Date Desc
         * AddlnData --> Additional Datas need to printed in Excel. Null - No Data Avail
         * Row1MergeCols --> Apart from header, if addtiional header (above dt header) need to shown with merging cols count  / null --> Dont Merge
         * Row1ColDesc --> Apart from header, if addtional header (above dt header) need to shown with merging cols Desc / null --> Dont Merge
         */

        DBErrBuf = "";
        try
        {
            int i, col, row, TotStCol, TotStRow, SubTotStRow, PrtSubTot;
            int[] LColTot = new int[ColDataType.Length];
            int[] LColSubTot = new int[ColDataType.Length];

            i = col = row = TotStCol = TotStRow = SubTotStRow = PrtSubTot = 0;
            Array.Clear(LColTot, 0, LColTot.Length);
            Array.Clear(LColSubTot, 0, LColSubTot.Length);

            if (RptName.Length == 0)
            {
                DBErrBuf = "MA[32]: Invalid File Name";
                return 0;
            }

            LColTot = Enumerable.Range(0, dtRpt.Columns.Count).Select(n => 0).ToArray();
            LColSubTot = Enumerable.Range(0, dtRpt.Columns.Count).Select(n => 0).ToArray();
            if (RptTotalCols != null)
                LColTot = RptTotalCols;
            if (RptSubTotCols != null)
                LColSubTot = RptSubTotCols;

            if (ColDataType.Length != dtRpt.Columns.Count || ColDataType.Length != LColTot.Length || ColDataType.Length != LColSubTot.Length ||
                    (ColDesc != null && ColDesc.Length != ColDataType.Length))
            {
                DBErrBuf = "MA[33]: Invalid Report Columns";
                return 0;
            }

            if (Row1MergeCols != null)
            {
                if (Row1MergeCols.Length != Row1ColDesc.Length || !(Row1MergeCols.Length <= ColDataType.Length) ||
                            !(Row1ColDesc.Length <= ColDataType.Length))
                {
                    DBErrBuf = "MA[34]: Invalid Report Columns";
                    return 0;
                }

                for (i = row = 0; i < Row1MergeCols.Length; i++)
                    row += Row1MergeCols[i];

                if (row != ColDataType.Length)
                {
                    DBErrBuf = "MA[35]: Invalid Report Columns";
                    return 0;
                }
            }

            row = col = 1;
            using (ExcelPackage ExlPack = new ExcelPackage())
            {
                //Here setting some document properties
                ExlPack.Workbook.Properties.Author = "Origin Technology Associates";
                ExlPack.Workbook.Properties.Title = Globals.gsAppName;

                //Create a sheet
                ExlPack.Workbook.Worksheets.Add(Heading);
                ExcelWorksheet ws = ExlPack.Workbook.Worksheets[1];
                ws.Name = "Sheet";     //Setting Sheet's name
                ws.Cells.Style.Font.Size = 11; //Default font size for whole sheet
                ws.Cells.Style.Font.Name = "Calibri"; //Default Font name for whole sheet

                // Aligning Header Details
                if (Globals.gsAppName.Length != 0)
                    Excel_AddData(ws, 1, ref row, 1, dtRpt.Columns.Count + AutoSlNo, true, 18, ExcelHorizontalAlignment.Center, 0, Color.Empty, 0, Globals.gsAppName, "");
                if (Heading.Length != 0)
                    Excel_AddData(ws, 1, ref row, 1, dtRpt.Columns.Count + AutoSlNo, true, 0, ExcelHorizontalAlignment.Center, 0, Color.Empty, 0, Heading, "");
                row++;
                Excel_AddData(ws, 1, ref row, 1, dtRpt.Columns.Count + AutoSlNo, true, 0, ExcelHorizontalAlignment.Right, 0, Color.Empty, 0,
                            "Generated On:" + System.DateTime.Now.ToString("dd-MM-yyyy HH:mm"), "");
                if (FromDate.Length != 0 || ToDate.Length != 0)
                    Excel_AddData(ws, 1, ref row, 1, dtRpt.Columns.Count + AutoSlNo, true, 0, ExcelHorizontalAlignment.Left, 0, Color.Empty,
                                0, "Report of : " + FromDate + (ToDate.Length != 0 && FromDate != ToDate ? "  To  " + ToDate : ""), "");
                if (AddlnData != null && AddlnData.Length != 0)
                {
                    row++;
                    for (i = 0; i < AddlnData.Length; i++)
                        if (AddlnData[i] != null && AddlnData[i].Length != 0)
                            Excel_AddData(ws, 1, ref row, 1, dtRpt.Columns.Count + AutoSlNo, true, 0, ExcelHorizontalAlignment.Left, 0, Color.Empty, 0, AddlnData[i], "");
                }

                if (Row1MergeCols != null)
                {
                    row++;
                    for (i = 0, col = AutoSlNo + 1; i < Row1MergeCols.Length; i++)
                    {
                        Excel_AddData(ws, 0, ref row, col, col + AutoSlNo + Row1MergeCols[i] - 1, true, 0, ExcelHorizontalAlignment.Center, BorderReq,
                                Color.LightGray, RPTSTYLE_STRING, Row1ColDesc[i], "");
                        col += Row1MergeCols[i];
                    }
                }

                row++;
                if (AutoSlNo == 1)
                    Excel_AddData(ws, 0, ref row, 1, 1, true, 0, ExcelHorizontalAlignment.Center, BorderReq, Color.LightGray, RPTSTYLE_STRING, "#", "");
                for (col = 0; col < dtRpt.Columns.Count; col++)
                    Excel_AddData(ws, 0, ref row, col + AutoSlNo + 1, col + AutoSlNo + 1, true, 0, ExcelHorizontalAlignment.Center, BorderReq, Color.LightGray,
                                RPTSTYLE_STRING, ColDesc != null ? ColDesc[col] : dtRpt.Columns[col].ColumnName, "");
                row++;

                // Check Whether Total need to be shown
                TotStCol = TotStRow = SubTotStRow = 0;
                if (RptTotalCols != null)
                {
                    for (i = col = 0; i < LColTot.Length; i++)
                        col += LColTot[i] == 1 ? 1 : 0;
                    if (col != 0)
                    {
                        TotStRow = row;
                        for (i = TotStCol = 0; i < LColTot.Length; i++)
                        {
                            if (LColTot[i] == 1)
                                break;
                            TotStCol++;
                        }

                        // Check Whether Sub Total need to be shown
                        if (RptSubTotCols != null)
                        {
                            for (i = col = 0; i < LColSubTot.Length; i++)
                                col += (LColSubTot[i] == 1 ? 1 : 0);
                            SubTotStRow = col == 0 ? 0 : row;
                        }
                    }
                }


                for (i = 0; i < dtRpt.Rows.Count; i++) // Adding Data into rows
                {
                    if (SubTotStRow != 0 && i != 0)
                    {
                        for (col = 0; col < TotStCol; col++)
                            if (LColSubTot[col] == 1 && dtRpt.Rows[i - 1][col].ToString() != dtRpt.Rows[i][col].ToString()) // cols not equal, print sub total
                            {
                                PrtSubTot = 1;
                                break;
                            }

                        if (PrtSubTot == 1)
                        {
                            Excel_AddData(ws, 0, ref row, 1, TotStCol + AutoSlNo, true, 13, ExcelHorizontalAlignment.Center, BorderReq, Color.LightGray,
                                        0, "Sub-Total :", "");
                            for (col = TotStCol; col < dtRpt.Columns.Count; col++)
                                Excel_AddData(ws, 0, ref row, col + AutoSlNo + 1, col + AutoSlNo + 1, true, 13, ExcelHorizontalAlignment.Right, BorderReq,
                                        Color.LightGray, ColDataType[col], "", LColSubTot[col] == 0 ? "" :
                                        "SUBTOTAL(9," + ws.Cells[SubTotStRow, col + 1].Address + ":" + ws.Cells[row - 1, col + 1].Address + ")");
                            row += 2;
                            SubTotStRow = row;
                        }
                    }

                    PrtSubTot = 0;
                    if (AutoSlNo == 1)
                        Excel_AddData(ws, 0, ref row, 1, 1, false, 0, ExcelHorizontalAlignment.General, BorderReq, Color.Empty, RPTSTYLE_INT, (i + 1).ToString(), "");
                    for (col = 0; col < dtRpt.Columns.Count; col++)
                        Excel_AddData(ws, 0, ref row, col + AutoSlNo + 1, col + AutoSlNo + 1, false, 0, ExcelHorizontalAlignment.General, BorderReq, Color.Empty,
                                ColDataType[col], dtRpt.Rows[i][col].ToString(), "");
                    row++;
                }

                if (TotStRow != 0)
                {
                    if (SubTotStRow != 0)
                    {
                        Excel_AddData(ws, 0, ref row, 1, TotStCol + AutoSlNo, true, 13, ExcelHorizontalAlignment.Center, BorderReq, Color.LightGray,
                                              0, "Sub-Total :", "");
                        for (col = TotStCol; col < dtRpt.Columns.Count; col++)
                            Excel_AddData(ws, 0, ref row, col + AutoSlNo + 1, col + AutoSlNo + 1, true, 13, ExcelHorizontalAlignment.Right, BorderReq,
                                    Color.LightGray, ColDataType[col], "", LColSubTot[col] == 0 ? "" :
                                    "SUBTOTAL(9," + ws.Cells[SubTotStRow, col + AutoSlNo + 1].Address + ":" + ws.Cells[row - 1, col + AutoSlNo + 1].Address + ")");
                        row += 2;
                    }

                    Excel_AddData(ws, 0, ref row, 1, TotStCol + AutoSlNo, true, 13, ExcelHorizontalAlignment.Center, BorderReq, Color.FromArgb(173, 173, 173),
                                0, "Total :", "");
                    for (col = TotStCol; col < dtRpt.Columns.Count; col++)
                        Excel_AddData(ws, 0, ref row, col + AutoSlNo + 1, col + AutoSlNo + 1, true, 13, ExcelHorizontalAlignment.Right, BorderReq,
                                Color.FromArgb(173, 173, 173), ColDataType[col], "", LColTot[col] == 0 ? "" :
                                "SUBTOTAL(9," + ws.Cells[TotStRow, col + AutoSlNo + 1].Address + ":" + ws.Cells[row - 1, col + AutoSlNo + 1].Address + ")");
                }

                ws.Cells[ws.Dimension.Address].AutoFitColumns();
                Byte[] bin = ExlPack.GetAsByteArray();
                File.WriteAllBytes(RptName, bin);
                DBErrBuf = RptName;
                return 1;
            }
        }
        catch (Exception Ex)
        {
            DBErrBuf = "MC[31]: " + Ex.Message;
            PrintLog("GDExl Exception", DBErrBuf);
            return 0;
        }
    }


    //*********************************************************PDF Report*******************************************************


    private void Pdf_AddData(int ColSpan, int RowSpan, int FntSize, string BorderLRTB, int CSpanHAlign, int DataType, float PadLeft,
                        string Data, PdfPTable PdfTbls)
    {
        try
        {
            string buf = "";

            iTextSharp.text.Font fntSel;
            if (FntSize == PDF_FONT_NORMAL_BOLD)
                fntSel = FontFactory.GetFont(FontFactory.TIMES_BOLD, 11); // ,, iTextSharp.text.Font.UNDERLINE
            else if (FntSize == PDF_FONT_SMALL)
                fntSel = FontFactory.GetFont(FontFactory.TIMES, 9);
            else if (FntSize == PDF_FONT_SMALL_BOLD)
                fntSel = FontFactory.GetFont(FontFactory.TIMES_BOLD, 9);
            else if (FntSize == PDF_FONT_HIGH)
                fntSel = FontFactory.GetFont(FontFactory.TIMES, 13);
            else if (FntSize == PDF_FONT_HIGH_BOLD)
                fntSel = FontFactory.GetFont(FontFactory.TIMES_BOLD, 13);
            else
                fntSel = FontFactory.GetFont(FontFactory.TIMES, 11);

            if (ColSpan != 0)
                buf = Data;
            else
            {
                if (DataType == RPTSTYLE_DECIMAL)
                    buf = string.Format("{0:0.##}", Convert.ToDecimal(Data));
                else if (DataType == RPTSTYLE_DECIMAL_ROUND)
                    buf = string.Format("{0:0}", Convert.ToDecimal(Data));
                else if (DataType == RPTSTYLE_INT)
                    buf = string.Format("{0:0}", Convert.ToInt16(Data));
                else
                    buf = Data;
            }

            PdfPCell pclCol = new PdfPCell(new Phrase(buf, fntSel));
            if (ColSpan >= 2)
                pclCol.Colspan = ColSpan;
            if (RowSpan >= 2)
                pclCol.Rowspan = RowSpan;

            if (BorderLRTB.Length != 0)
            {
                pclCol.BorderWidthLeft = BorderLRTB[0] - '0' == 1 ? .5f : 0;
                pclCol.BorderWidthRight = BorderLRTB.Length >= 2 ? BorderLRTB[1] - '0' == 1 ? .5f : 0 : 0;
                pclCol.BorderWidthTop = BorderLRTB.Length >= 3 ? BorderLRTB[2] - '0' == 1 ? .5f : 0 : 0;
                pclCol.BorderWidthBottom = BorderLRTB.Length >= 4 ? BorderLRTB[3] - '0' == 1 ? .5f : 0 : 0;
            }
            else
                pclCol.Border = 0;

            pclCol.PaddingLeft = PadLeft;
            pclCol.HorizontalAlignment = ColSpan != 0 ? CSpanHAlign : DataType == RPTSTYLE_INT || DataType == RPTSTYLE_DECIMAL ||
                                            DataType == RPTSTYLE_DECIMAL_ROUND ? Element.ALIGN_RIGHT : Element.ALIGN_LEFT;
            PdfTbls.AddCell(pclCol);
        }
        catch { }
    }


    private void Pdf_AddImage(int ColSpan, int RowSpan, string BorderLRTB, iTextSharp.text.Image Img, PdfPTable PdfTbls)
    {
        try
        {
            if (Img == null)
            {
                Pdf_AddData(ColSpan, RowSpan, PDF_FONT_NORMAL, BorderLRTB, Element.ALIGN_LEFT, RPTSTYLE_STRING, 5f, "", PdfTbls);
                return;
            }

            PdfPCell pclCol = new PdfPCell(Img);
            if (ColSpan >= 2)
                pclCol.Colspan = ColSpan;
            if (RowSpan >= 2)
                pclCol.Rowspan = RowSpan;
            if (BorderLRTB.Length != 0)
            {
                pclCol.BorderWidthLeft = BorderLRTB[0] - '0' == 1 ? .5f : 0;
                pclCol.BorderWidthRight = BorderLRTB.Length >= 2 ? BorderLRTB[1] - '0' == 1 ? .5f : 0 : 0;
                pclCol.BorderWidthTop = BorderLRTB.Length >= 3 ? BorderLRTB[2] - '0' == 1 ? .5f : 0 : 0;
                pclCol.BorderWidthBottom = BorderLRTB.Length >= 4 ? BorderLRTB[3] - '0' == 1 ? .5f : 0 : 0;
            }
            else
                pclCol.Border = 0;
            pclCol.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfTbls.AddCell(pclCol);
        }
        catch { }
    }


    private void Pdf_AddNewLine(int ColSpan, int RowSpan, string BorderLRTB, PdfPTable PdfTbls)
    {
        try
        {
            PdfPCell pclCol = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontFactory.TIMES, 9)));
            if (ColSpan >= 2)
                pclCol.Colspan = ColSpan;
            if (RowSpan >= 2)
                pclCol.Rowspan = RowSpan;
            if (BorderLRTB.Length != 0)
            {
                pclCol.BorderWidthLeft = BorderLRTB[0] - '0' == 1 ? .5f : 0;
                pclCol.BorderWidthRight = BorderLRTB.Length >= 2 ? BorderLRTB[1] - '0' == 1 ? .5f : 0 : 0;
                pclCol.BorderWidthTop = BorderLRTB.Length >= 3 ? BorderLRTB[2] - '0' == 1 ? .5f : 0 : 0;
                pclCol.BorderWidthBottom = BorderLRTB.Length >= 4 ? BorderLRTB[3] - '0' == 1 ? .5f : 0 : 0;
            }
            else
                pclCol.Border = 0;
            PdfTbls.AddCell(pclCol);
        }
        catch { }
    }


    private int GenDynamicPdf(string RptName, string Heading, int IsLandscape, int AutoSlNo, int[] ColDataType, string[] ColDesc, int[] RptSubTotCols,
                        int[] RptTotalCols, string FromDate, string ToDate, string[] AddlnData, int[] Row1MergeCols, string[] Row1ColDesc, DataTable dtRpt)
    {
        /* Return --> 0 Error, 1 Success   [ sUtilErrBuf --> Error - Msg, Success - Report Full Path  ] 
         * RptName --> Name of the report. YYYYMMDDhhmmss + .xls/.pdf added with it
         * Heading --> Text to be printed as First Row
         * IsLandscape --> If Pdf ==> 1 Landscape mode, 0 Portrait Mode
         * AutoSlNo --> Show # in all Data Row Columns. [1 - Yes, 0 - NA]
         * ColDataType --> Column's Data Type ==> Short String / String / Date [Left Align];   Int / Decimal [Right Align]
         * ColDesc --> Col Names in Document. If Null, Datatable column name will be used
         * RptSubTotCols --> Based on these columns, calculate sub total. [0 Not Req, 1 Group by this Col] / null --> Dont calculate
         * RptTotalCols --> Columns for which Total Need to be calculated [0 Not Req, 1 Total Req]   / null --> Dont Calculate
         * From/To Date --> Both Avail  : Report Period XX to XX
         *                  Only ToDate : Report Of : XX
         *                  Both Empty  : No Date Desc
         * AddlnData --> Additional Datas need to printed in pdf. Null - No Data Avail
         * Row1MergeCols --> Apart from header, if addtiional header (above dt header) need to shown with merging cols count  / null --> Dont Merge
         * Row1ColDesc --> Apart from header, if addtional header (above dt header) need to shown with merging cols Desc / null --> Dont Merge
         */

        DBErrBuf = "";
        try
        {
            int i, j, k, Cols, TotColSpan, TotReq, SubTotReq, PrtSubTot;
            float[] PdfColWdt;
            int[] LColTot;
            int[] LColSubTot;
            double[] dTotal;
            double[] dSubTot;
            PdfPTable pdfTblHdl = new PdfPTable(dtRpt.Columns.Count + AutoSlNo);

            i = j = k = Cols = TotColSpan = TotReq = SubTotReq = PrtSubTot = 0;

            if (RptName.Length == 0)
            {
                DBErrBuf = "MA[36]: Invalid Report Path";
                PrintLog("GPdf", DBErrBuf);
                return 0;
            }

            PdfColWdt = Enumerable.Range(0, dtRpt.Columns.Count + AutoSlNo).Select(n => 8f).ToArray();
            LColTot = Enumerable.Range(0, dtRpt.Columns.Count).Select(n => 0).ToArray();
            LColSubTot = Enumerable.Range(0, dtRpt.Columns.Count).Select(n => 0).ToArray();
            dTotal = Enumerable.Range(0, dtRpt.Columns.Count).Select(n => 0.00).ToArray();
            dSubTot = Enumerable.Range(0, dtRpt.Columns.Count).Select(n => 0.00).ToArray();

            if (RptTotalCols != null)
                LColTot = RptTotalCols;
            if (RptSubTotCols != null)
                LColSubTot = RptSubTotCols;

            if (ColDataType.Length != dtRpt.Columns.Count || LColTot.Length != dtRpt.Columns.Count || LColSubTot.Length != dtRpt.Columns.Count ||
                    (ColDesc != null && ColDesc.Length != ColDataType.Length))
            {
                DBErrBuf = "MA[37]: Invalid Report Columns";
                PrintLog("GPdf", DBErrBuf);
                return 0;
            }

            if (Row1MergeCols != null)
            {
                if (Row1MergeCols.Length != Row1ColDesc.Length || !(Row1MergeCols.Length <= ColDataType.Length) ||
                            !(Row1ColDesc.Length <= ColDataType.Length))
                {
                    DBErrBuf = "MA[38]: Invalid Report Columns";
                    PrintLog("GPdf", DBErrBuf);
                    return 0;
                }

                for (i = j = 0; i < Row1MergeCols.Length; i++)
                    j += Row1MergeCols[i];

                if (j != ColDataType.Length)
                {
                    DBErrBuf = "MA[39]: Invalid Report Columns";
                    PrintLog("GPdf", DBErrBuf);
                    return 0;
                }
            }

            k = 0;
            if (AutoSlNo == 1)
                PdfColWdt[k++] = 3f;
            for (i = 0; i < dtRpt.Columns.Count; i++, k++)
                PdfColWdt[k] = ColDataType[i] == RPTSTYLE_INT ? 3f : ColDataType[i] == RPTSTYLE_DECIMAL || ColDataType[i] == RPTSTYLE_DECIMAL_ROUND ? 4.5f :
                                    ColDataType[i] == RPTSTYLE_DATE || ColDataType[i] == RPTSTYLE_STRING_SHORT ? 5.5f : 9f;

            Cols = PdfColWdt.Length;
            pdfTblHdl.WidthPercentage = 100;
            pdfTblHdl.SetWidths(PdfColWdt);
            pdfTblHdl.HorizontalAlignment = 1;
            pdfTblHdl.SpacingBefore = 20f;
            pdfTblHdl.SpacingAfter = 30f;

            if (File.Exists(RptName) == true)
                File.Delete(RptName);

            Document Dcmnt = new Document(PageSize.A4, 25, 20, 25, 20);
            if (IsLandscape == 1)
                Dcmnt.SetPageSize(PageSize.A4.Rotate());

            PdfWriter pdfwr = PdfWriter.GetInstance(Dcmnt, new FileStream(RptName, FileMode.Create));
            Dcmnt.Open();
            Pdf_AddData(Cols, 1, PDF_FONT_HIGH_BOLD, "", Element.ALIGN_CENTER, RPTSTYLE_STRING, 6f, Globals.gsAppName, pdfTblHdl);

            if (Heading.Length != 0)
                Pdf_AddData(Cols, 1, PDF_FONT_HIGH, "", Element.ALIGN_CENTER, RPTSTYLE_STRING, 6f, Heading, pdfTblHdl);
            Pdf_AddNewLine(Cols, 1, "", pdfTblHdl);

            Pdf_AddData(Cols, 1, PDF_FONT_NORMAL, "", Element.ALIGN_RIGHT, RPTSTYLE_STRING, 5f, "Report Generated On : " +
                        System.DateTime.Now.ToString("dd-MM-yyyy HH:mm"), pdfTblHdl);
            if (FromDate.Length != 0 || ToDate.Length != 0)
                Pdf_AddData(Cols, 1, PDF_FONT_NORMAL, "", Element.ALIGN_LEFT, RPTSTYLE_STRING, 5f, "Report Of : " + FromDate +
                                    (ToDate.Length != 0 && FromDate != ToDate ? "  To  " + ToDate : ""), pdfTblHdl);

            if (AddlnData != null && AddlnData.Length != 0)
            {
                for (i = 0; i < AddlnData.Length; i++)
                {
                    if (AddlnData[i] == null || AddlnData[i].Length == 0)
                        continue;
                    Pdf_AddData(Cols, 1, PDF_FONT_NORMAL, "", Element.ALIGN_LEFT, RPTSTYLE_STRING, 5f, AddlnData[i], pdfTblHdl);
                }
            }
            Pdf_AddNewLine(Cols, 1, "", pdfTblHdl);

            // Check Whether Total need to be shown
            TotReq = SubTotReq = 0;
            if (RptTotalCols != null)
                for (i = TotReq = 0; i < LColTot.Length; i++)
                    TotReq += LColTot[i] == 1 ? 1 : 0;

            if (TotReq != 0)
            {
                TotReq = 1;
                for (i = TotColSpan = 0; i < LColTot.Length; i++)
                {
                    if (LColTot[i] == 1)
                        break;
                    TotColSpan++;
                }

                // Check Whether Sub Total need to be shown
                if (RptSubTotCols != null)
                {
                    for (i = SubTotReq = 0; i < LColSubTot.Length; i++)
                        SubTotReq += LColSubTot[i] == 1 ? 1 : 0;
                    SubTotReq = SubTotReq == 0 ? 0 : 1;
                }
            }

            if (Row1MergeCols != null)      // Merger Row Header
            {
                for (i = 0; i < Row1MergeCols.Length; i++)
                    Pdf_AddData(Row1MergeCols[i] + AutoSlNo, 1, PDF_FONT_NORMAL_BOLD, "1111", Element.ALIGN_CENTER, RPTSTYLE_STRING, 5f, Row1ColDesc[i], pdfTblHdl);
            }

            if (AutoSlNo == 1)
                Pdf_AddData(0, 1, PDF_FONT_NORMAL_BOLD, "1111", Element.ALIGN_CENTER, RPTSTYLE_STRING, 5f, "#", pdfTblHdl);
            for (i = 0; i < dtRpt.Columns.Count; i++) // Column Heading
                Pdf_AddData(0, 1, PDF_FONT_NORMAL_BOLD, "1111", Element.ALIGN_CENTER, RPTSTYLE_STRING, 5f, ColDesc != null ? ColDesc[i] :
                        dtRpt.Columns[i].ColumnName, pdfTblHdl);

            // Data
            for (i = PrtSubTot = 0; i < dtRpt.Rows.Count; i++)
            {
                if (SubTotReq == 1 && i != 0)
                {
                    for (j = 0; j < TotColSpan; j++)
                        if (LColSubTot[j] == 1 && dtRpt.Rows[i - 1][j].ToString() != dtRpt.Rows[i][j].ToString()) // cols not equal, print sub total
                        {
                            PrtSubTot = 1;
                            break;
                        }

                    if (PrtSubTot == 1)
                    {
                        Pdf_AddData(TotColSpan + AutoSlNo, 1, PDF_FONT_NORMAL, "1111", Element.ALIGN_RIGHT, RPTSTYLE_STRING, 5f, "Sub-Total", pdfTblHdl);
                        for (j = TotColSpan; j < Cols; j++)
                            Pdf_AddData(AutoSlNo, 1, PDF_FONT_NORMAL, "1111", Element.ALIGN_RIGHT, ColDataType[i], 5f,
                                            LColSubTot[j] == 1 ? dSubTot[j].ToString() : "", pdfTblHdl);
                        Pdf_AddNewLine(Cols, 1, "1100", pdfTblHdl);
                        Array.Clear(dSubTot, 0, dTotal.Length);
                    }
                }

                PrtSubTot = 0;
                if (AutoSlNo == 1)
                    Pdf_AddData(0, 1, PDF_FONT_NORMAL, "1111", Element.ALIGN_RIGHT, RPTSTYLE_INT, 5f, (i + 1).ToString(), pdfTblHdl);
                for (j = 0; j < dtRpt.Columns.Count; j++)
                {
                    Pdf_AddData(0, 1, PDF_FONT_NORMAL, "1111", Element.ALIGN_RIGHT, ColDataType[j], 5f, dtRpt.Rows[i][j].ToString(), pdfTblHdl);
                    if (TotReq == 1 && LColTot[j] == 1)
                    {
                        dSubTot[j] += Convert.ToDouble(dtRpt.Rows[i][j]);
                        dTotal[j] += Convert.ToDouble(dtRpt.Rows[i][j]);
                    }
                }
            }   // end for

            if (TotReq == 1)
            {
                if (SubTotReq == 1)
                {
                    Pdf_AddData(TotColSpan + AutoSlNo, 1, PDF_FONT_NORMAL, "1111", Element.ALIGN_RIGHT, RPTSTYLE_STRING, 5f, "Sub-Total", pdfTblHdl);
                    for (j = TotColSpan; j < Cols; j++)
                        Pdf_AddData(0, 1, PDF_FONT_NORMAL, "1111", Element.ALIGN_RIGHT, ColDataType[j], 5f, LColSubTot[j] == 1 ? dSubTot[j].ToString() : "", pdfTblHdl);
                    Pdf_AddNewLine(Cols, 1, "1100", pdfTblHdl);
                }

                Pdf_AddData(TotColSpan + AutoSlNo, 1, PDF_FONT_NORMAL_BOLD, "1111", Element.ALIGN_RIGHT, RPTSTYLE_STRING, 5f, "Total", pdfTblHdl);
                for (j = TotColSpan; j < Cols - AutoSlNo; j++)
                    Pdf_AddData(0, 1, PDF_FONT_NORMAL_BOLD, "1111", Element.ALIGN_RIGHT, ColDataType[j], 5f, LColTot[j] == 1 ? dTotal[j].ToString() : "", pdfTblHdl);
            }

            Dcmnt.Add(pdfTblHdl);
            Dcmnt.Close();
            DBErrBuf = RptName;
            return 1;
        }
        catch (Exception Ex)
        {
            DBErrBuf = "MC[32]: " + Ex.Message;
            PrintLog("GPdf Exception", DBErrBuf);
            return 0;
        }
    }


    public int GenerateReport(int DwnldFmt, int PdfLandscape, int ExlIsBorder, int AutoSlNo, string RptName, string Heading, int[] ColDataType, string[] ColDesc,
                    int[] RptSubTotCols, int[] RptTotalCols, string FromDate, string ToDate, string[] AddlnData, int[] Row1MergeCols, string[] Row1ColDesc,
                    DataTable dtRpt)
    {
        /* Return --> 0 Error, 1 Success   [ sUtilErrBuf --> Error - Msg, Success - Report Full Path  ] 
         * DwnldFmt --> Excel / Pdf
         * PdfLandscape --> If Pdf ==> 1 Landscape mode, 0 Portrait Mode
         * ExlIsBorder --> If Excel ==> Border Required for Col Header, Data, Totals [ 0 Not Req,  1 Req ]
         * AutoSlNo --> Show # in all Data Row Columns. [1 - Yes, 0 - NA]
         * RptName --> Name of the report. YYYYMMDDhhmmss + .xls/.pdf added with it
         * Heading --> Text to be printed as First Row
         * ColDataType --> Column's Data Type ==> Short String / String / Date [Left Align];   Int / Decimal [Right Align]
         * ColDesc --> Col Names in Document. If Null, Datatable column name will be used
         * RptSubTotCols --> Based on these columns, calculate sub total. [0 Not Req, 1 Group by this Col] / null --> Dont calculate
         * RptTotalCols --> Columns for which Total Need to be calculated [0 Not Req, 1 Total Req]   / null --> Dont Calculate
         * From/To Date --> Both Avail  : Report Period XX to XX
         *                  Only ToDate : Report Of : XX
         *                  Both Empty  : No Date Desc
         * AddlnData --> Additional Datas need to printed in Document. Null - No Data Avail
         * Row1MergeCols --> Apart from header, if addtiional header (above dt header) need to shown with merging cols count  / null --> Dont Merge
         * Row1ColDesc --> Apart from header, if addtional header (above dt header) need to shown with merging cols Desc / null --> Dont Merge
         */

        DBErrBuf = "";
        try
        {
            int ret = 0;
            string RptPath = "";

            RptName = RptName.Trim();
            if (RptName.Length == 0)
            {
                DBErrBuf = "MA[40]: Invalid Report Name";
                PrintLog("GenRpt", DBErrBuf);
                return 0;
            }

            RptName = Globals.RPTPATH + "/" + RptName + (RptName.EndsWith("_") == true ? "" : "_") + DateTime.Now.ToString("yyyyMMddHHmmss") +
                                    (DwnldFmt == Globals.DWNLDFMT_EXCEL ? ".xlsx" : ".pdf");
            RptPath = AppDomain.CurrentDomain.BaseDirectory + "/" + RptName;

            if (File.Exists(RptPath))
                File.Delete(RptPath);

            AutoSlNo = AutoSlNo == 1 ? 1 : 0;
            if (DwnldFmt == Globals.DWNLDFMT_EXCEL)
                ret = GenDynamicExcel(RptPath, Heading, ExlIsBorder, AutoSlNo, ColDataType, ColDesc, RptSubTotCols, RptTotalCols, FromDate, ToDate, AddlnData,
                                    Row1MergeCols, Row1ColDesc, dtRpt);
            else
                ret = GenDynamicPdf(RptPath, Heading, PdfLandscape, AutoSlNo, ColDataType, ColDesc, RptSubTotCols, RptTotalCols, FromDate, ToDate, AddlnData,
                                    Row1MergeCols, Row1ColDesc, dtRpt);
            if (ret == 1)
                DBErrBuf = RptName;
            return ret;
        }
        catch (Exception Ex)
        {
            DBErrBuf = "MC[33]: " + Ex.Message;
            PrintLog("GenRpt Exception", DBErrBuf);
            return 0;
        }
    }





    // ******************* Text Input Validation ***********************************************
    public string TrimExtraSpace(int SkipNewLine, string Src)
    {
        // Trim Mulitple White Space / TAB intend / New Line chars
        // SkipNewLine -- 1 Skip New Line, 0 NA
        DBErrBuf = "";
        try
        {
            char[] whitespace = new char[] { ' ', '\t', '\r', '\f', '\v', '\n' };
            if (SkipNewLine == 1)
                Array.Resize(ref whitespace, whitespace.Length - 1);
            string buf = string.Join(" ", Src.Split(whitespace, StringSplitOptions.RemoveEmptyEntries));
            return buf;
        }
        catch (Exception Ex)
        {
            DBErrBuf = "MC[34]: " + Ex.Message;
            PrintLog("TmExSpc Exception", DBErrBuf);
            return Src;
        }
    }


    public int IsValidEmail(string src)
    {
        // Return --> 1 Valid, 0 Error
        DBErrBuf = "";
        try
        {
            int i = 0, j = 0;
            //string Pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"+ @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            Regex rx = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", RegexOptions.IgnoreCase);

            if (src == null || src.Length == 0)
            {
                DBErrBuf = "MA[41]: Input Valid Email-ID";
                return 0;
            }

            string[] Data = src.Split(';');
            if (Data.Length == 0)
            {
                DBErrBuf = "MA[42]: Input Valid Email-ID";
                return 0;
            }

            for (i = 0; i < Data.Length; i++)
            {
                if (rx.IsMatch(Data[i]) == false)
                {
                    DBErrBuf = "MA[43]: Invalid Email-ID [" + Data[i] + "]";
                    return 0;
                }
                else if (!((Data[i][0] >= '0' && Data[i][0] <= '9') || (Data[i][0] >= 'a' && Data[i][0] <= 'z') || (Data[i][0] >= 'A' && Data[i][0] <= 'Z')))
                {
                    DBErrBuf = "MA[11]: Invalid Email-ID [" + Data[i] + "]";
                    return 0;
                }
            }

            for (i = 0; i < Data.Length; i++)
            {
                for (j = i + 1; j < Data.Length; j++)
                {
                    if (Data[i].ToUpper().CompareTo(Data[j].ToUpper()) == 0)
                    {
                        DBErrBuf = "MA[44]: Email-ID Duplicated for [" + Data[i] + "]";
                        return 0;
                    }
                }
            }

            return 1;
        }
        catch (Exception Ex)
        {
            DBErrBuf = "MC[35]: " + Ex.Message;
            PrintLog("ValEmail Exception", DBErrBuf);
            return 0;
        }
    }




    public int Text_ChkInputs(int[] AcptNull, ref string[] txtIps, string[] ErrMsg, int[] ChkVal, int[] MinLen, int[] MaxLen, int[] AllowNewLine)
    {
        /* Check whether Data Inputted are in correct format. Trims Extra Spaces & bypass singlequotes
            * Return --> 0 Error/Invalid, 1 Success
            * AcptNull -- Whether Ip Data is NULL. 1 - Accept Null & convert as empty string, 0 Throws error, if data is null
            * txtIps -- Ip Data to be checked
            * ErrMsg -- Ip Data Caption to be shown, if data is not valid.
            * ChkVal -- Ip Data Format
            * MinLen / MaxLen -- Ip Data Min / Max Length. Min/Max = 0 : wont check
            * AllowNewLine -- Allow new line in data.   1  Allowed;  0 NA
         */
        DBErrBuf = "";
        try
        {
            int i = 0;
            bool v = false;
            string buf, tmp;
            string[] VldMsg = { "", "", "All Zerors Not Allowed", "All Zerors Not Allowed", "Only 0-9A-F allowed", "Only 0-9A-F allowed", "Only 0-9A-F allowed",
                                "Only 0-9A-F allowed", "Alphabets Only Allowed", "Alphabets Only Allowed", "Mixed AlphaNumeric Only Allowed",
                                "Mixed AlphaNumeric Only Allowed", "Only AlphaNumeric Allowed, All Zerors Not Allowed", 
                                "Only AlphaNumeric Allowed, All Zerors Not Allowed", "Mixed Alpha Numeric Only Allowed",
                                "Mixed Alpha Numeric Only Allowed", "All Zerors/Specials Not Allowed", "All Zerors/Specials Not Allowed", "", "",
                                "All Zerors/Specials Not Allowed", "All Zerors/Specials Not Allowed", "All Zerors Not Allowed", "All Zerors Not Allowed",
                                "", "", "", "All Zerors Not Allowed", "All Zerors Not Allowed","All Zerors Not Allowed","All Zerors Not Allowed", "All Zerors Not Allowed", "All Zerors Not Allowed",
                                "All Zerors Not Allowed", "All Zerors Not Allowed", "Alphabets Only Allowed", "Alphabets Only Allowed",
                                "All Zerors/Specials Not Allowed", "All Zerors/Specials Not Allowed", "All Zerors/Specials Not Allowed",
                                "All Zerors/Specials Not Allowed", "Hex Value Only Allowed", "Hex Value Only Allowed" };

            buf = tmp = "";

            if (txtIps.Length != ErrMsg.Length || txtIps.Length != ChkVal.Length || txtIps.Length != MaxLen.Length || txtIps.Length != AllowNewLine.Length)
            {
                DBErrBuf = "MA[45]: Invalid Input Params Length ...\n Contact System Admin";
                return 0;
            }

            for (i = 0; i < txtIps.Length; i++)
            {
                buf = tmp = "";

                if (AcptNull[i] == 1 && txtIps[i] == null)
                    txtIps[i] = "";

                if (txtIps[i] == null)
                {
                    DBErrBuf = "MA[46]: Input Valid " + ErrMsg[i] + " ....!";
                    return 0;
                }

                buf = TrimExtraSpace(AllowNewLine[i], txtIps[i].Trim());
                if (ChkVal[i] == TXTIP_EMAIL || ChkVal[i] == TXTIP_IFAVL_EMAIL)
                    buf = buf.Replace(" ", "");
                else if (ChkVal[i] == TXTIP_HEX || ChkVal[i] == TXTIP_IFAVL_HEX || ChkVal[i] == TXTIP_HEX_LEN || ChkVal[i] == TXTIP_IFAVL_HEX_LEN || 
                            ChkVal[i] == TXTIP_MACID || ChkVal[i] == TXTIP_IFAVL_MACID)
                    buf = buf.ToUpper();

                if ((ChkVal[i] == TXTIP_NOS_ZERO || ChkVal[i] == TXTIP_NOS || ChkVal[i] == TXTIP_HEX || ChkVal[i] == TXTIP_HEX_LEN || ChkVal[i] == TXTIP_ALPHA ||
                        ChkVal[i] == TXTIP_ALPHA_NOS || ChkVal[i] == TXTIP_ALPHA_NOS_ANY || ChkVal[i] == TXTIP_ALPHA_NOS_SPL || ChkVal[i] == TXTIP_ALPHA_NOS_SPL_ANY ||
                        ChkVal[i] == TXTIP_ALPHA_SPC || ChkVal[i] == TXTIP_NOS_SPL || ChkVal[i] == TXTIP_DECIMAL || ChkVal[i] == TXTIP_DECIMAL_ZERO ||
                        ChkVal[i] == TXTIP_EMAIL || ChkVal[i] == TXTIP_MOBILE || ChkVal[i] == TXTIP_MOBILE_DIALCODE || ChkVal[i] == TXTIP_PHONENO ||
                        ChkVal[i] == TXTIP_PHONENO_MULTI || ChkVal[i] == TXTIP_NAME || ChkVal[i] == TXTIP_MASTER_CODE || ChkVal[i] == TXTIP_MASTER_DESC || 
                        ChkVal[i] == TXTIP_MACID)
                        && buf.Length == 0)
                {
                    DBErrBuf = "MA[47]: Input Valid " + ErrMsg[i] + " ....!";
                    return 0;
                }
                else if ((ChkVal[i] == TXTIP_IFAVL_NOS || ChkVal[i] == TXTIP_IFAVL_HEX || ChkVal[i] == TXTIP_IFAVL_HEX_LEN || ChkVal[i] == TXTIP_IFAVL_ALPHA ||
                        ChkVal[i] == TXTIP_IFAVL_ALPHA_NOS || ChkVal[i] == TXTIP_IFAVL_ALPHA_NOS_ANY || ChkVal[i] == TXTIP_IFAVL_ALPHA_NOS_SPL ||
                        ChkVal[i] == TXTIP_IFAVL_ALPHA_NOS_SPL_ANY || ChkVal[i] == TXTIP_IFAVL_ALPHA_SPC || ChkVal[i] == TXTIP_IFAVL_NOS_SPL ||
                        ChkVal[i] == TXTIP_IFAVL_DECIMAL || ChkVal[i] == TXTIP_IFAVL_EMAIL || ChkVal[i] == TXTIP_IFAVL_MOBILE ||
                        ChkVal[i] == TXTIP_IFAVL_MOBILE_DIALCODE || ChkVal[i] == TXTIP_IFAVL_PHONENO || ChkVal[i] == TXTIP_IFAVL_PHONENO_MULTI ||
                        ChkVal[i] == TXTIP_IFAVL_NAME || ChkVal[i] == TXTIP_IFAVL_MASTER_CODE || ChkVal[i] == TXTIP_IFAVL_MASTER_DESC || ChkVal[i] == TXTIP_IFAVL_MACID) 
                        && buf.Length == 0)
                    continue;
                else if ((ChkVal[i] == TXTIP_HEX_LEN || ChkVal[i] == TXTIP_IFAVL_HEX_LEN) && buf.Length % 2 != 0)
                {
                    DBErrBuf = "MA[48]: Input Valid " + ErrMsg[i] + " ...! \nInvalid HexaDecimal Length. It must be Multiples of 2 ....";
                    return 0;
                }


                tmp = buf;
                if (ChkVal[i] == TXTIP_NONE)
                    v = true;
                else if (ChkVal[i] == TXTIP_NOS || ChkVal[i] == TXTIP_NOS_ZERO || ChkVal[i] == TXTIP_IFAVL_NOS)
                    v = new Regex("^[0-9]*$").IsMatch(buf);
                else if (ChkVal[i] == TXTIP_ALPHA || ChkVal[i] == TXTIP_IFAVL_ALPHA)
                    v = new Regex("^[a-zA-Z]*$").IsMatch(buf);
                else if (ChkVal[i] == TXTIP_ALPHA_NOS || ChkVal[i] == TXTIP_IFAVL_ALPHA_NOS || ChkVal[i] == TXTIP_ALPHA_NOS_ANY ||
                                ChkVal[i] == TXTIP_IFAVL_ALPHA_NOS_ANY)
                    v = new Regex("^[a-zA-Z0-9]*$").IsMatch(buf);
                else if (ChkVal[i] == TXTIP_ALPHA_NOS_SPL || ChkVal[i] == TXTIP_IFAVL_ALPHA_NOS_SPL || ChkVal[i] == TXTIP_ALPHA_NOS_SPL_ANY ||
                                ChkVal[i] == TXTIP_IFAVL_ALPHA_NOS_SPL_ANY)
                    v = new Regex("^[a-zA-Z0-9-*. ,@//()']*$").IsMatch(buf);
                else if (ChkVal[i] == TXTIP_ALPHA_SPC || ChkVal[i] == TXTIP_IFAVL_ALPHA_SPC)
                    v = new Regex("^[a-zA-Z ]*$").IsMatch(buf);
                else if (ChkVal[i] == TXTIP_HEX || ChkVal[i] == TXTIP_IFAVL_HEX || ChkVal[i] == TXTIP_HEX_LEN || ChkVal[i] == TXTIP_IFAVL_HEX_LEN)
                    v = new Regex("^[a-fA-F0-9]*$").IsMatch(buf);
                else if (ChkVal[i] == TXTIP_MACID || ChkVal[i] == TXTIP_IFAVL_MACID)
                    v = new Regex("^[a-fA-F0-9:]*$").IsMatch(buf);
                else if (ChkVal[i] == TXTIP_NOS_SPL || ChkVal[i] == TXTIP_IFAVL_NOS_SPL)
                    v = new Regex("^[0-9-*. ,@//()']*$").IsMatch(buf);
                else if (ChkVal[i] == TXTIP_DECIMAL || ChkVal[i] == TXTIP_IFAVL_DECIMAL || ChkVal[i] == TXTIP_DECIMAL_ZERO)
                {
                    v = new Regex("^[0-9.]*$").IsMatch(buf);
                    if (v)
                        v = new Regex(@"\d+\.?\d+?").IsMatch(buf);
                }
                else if (ChkVal[i] == TXTIP_EMAIL || ChkVal[i] == TXTIP_IFAVL_EMAIL)
                {
                    if (IsValidEmail(buf) != 1)
                        return 0;
                }
                else if (ChkVal[i] == TXTIP_MOBILE || ChkVal[i] == TXTIP_IFAVL_MOBILE)
                {
                    MinLen[i] = 10; MaxLen[i] = 11;
                    v = new Regex("^[0-9]*$").IsMatch(buf);
                }
                else if (ChkVal[i] == TXTIP_MOBILE_DIALCODE || ChkVal[i] == TXTIP_IFAVL_MOBILE_DIALCODE)
                    v = new Regex(@"^(\+?)[0-9]*$").IsMatch(buf);
                else if (ChkVal[i] == TXTIP_PHONENO || ChkVal[i] == TXTIP_IFAVL_PHONENO)
                    v = new Regex(@"^\d+(?:[- ]?\d+)$").IsMatch(buf);
                else if (ChkVal[i] == TXTIP_PHONENO_MULTI || ChkVal[i] == TXTIP_IFAVL_PHONENO_MULTI)
                    v = new Regex(@"^(\d+(?:[- ]?\d+)\,?)*$").IsMatch(buf);
                else if (ChkVal[i] == TXTIP_NAME || ChkVal[i] == TXTIP_IFAVL_NAME)
                    v = new Regex("^[a-zA-Z .]*$").IsMatch(buf);
                else if (ChkVal[i] == TXTIP_MASTER_CODE || ChkVal[i] == TXTIP_IFAVL_MASTER_CODE)
                    v = new Regex("^[a-zA-Z0-9- .//]*$").IsMatch(buf);
                else if (ChkVal[i] == TXTIP_MASTER_DESC || ChkVal[i] == TXTIP_IFAVL_MASTER_DESC)
                    v = new Regex("^[a-zA-Z0-9- .//&,()@']*$").IsMatch(buf);

                if (!v)
                {
                    DBErrBuf = "MA[49]: Input Valid " + ErrMsg[i] + " ....!\n" + VldMsg[ChkVal[i]];
                    return 0;
                }

                v = true;
                DBErrBuf = "Input Valid " + ErrMsg[i] + " ....!\n" + VldMsg[ChkVal[i]];
                if (ChkVal[i] == TXTIP_NOS || ChkVal[i] == TXTIP_IFAVL_NOS || ChkVal[i] == TXTIP_ALPHA_NOS_ANY || ChkVal[i] == TXTIP_IFAVL_ALPHA_NOS_ANY ||
                            ChkVal[i] == TXTIP_MOBILE || ChkVal[i] == TXTIP_IFAVL_MOBILE)
                    v = !buf.All(c => c == '0');
                else if (ChkVal[i] == TXTIP_MOBILE_DIALCODE || ChkVal[i] == TXTIP_IFAVL_MOBILE_DIALCODE)
                {
                    tmp = buf.Replace("+", "");
                    v = !tmp.All(c => c == '0');
                }
                else if (ChkVal[i] == TXTIP_ALPHA || ChkVal[i] == TXTIP_IFAVL_ALPHA)
                    v = buf.All(c => (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'));
                else if (ChkVal[i] == TXTIP_ALPHA_NOS || ChkVal[i] == TXTIP_IFAVL_ALPHA_NOS)
                    v = !buf.All(c => (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z')) && !buf.All(c => c >= '0' && c <= '9') && !buf.All(c => c == '0');
                else if (ChkVal[i] == TXTIP_ALPHA_NOS_SPL || ChkVal[i] == TXTIP_IFAVL_ALPHA_NOS_SPL)
                {
                    tmp = Regex.Replace(buf, "[^a-zA-Z0-9]+", "", RegexOptions.Compiled);
                    if (tmp.Length == 0)
                        return 0;
                    v = !tmp.All(c => (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z')) && !tmp.All(c => c >= '0' && c <= '9') && !tmp.All(c => c == '0');
                }
                else if (ChkVal[i] == TXTIP_ALPHA_NOS_SPL_ANY || ChkVal[i] == TXTIP_IFAVL_ALPHA_NOS_SPL_ANY)
                {
                    tmp = Regex.Replace(buf, "[^a-zA-Z0-9]+", "", RegexOptions.Compiled);
                    if (tmp.Length == 0)
                        return 0;
                    v = !tmp.All(c => c == '0');
                }
                else if (ChkVal[i] == TXTIP_NOS_SPL || ChkVal[i] == TXTIP_IFAVL_NOS_SPL)
                {
                    tmp = Regex.Replace(buf, "[^0-9]+", "", RegexOptions.Compiled);
                    if (tmp.Length == 0)
                        v = false;
                    else if (buf.All(c => c == '0'))
                        v = false;
                }
                else if ((ChkVal[i] == TXTIP_DECIMAL || ChkVal[i] == TXTIP_IFAVL_DECIMAL) && Convert.ToDecimal(buf) == 0)
                    v = false;
                else if (ChkVal[i] == TXTIP_PHONENO || ChkVal[i] == TXTIP_IFAVL_PHONENO || ChkVal[i] == TXTIP_PHONENO_MULTI || ChkVal[i] == TXTIP_IFAVL_PHONENO_MULTI)
                {
                    tmp = Regex.Replace(buf, "[^0-9]+", "", RegexOptions.Compiled);
                    if (tmp.Length == 0)
                        return 0;

                    v = !tmp.All(c => c == '0');
                }
                else if (ChkVal[i] == TXTIP_NAME || ChkVal[i] == TXTIP_IFAVL_NAME)
                {
                    tmp = Regex.Replace(buf, "[^a-zA-Z0-9]+", "", RegexOptions.Compiled);
                    if (tmp.Length == 0)
                        return 0;
                }
                else if (ChkVal[i] == TXTIP_MASTER_CODE || ChkVal[i] == TXTIP_IFAVL_MASTER_CODE)
                {
                    tmp = Regex.Replace(buf, "[^a-zA-Z0-9]+", "", RegexOptions.Compiled);
                    if (tmp.Length == 0)
                        return 0;
                    v = !tmp.All(c => c == '0');
                }
                else if (ChkVal[i] == TXTIP_MASTER_DESC || ChkVal[i] == TXTIP_IFAVL_MASTER_DESC)
                {
                    tmp = Regex.Replace(buf, "[^a-zA-Z0-9]+", "", RegexOptions.Compiled);
                    if (tmp.Length == 0)
                        return 0;
                    v = !tmp.All(c => c == '0');
                }

                if (!v)
                {
                    DBErrBuf = "MA[50]: Input Valid " + ErrMsg[i] + " ....!\n" + VldMsg[ChkVal[i]];
                    return 0;
                }


                if (ChkVal[i] != TXTIP_NONE && MinLen[i] != 0 && MaxLen[i] != 0 && buf.Length != 0 && !(buf.Length >= MinLen[i] && buf.Length <= MaxLen[i]))
                {   // Chk MaxLen
                    DBErrBuf = "MA[51]: Input Valid " + ErrMsg[i] + " ....!  Min Len: " + MinLen[i] + "   &  Max Len: " + MaxLen[i];
                    return 0;
                }
                txtIps[i] = buf.Replace("'", "''");
            }

            return 1;
        }
        catch (Exception Ex)
        {
            DBErrBuf = "MC[36]: " + Ex.Message;
            PrintLog("TxtChkIps Exception", DBErrBuf);
            return 0;
        }
    }


    public int SendMail(string SMTPHost, string SMTPPort, string FromDesc, string FromAddr, string FromPwd, string ToAddr, string Subject, string Body,
                    bool HtmlFlag, ArrayList AttachPaths)
    {
        /* Send Mail with Attachment
            *  Return --> 1 Mail Sent, 0 Failed
            *  SMTPHost / Port -- SMTP Details
            *  FromDesc -- Description need to be shown in mail instead of MailID
            *  FromAddr -- From EmailId
            *  FromPwd -- From EmailId Password
            *  ToAddr -- To EmailId. Multi Address separated by Comma
            *  Subject -- Mail Subject
            *  Body -- Mail Body
            *  HtmlFlag -- In Html Format;  true / false
            *  AttachPaths -- Attachement File Paths. NULL - NA, Valid Paths
        */
        DBErrBuf = "";
        try
        {
            MailMessage Message = new MailMessage();
            SmtpClient Smtp = new SmtpClient();
            string buf = "";

            if (ToAddr.Length == 0)
            {
                DBErrBuf = "MA[52]: Invalid Mail To Address";
                return 0;
            }

            buf = FromAddr.Length == 0 ? FromAddr : FromDesc + "<" + FromAddr + ">";
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(buf);
                mail.To.Add(ToAddr);
                mail.Subject = Subject;
                mail.Body = Body;
                mail.IsBodyHtml = HtmlFlag;
                if (AttachPaths != null)
                    foreach (string s in AttachPaths)
                        mail.Attachments.Add(new Attachment(s));

                using (SmtpClient smtp = new SmtpClient(SMTPHost, Convert.ToInt32(SMTPPort)))
                {
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential(FromAddr, FromPwd);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }

            return 1;
        }
        catch (Exception ex)
        {
            DBErrBuf = "MC[37]: Mail Send Failed " + ex.Message;
            PrintLog("SendMail Exception", DBErrBuf);
            return 0;
        }
    }

    public string AppEncrypt(string src)
    {
        DBErrBuf = "";
        try
        {
            string buf = "";
            buf = Convert.ToBase64String(Encoding.ASCII.GetBytes(src));
            return buf;
        }
        catch (Exception Ex)
        {
            DBErrBuf = "MC[25]: " + Ex.Message;
            PrintLog("ECDATA", DBErrBuf);
            return "";
        }
    }


}
