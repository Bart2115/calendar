using Calendar.Database.DTO;
using Calendar.Database.Repositories;
using Calendar.Notes.Helpers;
using MediatR;

namespace Calendar.Notes.Queries
{
    public class AddNoteCommand : IRequest<bool>
    {
        public NoteDTO noteDTO { get; }

        public AddNoteCommand(NoteDTO noteDTO) { 
            this.noteDTO = noteDTO;
        }
    }

    public class AddNoteCommandHandler : IRequestHandler<AddNoteCommand, bool>
    {
        private readonly INotesRepository repository;

        public AddNoteCommandHandler(INotesRepository repository)
        {
            this.repository = repository;
        }
        
        public async Task<bool> Handle(AddNoteCommand request, CancellationToken cancellationToken)
        {
            if (DateValidator.IsValidDate(request.noteDTO.Date) == false)
                return false;
            return await repository.AddNote(request.noteDTO);    
        }
    }
}
