using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi1.Data;
using WebApi1.Models;
using WebApi1.Models.ViewModels;

namespace WebApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IFriendRepository _friend;

        public FriendController(DataContext context, IFriendRepository friend)
        {
            _context = context;
            _friend = friend;
        }

        [HttpPost("acceptfriend/{requestId}")]
        public async Task<IActionResult> AcceptFriend(string requestId)
        {
            return Ok();
        }

        [HttpGet("getallmyfriends")]
        public async Task<List<FriendViewModel>> GetAllMyFriends()
        {
            return _friend.GetAllMyFriends(await GetUserId());
        }

        [HttpGet("getonlinefriends")]
        public async Task<List<FriendViewModel>> GetOnlineFriends()
        {
            List<FriendViewModel> onlineFriends = new List<FriendViewModel>();

            foreach (FriendViewModel vm in _friend.GetAllMyFriends(await GetUserId()))
            {
                if (vm.Online == true)
                {
                    onlineFriends.Add(vm);
                }
            }

            return onlineFriends;
        }

        [HttpPost("addfriend/{receiverId}")]
        public async Task<IActionResult> SendFriendRequest(string receiverId)
        {
            //Get receiver from id
            User receiver = await _context.Users.FirstOrDefaultAsync(u => u.Id == receiverId);

            //Check if user exists
            if (receiver == null)
                return BadRequest("No such user");

            //Get own userid
            var userId = GetUserId().Result;
            //HttpContext.User.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;

            //User me = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            bool isFriends = _friend.CheckIfFriends(userId, receiverId);
            FriendRequest request = _friend.GetRequestIfExists(userId, receiverId);

            if (isFriends)
                return BadRequest("Allerede venner.");

            if (request == null)
            {
                request = new FriendRequest()
                {
                    Id = userId + ":" + receiverId,
                    IsActive = true
                };
                _context.FriendRequests.Add(request);
                await _context.SaveChangesAsync();

                return Ok();
            }

            if (request.Id.StartsWith(userId) && request.IsActive == false)
            {
                request.IsActive = true;
            }
            else if (request.Id.StartsWith(receiverId) && request.IsActive == false)
            {
                request.Id = userId + ":" + receiverId;
                request.IsActive = true;
            }
            else if (request.Id.StartsWith(userId) && request.IsActive == true)
            {
                return BadRequest("Request finnes allerede.");
            }
            else if (request.Id.StartsWith(receiverId) && request.IsActive == true)
            {
                _context.Friends.Add(_friend.MakeFriendship(userId, receiverId));
                request.IsActive = false;
            }
            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpGet("getmyrequests")]
        public async Task<List<MyFriendRequestsViewModel>> GetMyRequests()
        {
            return await _friend.GetMyRequests(GetUserId().Result);
        }
        [HttpGet("getothersrequests")]
        public async Task<List<MyFriendRequestsViewModel>> GetOthersRequests()
        {
            return await _friend.GetOthersRequests(GetUserId().Result);
        }

        [HttpGet("getallrequests")]
        public async Task<AllRequests> GetAllRequests()
        {
            AllRequests requests = new AllRequests()
            {
                My = await _friend.GetMyRequests(GetUserId().Result),
                Others = await _friend.GetOthersRequests(GetUserId().Result)
            };
            return requests;
        }

        [HttpPost("deletemyfriend/{friendId}")]
        public async Task<IActionResult> DeleteFriend(string friendId)
        {
            return Ok();
        }

        private async Task<string> GetUserId()
        {
            return HttpContext.User.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
        }
    }
}