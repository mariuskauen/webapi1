using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi1.Models.ViewModels
{
    public class AllRequests
    {
        public List<MyFriendRequestsViewModel> My { get; set; }

        public List<MyFriendRequestsViewModel> Others { get; set; }
    }
}
