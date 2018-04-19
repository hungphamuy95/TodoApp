using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Models;

namespace TodoApp.Interfaces
{
    public interface IBoardRespository
    {
        Task<IEnumerable<BoardModel>> GetAllBoard();
        Task<BoardModel> GetSingleBoard(string id);
        Task AddBoard(BoardModel item);
        Task<bool> RemoveBoard(string id);
        Task<bool> UpdateBoard(string id, BoardModel item);
        Task<bool> RemoveAll();
    }
}
