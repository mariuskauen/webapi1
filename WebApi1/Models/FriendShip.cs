﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi1.Models
{
    public class FriendShip
    {
        public User FriendOne { get; set; }

        public string FriendOneId { get; set; }

        public User FriendTwo { get; set; }

        public string FriendTwoId { get; set; }

        public DateTime FriendsSince { get; set; }

        public bool IsFriends { get; set; }
    }
}
