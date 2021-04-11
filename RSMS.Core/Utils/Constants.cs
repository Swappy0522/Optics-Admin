using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optics.Core.Utils
{
    public class Constants
    {
        // List of SP's for Login Page
        public const string SPCheckLogin = "spCheckLogin";


        // List of SP's for Student's Page
        public const string SPCustomerDetails = "uspSelectAllCustomer";
        public const string SPAddCustomer = "uspInsertCustomerDetails";
        public const string SPDeleteStudent = "uspDeleteStudentDetails";
        public const string SPUpdateStudent = "uspUpdateStudentDetails";
        public const string SPSingleCustomerDetails = "uspSelectSingleCustomerDetails";
        public const string SPStudentsDetailsAsPerCourseBranchBatch = "uspSelectStudentsDetailsFromCourseBranchBatch";
        public const string SPGetSingleStudentAttendanceDetails = "uspGetSingleStudentAttendanceDetails";
      
      
        // List of SP's for Error Log
        public const string SPLogError = "uspLogError";

      

    }
}
