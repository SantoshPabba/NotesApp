using DataAccess.DBContexts;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    
    public class NotesRepository : INotesRepository
    {
        private readonly NotesDbContext _context;

        public NotesRepository(NotesDbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Note>> GetAllNotesAsync() => await _context.Notes.ToListAsync();

        public async Task<Note> GetNoteByIdAsync(int id) => await _context.Notes.FindAsync(id);

        private bool NotesExists(int Id) => _context.Notes.Any(x => x.ID == Id);

        public async Task AddNoteAsync(Note note, string UserName)
        {
            try
            {
                _context.Notes.Add(note);
                //await _context.SaveChangesAsync();
                await _context.SaveChangesAsync(UserName: UserName);
            }
            catch (Exception) { throw; }
        }

        public async Task UpdateNoteAsync(Note note, string UserName)
        {
            try
            {
                var oldNote = await GetNoteByIdAsync(note.ID);
                var dummyNote = await GetNoteByIdAsync(note.ID);

                dummyNote.Title = note.Title;
                dummyNote.Content = note.Content;
                dummyNote.DateModified = note.DateModified;

                _context.Entry(dummyNote).State = EntityState.Modified;
                _context.Entry(oldNote).CurrentValues.SetValues(dummyNote);
                await _context.SaveChangesAsync(UserName: UserName);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (NotesExists(note.ID))
                    throw;
            }
        }

        public async Task DeleteNoteAsync(Note note, string UserName)
        {
            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
            //await _context.SaveChangesAsync(UserName);
        }

        public async Task DeleteNoteByIDAsync(int id, string UserName)
        {
            var note = await GetNoteByIdAsync(id);
            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
            //await _context.SaveChangesAsync(UserName);
        }
    }
}
