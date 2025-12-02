using Izle.DataAccessLayer.Abstract;
using Izle.DataAccessLayer.Concrete;
using Izle.DataAccessLayer.Repositories;
using Izle.EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Izle.DataAccessLayer.EntityFramework
{
    public class EfUserMovieDal : GenericRepository<UserMovie>, IUserMovieDal
    {
        public EfUserMovieDal(Context context) : base(context)
        {
        }

        public List<UserMovie> GetUserMoviesWithMovie(int userId)
        {
            using var context = new Context();
            return context.UserMovies
                .Include(um => um.Movie)
                .Where(um => um.UserId == userId)
                .ToList();
        }

        public List<UserMovie> GetUserMoviesByStatus(int userId, string status)
        {
            using var context = new Context();
            return context.UserMovies
                .Include(um => um.Movie)
                .Where(um => um.UserId == userId && um.Status == status)
                .OrderByDescending(um => um.AddedDate)
                .ToList();
        }

        public UserMovie GetUserMovie(int userId, int movieId)
        {
            using var context = new Context();
            return context.UserMovies
                .FirstOrDefault(um => um.UserId == userId && um.MovieId == movieId);
        }
    }
}