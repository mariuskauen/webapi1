﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi1.Models;
using WebApi1.Models.ViewModels;

namespace WebApi1.Data
{
    public class FriendRepository : IFriendRepository
    {
        private readonly DataContext _context;

        public FriendRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<MyFriendRequestsViewModel>> GetMyRequests(string _userid)
        {
            MyFriendRequestsViewModel vm;
            List<MyFriendRequestsViewModel> friendRequests = new List<MyFriendRequestsViewModel>();
            var user = await _context.Users.Include(g => g.MyRequests).ThenInclude(MyRequests => MyRequests.Receiver).FirstOrDefaultAsync(x => x.Id == _userid);

            var myRequests = user.MyRequests.ToList();
            foreach (FriendRequest request in myRequests)
            {
                if (request.IsActive)
                {
                    vm = new MyFriendRequestsViewModel()
                    {
                        RequestId = request.Id,
                        FriendId = request.ReceiverId,
                        FriendName = request.Receiver.Username
                    };

                    friendRequests.Add(vm);
                }
            }
            return friendRequests;
        }

        public async Task<List<MyFriendRequestsViewModel>> GetOthersRequests(string _userid)
        {
            MyFriendRequestsViewModel vm;
            List<MyFriendRequestsViewModel> friendRequests = new List<MyFriendRequestsViewModel>();
            var user = await _context.Users.Include(g => g.OthersRequests).ThenInclude(OthersRequests => OthersRequests.Sender).FirstOrDefaultAsync(x => x.Id == _userid);

            var othersRequests = user.OthersRequests.ToList();
            foreach (FriendRequest request in othersRequests)
            {
                if (request.IsActive)
                {
                    vm = new MyFriendRequestsViewModel()
                    {
                        RequestId = request.Id,
                        FriendId = request.SenderId,
                        FriendName = request.Sender.Username
                    };

                    friendRequests.Add(vm);
                }
            }
            return friendRequests;
        }
        public async Task<List<FriendViewModel>> GetAllMyFriends(string id)
        {
            List<FriendViewModel> friends = new List<FriendViewModel>();
            var user = await _context.Users
                .Include(r => r.FriendsOne)
                .ThenInclude(g => g.FriendTwo)
                .Include(s => s.FriendsTwo)
                .ThenInclude(g => g.FriendOne)
                .FirstOrDefaultAsync(f => f.Id == id);

            foreach (FriendShip friend in user.FriendsOne)
            {
                if (friend.IsFriends)
                {
                    FriendViewModel vm = new FriendViewModel()
                    {
                        Id = friend.FriendTwoId,
                        Username = friend.FriendTwo.Username,
                        Firstname = friend.FriendTwo.Firstname,
                        Lastname = friend.FriendTwo.Lastname
                    };

                    friends.Add(vm);
                }
            }
            foreach (FriendShip friend in user.FriendsTwo)
            {
                if (friend.IsFriends)
                {
                    FriendViewModel vm = new FriendViewModel()
                    {
                        Id = friend.FriendOneId,
                        Username = friend.FriendOne.Username,
                        Firstname = friend.FriendOne.Firstname,
                        Lastname = friend.FriendOne.Lastname
                    };

                    friends.Add(vm);
                }
            }

            return friends;
        }
    }
}
