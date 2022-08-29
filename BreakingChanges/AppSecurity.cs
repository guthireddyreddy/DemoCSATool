using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

[assembly: SecurityCritical]
namespace BreakingChangesAppSecurity
{
    public class AppSecurity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
