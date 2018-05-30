using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IMDB.Entities;
using IMDB.API.ServiceInterfaces;
using IMDB.DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using IMDB.DataLayer.Models.ViewModels;

namespace IMDB.API.Controllers
{
    //[Authorize]
    [Produces("application/json")]
    [Route("api/Movies")]
    public class MoviesController : Controller
    {
        private readonly IMovieService movieService;
        public MoviesController(IMovieService movieService)
        {
            this.movieService = movieService;
        }


        [HttpGet("GetMovieById/{id}")]
        public async Task<IActionResult> GetMovieById([FromRoute]long id)
        {
            var returnValue = movieService.GetMovieById(id);

            if (returnValue == null) return NotFound();

            return Ok(returnValue);
        }


        [HttpGet("GetTop100RatedMovies")]
        public async Task<IActionResult> GetTop100RatedMovies()
        {
            var returnValue = movieService.GetTop100RatedMovies();

            if (returnValue == null) return NotFound();

            return Ok(returnValue);
        }
        [HttpGet("LoadMovies")]
        public async Task<IActionResult> LoadMovies()
        {
            var returnValue = movieService.LoadMovies();

            if (returnValue == null) return NotFound();

            return Ok(returnValue);
        }
        [HttpGet("SearchMovieByTitle/{title}")]
        public async Task<IActionResult> SearchMovieByTitle([FromRoute]string title)
        {
            var returnValue = movieService.SearchMovieByTitle(title);
            if (returnValue == null) return NotFound();
            return Ok(returnValue);
        }

        [HttpPost("InsertMovie")]
        [Authorize(Roles = "SU")]
        public async Task<IActionResult> InsertMovie([FromBody]Movie movie)
        {
            var returnValue = movieService.InsertMovie(movie);
            if (returnValue == 0) return NotFound();
            return Ok(returnValue);
        }

        [HttpPut("UpdateMovie")]
        [Authorize(Roles = "SU")]
        public async Task<IActionResult> UpdateMovie([FromBody]Movie movie)
        {
            var returnValue = movieService.UpdateMovie(movie);
            if (returnValue == null) return NotFound();
            return Ok(returnValue);
        }

        [HttpPost("InsertMovieRating")]
        public async Task<IActionResult> InsertMovieRating([FromBody]Rating rating)
        {
            var returnValue = movieService.InsertMovieRating(rating);
            if (returnValue == null) return NotFound();
            return Ok(returnValue);
        }

        [HttpPost("AddMovieStaff")]
        [Authorize(Roles = "SU")]
        public async Task<IActionResult> AddMovieStaff([FromBody]MovieStaff movieStaff)
        {
            var returnValue = movieService.AddMovieStaff(movieStaff);
            if (returnValue == null) return NotFound();
            return Ok(returnValue);
        }

        [HttpDelete("DeleteMovieStaff/{id}")]
        [Authorize(Roles = "SU")]
        public async Task<IActionResult> DeleteMovieStaff([FromRoute]long id)
        {
            var returnValue = movieService.DeleteMovieStaff(id);
            if (returnValue == null) return NotFound();
            return Ok(returnValue);
        }

        [HttpDelete("DeleteMovie/{id}")]
        [Authorize(Roles = "SU")]
        public async Task<IActionResult> DeleteMovie([FromRoute]long id)
        {
            var returnValue = movieService.DeleteMovie(id);
            if (returnValue == null) return NotFound();
            return Ok(returnValue);
        }
    }
}