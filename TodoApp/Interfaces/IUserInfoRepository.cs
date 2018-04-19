using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Models;

namespace TodoApp.Interfaces
{
    public interface IUserInfoRepository
    {
        Task<IEnumerable<UserInfoModel>> GetAllUser();
        Task<UserInfoModel> GetSingleUser(string id);
        Task<UserInfoModel> GetSingleuserByPassword(UserInfoModel item);
        Task AddUser(UserInfoModel item);
        Task<bool> RemoveAll();
        Task<bool> UpdateUser(string id, UserInfoModel item);
        Task<bool> Removeuser(string id);
    }
}
