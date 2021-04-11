using Optics.Core.Abstract;
using Optics.Core.Entities;
using Optics.Core.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optics.Core.Concrete
{
    public class AdminRepository: IAdminRepository
    {


        public Admin CheckAdminExist(string email,string password)
        {
            List<Admin> lstStudents = null;
            Hashtable htParams = new Hashtable();
            try
            {
                DataSet dsStudents = new DataSet();
                htParams.Add("@EmailID", email);
                htParams.Add("@Password", password);
                dsStudents = DatabaseOperation.SelectDataFromDatabase(htParams, Constants.SPCheckLogin);
                lstStudents = dsStudents.Tables[0].ToList<Admin>();
            }
            catch (Exception ex)
            {
                Utility.LogError(ex.Message, ex.ToString(), "GetSingleStudent", "StudentRepository");
            }

            var stud = (from student in lstStudents
                        select student).SingleOrDefault();
            return stud;
        }
    }
}
