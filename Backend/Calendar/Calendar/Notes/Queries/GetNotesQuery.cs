using Calendar.Database.DTO;
using Calendar.Database.Repositories;
using Calendar.Notes.Helpers;
using MediatR;

namespace Calendar.Notes.Queries
{
    public class GetNotesQuery : IRequest<IEnumerable<NoteDTO>>
    {
        public string date { get; }

        public GetNotesQuery(string date) { 
            this.date = date;
        }
    }

    public class GetNotesQueryHandler : IRequestHandler<GetNotesQuery, IEnumerable<NoteDTO>>
    {
        private readonly INotesRepository repository;

        public GetNotesQueryHandler(INotesRepository repository)
        {
            this.repository = repository;
        }
        
        public async Task<IEnumerable<NoteDTO>> Handle(GetNotesQuery request, CancellationToken cancellationToken)
        {
            if (DateValidator.IsValidDate(request.date) == false)
                return Enumerable.Empty<NoteDTO>();
            return await repository.GetAllNotesForDate(request.date);
        }
    }
}
