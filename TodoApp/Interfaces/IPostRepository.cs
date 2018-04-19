using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Models;

namespace TodoApp.Interfaces
{
    public interface IPostRepository
    {
        Task<IEnumerable<PostModel>> GetAllPosts();
        Task<PostModel> GetSinglePost();
        Task AddPost();
        Task<bool> RemovePost();
        Task<bool> UpdatePost(string id, PostModel item);
        Task<bool> RemoveAllPost();
    }
}
