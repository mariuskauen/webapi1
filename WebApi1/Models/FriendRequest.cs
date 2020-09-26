using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi1.Models
{
    public class FriendRequest
    {
        public string Id { get; set; }

        public bool IsActive { get; set; }
    }
}
