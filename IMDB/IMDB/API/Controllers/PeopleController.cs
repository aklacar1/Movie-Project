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
    /// This controller is used for manipulation of Person table data.
    /// </summary>
    [Produces("application/json")]
    [Route("api/People")]
    public class PeopleController : Controller
    {
        private readonly MovieDBContext _context;

        public PeopleController(MovieDBContext context)
        {
            _context = context;
        }

        // GET: api/People
        [HttpGet]
        [Authorize]
        public IEnumerable<Person> GetPerson()
        {
            return _context.Person;
        }

        // GET: api/People/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetPerson([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var person = await _context.Person.SingleOrDefaultAsync(m => m.PersonId == id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        // PUT: api/People/5
        [HttpPut("{id}")]
        [Authorize(Roles = "SU")]
        public async Task<IActionResult> PutPerson([FromRoute] long id, [FromBody] Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != person.PersonId)
            {
                return BadRequest();
            }

            _context.Entry(person).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
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

        // POST: api/People
        [HttpPost]
        [Authorize(Roles = "SU")]
        public async Task<IActionResult> PostPerson([FromBody] Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Person.Add(person);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPerson", new { id = person.PersonId }, person);
        }

        // DELETE: api/People/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "SU")]
        public async Task<IActionResult> DeletePerson([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var person = await _context.Person.SingleOrDefaultAsync(m => m.PersonId == id);
            if (person == null)
            {
                return NotFound();
            }

            _context.Person.Remove(person);
            await _context.SaveChangesAsync();

            return Ok(person);
        }

        private bool PersonExists(long id)
        {
            return _context.Person.Any(e => e.PersonId == id);
        }
    }
}