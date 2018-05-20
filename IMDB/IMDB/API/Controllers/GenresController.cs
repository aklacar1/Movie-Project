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
    /// This controlled is used for CURD over Genre table. 
    /// In order to add new Genre for specific movie, you have to do it here, alternatevely in Movies Controller.
    /// </summary>
    [Produces("application/json")]
    [Route("api/Genres")]
    public class GenresController : Controller
    {
        private readonly MovieDBContext _context;

        public GenresController(MovieDBContext context)
        {
            _context = context;
        }

        // GET: api/Genres
        [HttpGet]
        [Authorize]
        public IEnumerable<Genre> GetGenre()
        {
            return _context.Genre;
        }

        // GET: api/Genres/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetGenre([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var genre = await _context.Genre.SingleOrDefaultAsync(m => m.GenreId == id);

            if (genre == null)
            {
                return NotFound();
            }

            return Ok(genre);
        }

        // PUT: api/Genres/5
        [HttpPut("{id}")]
        [Authorize(Roles ="SU")]
        public async Task<IActionResult> PutGenre([FromRoute] long id, [FromBody] Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != genre.GenreId)
            {
                return BadRequest();
            }

            _context.Entry(genre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenreExists(id))
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

        // POST: api/Genres
        [HttpPost]
        [Authorize(Roles = "SU")]
        public async Task<IActionResult> PostGenre([FromBody] Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Genre.Add(genre);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGenre", new { id = genre.GenreId }, genre);
        }

        // DELETE: api/Genres/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "SU")]
        public async Task<IActionResult> DeleteGenre([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var genre = await _context.Genre.SingleOrDefaultAsync(m => m.GenreId == id);
            if (genre == null)
            {
                return NotFound();
            }

            _context.Genre.Remove(genre);
            await _context.SaveChangesAsync();

            return Ok(genre);
        }

        private bool GenreExists(long id)
        {
            return _context.Genre.Any(e => e.GenreId == id);
        }
    }
}