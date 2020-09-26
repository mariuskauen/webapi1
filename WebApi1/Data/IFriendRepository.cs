using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi1.Models;
using WebApi1.Models.ViewModels;

namespace WebApi1.Data
{
    public interface IFriendRepository
    {
        Task<List<MyFriendRequestsViewModel>> GetOthersRequests(string _userid);

        Task<List<MyFriendRequestsViewModel>> GetMyRequests(string _userid);

        List<FriendViewModel> GetAllMyFriends(string id);

        bool CheckIfFriends(string userId, string receiverId);

        FriendRequest GetRequestIfExists(string userId, string receiverId);

        FriendShip MakeFriendship(string userId, string friendId);
    }
}
