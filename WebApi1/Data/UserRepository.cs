using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi1.Models;
using WebApi1.Models.ViewModels;

namespace WebApi1.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<UserListViewModel>> GetUsersForList(string userId)
        {
            List<UserListViewModel> userList = new List<UserListViewModel>();
            UserListViewModel vm = new UserListViewModel();
            var users = await _context.Users.ToListAsync();

            return userList;
        }
    }
}
