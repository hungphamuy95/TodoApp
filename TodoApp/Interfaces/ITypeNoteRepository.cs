using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Models;

namespace TodoApp.Interfaces
{
    public interface ITypeNoteRepository
    {
        Task<IEnumerable<TypeNoteModel>> GetAllTypeNote();
        Task<TypeNoteModel> GetSingleTypeNote(string id);
        Task AddTypeNote(TypeNoteModel item);
        Task<bool> RemoveTypeNote(string id);
        Task<bool> UpdateTypeNote(string id, TypeNoteModel item);
        Task<bool> RemoveAll();
        Task<IEnumerable<TypeNoteModel>> TypeAndNoteChildren { get; }
    }
}
