using Optics.Core.Entities;
using Optics.Core.Concrete;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Optics_Admin
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CheckCookie();
            }
        }

        public void CheckCookie()
        {
            if (Request.Cookies[ConfigurationManager.AppSettings["AdminLoginCookieName"]] != null && Request.Cookies[ConfigurationManager.AppSettings["AdminLoginCookieName"].ToString()]["AdminID"] != "")
            {
                Response.Redirect("Dashboard.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            AdminRepository objStudentRepository = new AdminRepository();
            Admin objStudent = objStudentRepository.CheckAdminExist(txtLogin.Text.Sanatize(), Crypts.Encrypt(txtPassword.Text.Trim()));
          
            if (objStudent.UserName != "")
            {
                string AdminID = objStudent.AdminID.ToString();
                string EmailID = objStudent.EmailID;
                string UserName = objStudent.UserName;

                CreateAdminCookie(Crypts.Encrypt(AdminID), EmailID, UserName);
                if (UserName != "")
                {
                    Response.Redirect("Dashboard.aspx");
                }
                else
                {
                    lblMsg.Text = "Sorry ! No Access authorization is provided yet to your role.";
                    lblMsg.Style.Add("color", "red");
                }
            }
            else
            {
                lblMsg.Text = "Invalid Email/Password";
                lblMsg.Style.Add("color", "red");
            }
        }

        protected void CreateAdminCookie(string AdminID, string AdminEmail, string UserName)
        {
            HttpCookie AdminLoginCookie = new HttpCookie(ConfigurationManager.AppSettings["AdminLoginCookieName"]);
            AdminLoginCookie.Values.Add("AdminID", AdminID);
            AdminLoginCookie.Values.Add("AdminEmail", AdminEmail);
            AdminLoginCookie.Values.Add("UserName", UserName);
            Response.AppendCookie(AdminLoginCookie);
        }

    }
}