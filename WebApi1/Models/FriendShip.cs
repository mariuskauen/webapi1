using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi1.Models
{
    public class FriendShip
    {
        public string Id { get; set; }

        public bool IsFriends { get; set; }

        public DateTime FriendsFrom { get; set; }
    }
}
