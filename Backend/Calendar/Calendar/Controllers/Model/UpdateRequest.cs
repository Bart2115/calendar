using Calendar.Database.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Calendar.Controllers.Model
{
    public class UpdateRequest
    {
        public NoteDTO NoteDTO { get; set; }
        public NoteDTO UpdatedValuesDTO { get; set; }
    }
}
