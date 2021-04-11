using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


public class CookieManager
{
    public CookieManager()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #region Admin Side Cookie functions

    public bool CheckAdminLoginCookie(out int AdminID)
    {
        bool result = false;
        if (HttpContext.Current.Request.Cookies[ConfigurationManager.AppSettings["AdminLoginCookieName"]] != null && HttpContext.Current.Request.Cookies[ConfigurationManager.AppSettings["AdminLoginCookieName"]]["AdminID"] != null && HttpContext.Current.Request.Cookies[ConfigurationManager.AppSettings["AdminLoginCookieName"]]["AdminID"].ToString() != "")
        {

            string val = HttpContext.Current.Request.Cookies[ConfigurationManager.AppSettings["AdminLoginCookieName"]]["AdminID"].ToString();
            string Email = HttpContext.Current.Request.Cookies[ConfigurationManager.AppSettings["AdminLoginCookieName"]]["AdminEmail"].ToString();
            //string RollID = HttpContext.Current.Request.Cookies[ConfigurationManager.AppSettings["AdminLoginCookieName"]]["RollID"].ToString();
            AdminID = Convert.ToInt32(Crypts.Decrypt(val));
            //AdminRoles = Crypts.Decrypt(RollID);
            result = true;

        }
        else
        {
            //AdminRoles = "";
            AdminID = 0;
            result = false;
        }
        return result;

    }
    public void CreateAdminCookie(string AdminID, string AdminEmailID, string RollID)
    {
        HttpCookie LogInCookie = new HttpCookie(ConfigurationManager.AppSettings["AdminLoginCookieName"]);
        LogInCookie.Values.Add("AdminID", AdminID);
        LogInCookie.Values.Add("AdminEmail", AdminEmailID);
        //LogInCookie.Values.Add("RollID", RollID);
        HttpContext.Current.Response.AppendCookie(LogInCookie);

    }

    #endregion
}
