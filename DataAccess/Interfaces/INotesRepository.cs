using DataAccess.Entities;

namespace DataAccess.Interfaces
{
    public interface INotesRepository
    {
        Task<IEnumerable<Note>> GetAllNotesAsync();
        Task<Note> GetNoteByIdAsync(int id);
        Task AddNoteAsync(Note note, string UserName);
        Task UpdateNoteAsync(Note note, string UserName);
        Task DeleteNoteAsync(Note note, string UserName);
        Task DeleteNoteByIDAsync(int id, string UserName);

    }
}
