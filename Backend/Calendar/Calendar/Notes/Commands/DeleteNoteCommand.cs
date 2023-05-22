using Calendar.Database.DTO;
using Calendar.Database.Repositories;
using Calendar.Notes.Helpers;
using MediatR;

namespace Calendar.Notes.Queries
{
    public class DeleteNoteCommand : IRequest<bool>
    {
        public NoteDTO noteDTO { get; }

        public DeleteNoteCommand(NoteDTO noteDTO) { 
            this.noteDTO = noteDTO;
        }
    }

    public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand, bool>
    {
        private readonly INotesRepository repository;

        public DeleteNoteCommandHandler(INotesRepository repository)
        {
            this.repository = repository;
        }
        
        public async Task<bool> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
        {
            if (DateValidator.IsValidDate(request.noteDTO.Date) == false)
                return false;
            return await repository.DeleteNote(request.noteDTO);    
        }
    }
}
