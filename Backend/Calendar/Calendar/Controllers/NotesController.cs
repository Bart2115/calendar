using Calendar.Controllers.Model;
using Calendar.Database.DTO;
using Calendar.Notes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Calendar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ApiBaseController
    {
        public NotesController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        public async Task<IActionResult> GetFromDate(string noteDate)
        {
            var result = await _mediator.Send(new GetNotesQuery(noteDate));
            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(NoteDTO noteDTO)
        {
            var result = await _mediator.Send(new AddNoteCommand(noteDTO));
            if (result)
                return Ok();
            return BadRequest();
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateRequest updateRequest)
        {
            var result = await _mediator.Send(new UpdateNoteCommand(updateRequest));
            if (result)
                return Ok();
            return BadRequest();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(NoteDTO noteDTO)
        {
            var result = await _mediator.Send(new DeleteNoteCommand(noteDTO));
            if (result)
                return Ok();
            return BadRequest();
        }
    }
}