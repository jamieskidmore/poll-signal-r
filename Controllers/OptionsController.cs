using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PollSignalR.Models;
using PollSignalR.Hubs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;


namespace PollSignalR.Controllers
{
    [ApiController]
    [Route("api/polls/{pollId}/options")]
    public class OptionsController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IHubContext<VoteHub> _hubContext;

        public OptionsController(DatabaseContext context, IHubContext<VoteHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Option>>> GetOptions(int pollId)
        {
            var options = await _context.Options.Where(o => o.PollId == pollId).ToListAsync();
            return options;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Option>> GetOption(int pollId, int id)
        {
            var option = await _context.Options.FirstOrDefaultAsync(o => o.Id == id && o.PollId == pollId);

            if (option == null)
            {
                return NotFound();
            }

            return option;
        }

        [HttpPost]
        public async Task<ActionResult<Option>> PostOption(int pollId, Option option)
        {
            option.PollId = pollId;
            _context.Options.Add(option);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOption), new { pollId = pollId, id = option.Id }, option);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOption(int pollId, int id, Option option)
        {
            if (id != option.Id || pollId != option.PollId)
            {
                return BadRequest();
            }

            _context.Entry(option).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            await _hubContext.Clients.All.SendAsync("ReceiveVote", option);

            return NoContent();
        }

     
    }
}
