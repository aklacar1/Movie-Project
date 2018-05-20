using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IMDB.DataLayer.Entities;
using IMDB.Entities;

namespace IMDB.API.Controllers
{
    /// <summary>
    /// This Controller is used for connecting Jobs and People
    /// </summary>
    [Produces("application/json")]
    [Route("api/PersonJobs")]
    public class PersonJobsController : Controller
    {
        private readonly MovieDBContext _context;

        public PersonJobsController(MovieDBContext context)
        {
            _context = context;
        }

        // GET: api/PersonJobs
        [HttpGet]
        public IEnumerable<PersonJobs> GetPersonJobs()
        {
            return _context.PersonJobs;
        }

        // GET: api/PersonJobs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonJobs([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var personJobs = await _context.PersonJobs.SingleOrDefaultAsync(m => m.Id == id);

            if (personJobs == null)
            {
                return NotFound();
            }

            return Ok(personJobs);
        }

        // PUT: api/PersonJobs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonJobs([FromRoute] long id, [FromBody] PersonJobs personJobs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != personJobs.Id)
            {
                return BadRequest();
            }

            _context.Entry(personJobs).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonJobsExists(id))
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

        // POST: api/PersonJobs
        [HttpPost]
        public async Task<IActionResult> PostPersonJobs([FromBody] PersonJobs personJobs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.PersonJobs.Add(personJobs);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersonJobs", new { id = personJobs.Id }, personJobs);
        }

        // DELETE: api/PersonJobs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonJobs([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var personJobs = await _context.PersonJobs.SingleOrDefaultAsync(m => m.Id == id);
            if (personJobs == null)
            {
                return NotFound();
            }

            _context.PersonJobs.Remove(personJobs);
            await _context.SaveChangesAsync();

            return Ok(personJobs);
        }

        private bool PersonJobsExists(long id)
        {
            return _context.PersonJobs.Any(e => e.Id == id);
        }
    }
}