using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IMDB.DataLayer.Entities;
using IMDB.Entities;
using Microsoft.AspNetCore.Authorization;

namespace IMDB.API.Controllers
{
    /// <summary>
    /// This controller is used for CRUD manipulation over types of Jobs.
    /// </summary>
    [Produces("application/json")]
    [Route("api/Jobs")]
    public class JobsController : Controller
    {
        private readonly MovieDBContext _context;

        public JobsController(MovieDBContext context)
        {
            _context = context;
        }

        // GET: api/Jobs
        [HttpGet]
        [Authorize]
        public IEnumerable<Job> GetJob()
        {
            return _context.Job;
        }

        // GET: api/Jobs/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetJob([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var job = await _context.Job.SingleOrDefaultAsync(m => m.JobId == id);

            if (job == null)
            {
                return NotFound();
            }

            return Ok(job);
        }

        // PUT: api/Jobs/5
        [HttpPut("{id}")]
        [Authorize(Roles = "SU")]
        public async Task<IActionResult> PutJob([FromRoute] long id, [FromBody] Job job)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != job.JobId)
            {
                return BadRequest();
            }

            _context.Entry(job).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Jobs
        [HttpPost]
        [Authorize(Roles = "SU")]
        public async Task<IActionResult> PostJob([FromBody] Job job)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Job.Add(job);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJob", new { id = job.JobId }, job);
        }

        // DELETE: api/Jobs/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "SU")]
        public async Task<IActionResult> DeleteJob([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var job = await _context.Job.SingleOrDefaultAsync(m => m.JobId == id);
            if (job == null)
            {
                return NotFound();
            }

            _context.Job.Remove(job);
            await _context.SaveChangesAsync();

            return Ok(job);
        }

        private bool JobExists(long id)
        {
            return _context.Job.Any(e => e.JobId == id);
        }
    }
}