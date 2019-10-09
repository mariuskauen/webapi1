using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi1.Models.ViewModels
{
    public class FriendViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public bool Online { get; set; }
    }
}
