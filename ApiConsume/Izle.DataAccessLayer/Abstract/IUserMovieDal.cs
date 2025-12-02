using Izle.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Izle.DataAccessLayer.Abstract
{
    public interface IUserMovieDal : IGenericDal<UserMovie>
    {
        List<UserMovie> GetUserMoviesWithMovie(int userId);
        List<UserMovie> GetUserMoviesByStatus(int userId, string status);
        UserMovie GetUserMovie(int userId, int movieId);
    }
}
