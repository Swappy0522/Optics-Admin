using Optics.Core.Concrete;
using Optics.Core.Entities;
using Optics.Core.ViewEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Optics_Admin
{
    public partial class Customers : System.Web.UI.Page
    {

        public void ServerDateOfBirth(object source, ServerValidateEventArgs args)
        {
            args.IsValid = (Convert.ToDateTime(args.Value) == Convert.ToDateTime(txtDOB.Text));
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadAllCustomer();

            }
        }

        private void LoadAllCustomer()
        {
            try
            {
                CustomerRepository objCustomerRepository = new CustomerRepository();
                List<StudentView> lstCustomer = objCustomerRepository.GetAllCustomer();
                rptStudents.DataSource = lstCustomer;
                rptStudents.DataBind();
            }
            catch (Exception ex)
            {
                Utility.LogError(ex.Message, ex.ToString(), "LoadAllStudents", "Student(ASPX)");
            }
        }
        private void UpdateStudentInDB()
        {
            Customer objStudent = null;
            try
            {
                objStudent = new Customer();
                objStudent.CustomerID = Convert.ToInt32(ViewState["UserID"].ToString());
                objStudent.FirstName = txtFName.Text;
                objStudent.LastName = txtLName.Text;
                objStudent.UpdatedBy = "1";
                objStudent.UpdatedOn = DateTime.Now;
                objStudent.Address = txtAddress.Text;
                objStudent.AltMobNo = txtAMobNo.Text;
                objStudent.BloodGroup = ddlBloodGroup.SelectedValue;
                //objStudent.CollegeID = Convert.ToByte(ddlCollege.SelectedValue);
                objStudent.DOB = Convert.ToDateTime(txtDOB.Text);
                objStudent.EmailID = txtEmail.Text;
                objStudent.MobileNo = txtMobNo.Text;
                if (rbMale.Checked)
                {
                    objStudent.Gender = "M";
                }
                else
                {
                    objStudent.Gender = "F";
                }
                CustomerRepository objStudentRepository = new CustomerRepository();
                objStudentRepository.UpdateStudent(objStudent);
                ClearDetails();
                LoadAllCustomer();
                btnSubmit.Text = "Submit";
            }
            catch (Exception ex)
            {
                Utility.LogError(ex.Message, ex.ToString(), "UpdateStudentInDB", "Student(ASPX)");
            }
        }
        private void AddStudentInDB()
        {
            Customer objStudent = null;
            try
            {
                objStudent = new Customer();
                objStudent.FirstName = txtFName.Text;
                objStudent.LastName = txtLName.Text;
                objStudent.Address = txtAddress.Text;
                objStudent.AltMobNo = txtAMobNo.Text;
                objStudent.BloodGroup = ddlBloodGroup.SelectedValue;
                //objStudent.CollegeID = Convert.ToByte(ddlCollege.SelectedValue);
                objStudent.DOB = Convert.ToDateTime(txtDOB.Text);
                objStudent.EmailID = txtEmail.Text;
                objStudent.MobileNo = txtMobNo.Text;
                if (rbMale.Checked)
                {
                    objStudent.Gender = "M";
                }
                else
                {
                    objStudent.Gender = "F";
                }
                objStudent.AddedBy = "1";
                objStudent.AddedOn = DateTime.Now;
                CustomerRepository objCustomerRepository = new CustomerRepository();
                objCustomerRepository.AddStudent(objStudent);
                LoadAllCustomer();
                ClearDetails();
            }
            catch (Exception ex)
            {
                Utility.LogError(ex.Message, ex.ToString(), "AddStudentInDB", "Student(ASPX)");
            }
        }

        private void FillStudentDetails(int CustomerID)
        {
            try
            {
                CustomerRepository objCustomerRepository = new CustomerRepository();
                Customer objStudent = objCustomerRepository.GetSingleCustomer(CustomerID);

                txtFName.Text = objStudent.FirstName;
                txtLName.Text = objStudent.LastName;
                txtAddress.Text = objStudent.Address;
                txtAMobNo.Text = objStudent.AltMobNo;
                ddlBloodGroup.SelectedValue = objStudent.BloodGroup;
               // ddlCollege.SelectedValue = objStudent.CollegeID.ToString();
                txtDOB.Text = objStudent.DOB.ToString("yyyy-MM-dd");
                txtEmail.Text = objStudent.EmailID;
                txtMobNo.Text = objStudent.MobileNo;
                if (objStudent.Gender.ToUpper() == "M")
                {
                    rbMale.Checked = true;
                    rbFemale.Checked = false;
                }
                else
                {
                    rbMale.Checked = false;
                    rbFemale.Checked = true;
                }
            }
            catch (Exception ex)
            {
                Utility.LogError(ex.Message, ex.ToString(), "FillStudentDetails", "Student(ASPX)");
            }
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (btnSubmit.Text == "Submit")
            {
                AddStudentInDB();
            }
            else
            {
                UpdateStudentInDB();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            btnSubmit.Text = "Submit";
            ClearDetails();
        }

        protected void imgInfo_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void imgEdit_Click(object sender, ImageClickEventArgs e)
        {
            int StudentID = Convert.ToInt32(((ImageButton)sender).CommandArgument);
            btnSubmit.Text = "Update";
            ViewState["StudentID"] = StudentID;
            FillStudentDetails(StudentID);
        }

        protected void imgDelete_Click(object sender, ImageClickEventArgs e)
        {
            int StudentID = Convert.ToInt32(((ImageButton)sender).CommandArgument);
            CustomerRepository objStudentRepository = new CustomerRepository();
            objStudentRepository.DeleteStudent(StudentID);
            ClearDetails();
            LoadAllCustomer();
        }

        private void ClearDetails()
        {
            txtFName.Text = String.Empty;
            txtLName.Text = String.Empty;
            txtAddress.Text = String.Empty;
            txtAMobNo.Text = String.Empty;
            ddlBloodGroup.SelectedIndex = -1;
            //ddlCollege.SelectedIndex = -1;
            txtDOB.Text = String.Empty;
            txtEmail.Text = String.Empty;
            txtMobNo.Text = String.Empty;
            rbMale.Checked = true;
            rbFemale.Checked = false;
        }
    }
}