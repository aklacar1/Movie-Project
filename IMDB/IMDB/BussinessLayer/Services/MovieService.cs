using IMDB.API.ServiceInterfaces;
using IMDB.DataLayer.Entities;
using IMDB.Entities;
using IMDB.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDB.BussinessLayer.Services
{
    internal partial class MovieService: IMovieService
    {
        private readonly MovieDBContext movieDB;

        public MovieService(MovieDBContext movieDB)
        {
            this.movieDB = movieDB;
        }
        /// <summary>
        /// These methods Get entire data about top 10 movies and return those movies.
        /// </summary>
        /// <returns></returns>
        #region Get Movie Data
        public List<Movie> GetTop100RatedMovies() {
            return movieDB.Movie
                .Include(Genre => Genre.Genre)
                .Include(c=>c.Company)
                .Include(M=>M.MovieStaff).ThenInclude(e=>e.PersonJobs).ThenInclude(e=>e.Job)
                .Include(M => M.MovieStaff).ThenInclude(e => e.PersonJobs).ThenInclude(e => e.Person)
                .OrderByDescending(x => x.Rating).Take(100).ToList();
        }
        public List<Movie> LoadMovies()
        {
            return movieDB.Movie
                .Include(Genre => Genre.Genre)
                .Include(c => c.Company)
                .Include(M => M.MovieStaff).ThenInclude(e => e.PersonJobs).ThenInclude(e => e.Job)
                .Include(M => M.MovieStaff).ThenInclude(e => e.PersonJobs).ThenInclude(e => e.Person)
                .OrderByDescending(x => x.Rating).ToList();
        }
        public Movie GetMovieById(long id)
        {
            return movieDB.Movie
                .Include(Genre => Genre.Genre)
                .Include(c => c.Company)
                .Include(M => M.MovieStaff).ThenInclude(e => e.PersonJobs).ThenInclude(e => e.Job)
                .Include(M => M.MovieStaff).ThenInclude(e => e.PersonJobs).ThenInclude(e => e.Person)
                .SingleOrDefault(x=> x.MovieId== id);
        }
        public List<Movie> SearchMovieByTitle(string title) {
            return movieDB.Movie
                .Include(Genre => Genre.Genre)
                .Include(c => c.Company)
                .Include(M => M.MovieStaff).ThenInclude(e => e.PersonJobs).ThenInclude(e => e.Job)
                .Include(M => M.MovieStaff).ThenInclude(e => e.PersonJobs).ThenInclude(e => e.Person)
                .Where(x=> x.Title.Contains(title))
                .OrderByDescending(x => x.Rating)
                .Take(10)
                .ToList();

        }
        #endregion
        #region Base Movie Manipulation
        /// <summary>
        /// Inserts movie and its subdependencies if any supplied. Considering it is new movie, its ratingis set to 5.
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        public long InsertMovie(Movie movie) {
            movie.Rating = 5;
            movie.Company = null;
            movie.Genre = null;
            movie.RatingNavigation = null;
            movie.MovieStaff = null;
            movieDB.Movie.Add(movie);
            return movie.MovieId;
        }
        /// <summary>
        /// Update movie, this method is used to also update Company of a Movie
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        public Movie UpdateMovie(Movie movie)
        {
            movieDB.Entry(movie).State = EntityState.Modified;
            movieDB.SaveChanges();
            return movie;
        }

        public Movie DeleteMovie(long id)
        {
            Movie movie = movieDB.Movie.Find(id);
            movieDB.Movie.Remove(movie);
            movieDB.SaveChanges();
            return movie;
        }
        #endregion
        #region Rating Manipulation
        /// <summary>
        /// Inserts new rating and calculates rating for the movie. There is no changing of this data, therefore only insert exists.
        /// </summary>
        /// <param name="rating"></param>
        /// <returns></returns>
        public decimal InsertMovieRating(Rating rating)
        {
            rating.Movie = null;
            movieDB.Rating.Add(rating);
            movieDB.SaveChanges();
            Dictionary<int,int> ratings = movieDB.SqlQuery<int,int>(System.Data.CommandType.Text, "SELECT SUM(Rating) as Rating,COUNT(MovieID) as Movies FROM Rating WHERE MovieID = "+rating.MovieId,"Rating","Movies") as Dictionary<int,int>;
            Movie movie = movieDB.Movie.Find(rating.MovieId);
            movie.Rating = (decimal)ratings.FirstOrDefault().Key/(decimal)ratings.FirstOrDefault().Value;
            movieDB.Entry(movie).State = EntityState.Modified;
            movieDB.SaveChanges();
            return movie.Rating;
        }
        #endregion
        #region Movie Staff manipulation
        /// <summary>
        /// In order to add new Actor or someone else to Movie.
        /// </summary>
        /// <param name="movieStaff"></param>
        /// <returns></returns>
        public long AddMovieStaff(MovieStaff movieStaff) {
            movieDB.MovieStaff.Add(movieStaff);
            movieDB.SaveChanges();
            return movieStaff.Id;
        }
        /// <summary>
        /// Delete Staff from movie.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MovieStaff DeleteMovieStaff(long id)
        {
            MovieStaff movieStaff = movieDB.MovieStaff.Find(id);
            movieDB.MovieStaff.Remove(movieStaff);
            movieDB.SaveChanges();
            return movieStaff;
        }
        #endregion
    }
}
