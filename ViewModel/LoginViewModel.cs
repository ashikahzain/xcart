using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace xcart.ViewModel
{
    public class LoginViewModel
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public string Password { get; set; }
    }
}
