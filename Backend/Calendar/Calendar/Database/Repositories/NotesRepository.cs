using AutoMapper;
using Calendar.Database.DbContexts;
using Calendar.Database.DTO;
using Calendar.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Calendar.Database.Repositories
{
    public interface INotesRepository
    {
        public Task<bool> AddNote(NoteDTO note);
        public Task<bool> UpdateNote(NoteDTO note, NoteDTO updatedValues);
        public Task<bool> DeleteNote(NoteDTO note);
        public Task<Note?> GetNote(NoteDTO note);
        public Task<IEnumerable<NoteDTO>> GetAllNotesForDate(string noteDate);
    }
    public class NotesRepository : INotesRepository
    {
        private CalendarDbContext _dbContext;
        private IMapper _mapper;

        public NotesRepository(CalendarDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<bool> AddNote(NoteDTO note)
        {
            if (await GetNote(note) == null)
            {
                var noteToAdd = _mapper.Map<Note>(note);
                await _dbContext.Notes.AddAsync(noteToAdd);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }

        public async Task<bool> DeleteNote(NoteDTO note)
        {
            var noteToDelete = await GetNote(note);

            if (noteToDelete != null)
            {
                _dbContext.Notes.Remove(noteToDelete);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }

        public async Task<IEnumerable<NoteDTO>> GetAllNotesForDate(string noteDate)
        {
            var result = await _dbContext.Notes
                .Where(n => n.Date == noteDate)
                .ToListAsync();

            return _mapper.Map<IEnumerable<NoteDTO>>(result);
        }

        public async Task<bool> UpdateNote(NoteDTO note, NoteDTO updatedValues)
        {
            var noteToUpdate = await GetNote(note);
            if (noteToUpdate != null)
            {
                noteToUpdate.Date = updatedValues.Date;
                noteToUpdate.Description = updatedValues.Description;
                noteToUpdate.Type = _mapper.Map<NoteType>(updatedValues.Type);
                _dbContext.Notes.Update(noteToUpdate);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<Note?> GetNote(NoteDTO note)
        {
            return await _dbContext.Notes.FirstOrDefaultAsync(n => n.Date == note.Date
                && n.Type == _mapper.Map<NoteType>(note.Type)
                && n.Description == note.Description);
        }
    }
}
