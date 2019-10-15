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
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUserRepository _userRepo;

        public UserController(DataContext context, IUserRepository userRepo)
        {
            _context = context;
            _userRepo = userRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserListViewModel>>> GetUsers()
        {
            //Denne må ikke brukes pga bug i entityframework core 3
            var userList = await _userRepo.GetUsersForList(await GetUserId());
            return userList;
        }

        [HttpGet("allusers")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("searchforuser")]
        public async Task<ActionResult<List<SearchUserViewModel>>> SearchForUser(string username)
        {
            List<SearchUserViewModel> userVm = new List<SearchUserViewModel>();
            List<User> user = await _context.Users.Where(u => u.Username.StartsWith(username)).ToListAsync();

            if (user == null)
                return BadRequest();

            foreach(User usr in user)
            {
                SearchUserViewModel vm = new SearchUserViewModel();
                vm.UserId = usr.Id;
                vm.Username = usr.Username;

                userVm.Add(vm);
            }

            return userVm;

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        private async Task<string> GetUserId()
        {
            return HttpContext.User.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
        }
        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}