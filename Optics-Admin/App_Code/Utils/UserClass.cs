using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Caching;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;


/// <summary>
/// Summary description for UserClass
/// </summary>
public class UserClass
{
    public UserClass()
    {
        //
        // TODO: Add constructor logic here
        //
    }



    

    

}
public static class UserExtentsions
{
    public static string ClearMe(this string Value)
    {
        if (Value != null && Value.ToString() != "")
        {
            return Value.Replace("'", "''");
        }
        else return Value;

    }

    public static string isNul(this object Value)
    {
        if (Value == null)
        {
            Value = "";
            return Value.ToString();
        }
        else
        {
            return Value.ToString();
        }
    }

    public static string ToIndianCurrency(string Currency)
    {
        double Amount = 0;
        if (Currency != "")
            Amount = Convert.ToDouble(Currency);

        return Currency = Amount.ToString("0,0", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
    }


}



