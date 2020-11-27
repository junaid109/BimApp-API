using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BimApp.API.Dtos
{
    public class AppUserForLoginDto
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
