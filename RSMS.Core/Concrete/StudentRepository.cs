using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Optics.Core.Abstract;
using Optics.Core.Entities;
using System.Data;
using System.Collections;
using Optics.Core.Utils;
using Optics.Core.ViewEntities;
using System.Windows;
using System.Windows.Forms;
namespace Optics.Core.Concrete
{
    public class CustomerRepository : IStudentRepository
    {
        public List<ViewEntities.StudentView> GetAllCustomer()
        {
            List<StudentView> lstStudents = null;
            try
            {
                DataSet dsStudents = new DataSet();
                dsStudents = DatabaseOperation.SelectDataFromDatabase(Constants.SPCustomerDetails);
                lstStudents = dsStudents.Tables[0].ToList<StudentView>();
            }
            catch (Exception ex)
            {
                Utility.LogError(ex.Message, ex.ToString(), "GetAllStudents", "StudentRepository");
            }
            return lstStudents;
        }
        public Entities.Customer GetSingleCustomer(int UserID)
        {
            List<Customer> lstStudents = null;
            Hashtable htParams = new Hashtable();
            try
            {
                DataSet dsStudents = new DataSet();
                htParams.Add("@UserID", UserID);
                dsStudents = DatabaseOperation.SelectDataFromDatabase(htParams, Constants.SPSingleCustomerDetails);
                lstStudents = dsStudents.Tables[0].ToList<Customer>();
            }
            catch (Exception ex)
            {
                Utility.LogError(ex.Message, ex.ToString(), "GetSingleStudent", "StudentRepository");
            }

            var stud = (from student in lstStudents
                        select student).SingleOrDefault();
            return stud;
        }
        public void AddStudent(Entities.Customer objStudent)
        {
            Hashtable htParams = new Hashtable();
            try
            {
                htParams.Add("@FirstName",objStudent.FirstName);
                htParams.Add("@LastName",objStudent.LastName);
                htParams.Add("@AddedBy",objStudent.AddedBy);
                htParams.Add("@AddedOn",objStudent.AddedOn);
                htParams.Add("@Address",objStudent.Address);
                htParams.Add("@AltMobNo",objStudent.AltMobNo);
                htParams.Add("@BloodGroup",objStudent.BloodGroup);
                htParams.Add("@DOB",objStudent.DOB);
                htParams.Add("@EmailID",objStudent.EmailID);
                htParams.Add("@Gender",objStudent.Gender);
                htParams.Add("@MobileNo",objStudent.MobileNo);
                int noOfRowsAffected = DatabaseOperation.InsertUpdateDeleteOperation(htParams, Constants.SPAddCustomer);
                if(noOfRowsAffected==-1)
                {
                  Utility.TopMostMessageBox.Show("This Student already exists.Try Again.");
                }
                else
                { 
                Utility.TopMostMessageBox.Show("Student is Added Successfully.");
                }
            }
            catch (Exception ex)
            {
                Utility.LogError(ex.Message, ex.ToString(), "AddStudent", "StudentRepository");
                Utility.TopMostMessageBox.Show("Student is not Added.Try again.");
            }
        }

        public List<StudentView> GetAllStudentsByCourseBranchBatch(byte branchID, byte courseID, byte batchID)
        {
            Hashtable htParams = new Hashtable();
            List<StudentView> lstStudents = null;
            try
            {
                htParams.Add("@BranchID", branchID);
                htParams.Add("@CourseID", courseID);
                htParams.Add("@BatchID", batchID);
                DataSet dsStudents = new DataSet();
                dsStudents = DatabaseOperation.SelectDataFromDatabase(htParams, Constants.SPStudentsDetailsAsPerCourseBranchBatch);
                lstStudents = dsStudents.Tables[0].ToList<StudentView>();
            }
            catch (Exception ex)
            {
                Utility.LogError(ex.Message, ex.ToString(), "GetAllStudentsByCourseBranchBatch", "StudentRepository");
            }
            return lstStudents;
        }

        public void UpdateStudent(Entities.Customer objCustomer)
        {
            Hashtable htParams = new Hashtable();
            try
            {
                htParams.Add("@FirstName", objCustomer.FirstName);
                htParams.Add("@LastName", objCustomer.LastName);               
                htParams.Add("@Address", objCustomer.Address);
                htParams.Add("@AltMobNo", objCustomer.AltMobNo);
                htParams.Add("@BloodGroup", objCustomer.BloodGroup);
                htParams.Add("@DOB", objCustomer.DOB);
                htParams.Add("@EmailID", objCustomer.EmailID);
                htParams.Add("@Gender", objCustomer.Gender);
                htParams.Add("@MobileNo", objCustomer.MobileNo);
                htParams.Add("@UpdatedBy", objCustomer.UpdatedBy);
                htParams.Add("@UpdatedOn", objCustomer.UpdatedOn);
                htParams.Add("@CustomerID", objCustomer.CustomerID);
                int noOfRowsAffected = DatabaseOperation.InsertUpdateDeleteOperation(htParams, Constants.SPUpdateStudent);
                if (noOfRowsAffected == -1)
                {
                    Utility.TopMostMessageBox.Show("This Student already exists.Try Again.");
                }
                else
                {
                    Utility.TopMostMessageBox.Show("Student Updated Successfully.");
                }
            }
            catch (Exception ex)
            {
                Utility.LogError(ex.Message, ex.ToString(), "UpdateStudent", "StudentRepository");
                Utility.TopMostMessageBox.Show("Student is not Updated.Try Again.");
            }
        }
        public void DeleteStudent(int studentId)
        {
            Hashtable htParams = new Hashtable();
            try
            {  
                htParams.Add("@StudentID", studentId);
                int noOfRowsAffected = DatabaseOperation.InsertUpdateDeleteOperation(htParams, Constants.SPDeleteStudent);
            }
            catch (Exception ex)
            {
                Utility.LogError(ex.Message, ex.ToString(), "DeleteStudent", "StudentRepository");
            }
        }
    }
}
