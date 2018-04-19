using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Models;

namespace TodoApp.Interfaces
{
    public interface INoteRepository
    {
        Task<IEnumerable<NoteModel>> GetAllNotes(); 
        Task<NoteModel> GetNote(string id);
        Task AddNote(NoteModel item);
        Task<bool> RemoveNote(string id);
        Task<bool> UpdateNote(string id, string body);
        Task<bool> UpdateNoteDocument(string id, NoteModel item);
        Task<bool> RemoveAllNotes();
    }
}
