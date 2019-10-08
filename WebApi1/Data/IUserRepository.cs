using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi1.Models.ViewModels;

namespace WebApi1.Data
{
    public interface IUserRepository
    {
           Task<List<UserListViewModel>> GetUsersForList(string userId);
    }
}
