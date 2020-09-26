using Microsoft.EntityFrameworkCore;
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
            throw new NotImplementedException();
        }

        public async Task<List<MyFriendRequestsViewModel>> GetOthersRequests(string _userid)
        {
            throw new NotImplementedException();
        }
        public List<FriendViewModel> GetAllMyFriends(string id)
        {
            List<FriendViewModel> friends = new List<FriendViewModel>();
            List<FriendShip> friendships = _context.Friends.Where(z => z.Id.Contains(id) && z.IsFriends == true).ToList();
            FriendViewModel vm;
            foreach(FriendShip f in friendships)
            {
                string userid;
                int pos = f.Id.IndexOf(id);
                if(pos == 0)
                {
                    userid = f.Id.Remove(pos, id.Length + 1);
                }
                else
                {
                    userid = f.Id.Remove(pos - 1, id.Length + 1);
                }
                User friend = _context.Users.Where(z => z.Id == userid).FirstOrDefault();
                if(friend != null)
                {
                    vm = new FriendViewModel()
                    {
                        Id = friend.Id,
                        Username = friend.Username,
                        Firstname = friend.Firstname,
                        Lastname = friend.Lastname,
                        Online = friend.Online
                    };
                    friends.Add(vm);
                }
            }
            
            return friends;
        }

        public bool CheckIfFriends(string userId, string receiverId)
        {
            FriendShip friend = _context.Friends.Where(z => z.Id.Contains(userId) && z.Id.Contains(receiverId)).FirstOrDefault();
            if (friend != null)
                return true;
            else
                return false;

        }

        public FriendRequest GetRequestIfExists(string userId, string receiverId)
        {
            FriendRequest request = _context.FriendRequests.Where(z => z.Id.Contains(userId) && z.Id.Contains(receiverId)).FirstOrDefault();
            return request;
        }

        public FriendShip MakeFriendship(string userId, string friendId)
        {
            FriendShip fs = new FriendShip()
            {
                Id = userId+":"+friendId,
                IsFriends = true,
                FriendsFrom = DateTime.Now
            };

            return fs;
        }
    }
}
