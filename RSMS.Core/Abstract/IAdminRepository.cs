using Optics.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optics.Core.Abstract
{
   

    interface IAdminRepository
    {
        
        Admin CheckAdminExist(string email,string password);
    
    }
}
