using Calendar.Controllers.Model;
using Calendar.Database.DTO;
using Calendar.Database.Repositories;
using Calendar.Notes.Helpers;
using MediatR;

namespace Calendar.Notes.Queries
{
    public class UpdateNoteCommand : IRequest<bool>
    {
        public NoteDTO noteDTO { get; }
        public NoteDTO updatedValuesDTO { get; }

        public UpdateNoteCommand(UpdateRequest updateRequest) { 
            this.noteDTO = updateRequest.NoteDTO;
            this.updatedValuesDTO = updateRequest.UpdatedValuesDTO;
        }
    }

    public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, bool>
    {
        private readonly INotesRepository repository;

        public UpdateNoteCommandHandler(INotesRepository repository)
        {
            this.repository = repository;
        }
        
        public async Task<bool> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
        {
            if (DateValidator.IsValidDate(request.noteDTO.Date) == false)
                return false;
            return await repository.UpdateNote(request.noteDTO, request.updatedValuesDTO);    
        }
    }
}
