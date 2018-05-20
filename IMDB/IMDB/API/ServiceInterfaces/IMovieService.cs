using IMDB.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDB.API.ServiceInterfaces
{
    public interface IMovieService
    {
        List<Movie> GetTop100RatedMovies();
        List<Movie> SearchMovieByTitle(string title);
        long InsertMovie(Movie movie);
        Movie UpdateMovie(Movie movie);
        decimal InsertMovieRating(Rating rating);
        long AddMovieStaff(MovieStaff movieStaff);
        MovieStaff DeleteMovieStaff(long id);
        Movie GetMovieById(long id);
        List<Movie> LoadMovies();
        Movie DeleteMovie(long id);
    }
}
