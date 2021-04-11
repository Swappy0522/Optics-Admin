using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Optics.Core.Entities;
using Optics.Core.ViewEntities;

namespace Optics.Core.Abstract
{
     interface IStudentRepository
    {
        List<ViewEntities.StudentView> GetAllCustomer();
        Customer GetSingleCustomer(int studentId);
        void AddStudent(Customer objCustomer);
        void UpdateStudent(Customer stud);
        void DeleteStudent(int studentId);
    }
}
