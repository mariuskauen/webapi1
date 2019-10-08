using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi1.Models.ViewModels
{
    public class LoginResponseViewModel
    {
        public string StatusCode { get; set; }

        public string Token { get; set; }
    }
}
