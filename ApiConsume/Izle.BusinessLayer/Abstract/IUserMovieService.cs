using Izle.EntityLayer.Concrete;
using System.Collections.Generic;

namespace Izle.BusinessLayer.Abstract
{
    public interface IUserMovieService : IGenericService<UserMovie>
    {
        List<UserMovie> TGetUserMoviesWithMovie(int userId);
        List<UserMovie> TGetUserMoviesByStatus(int userId, string status);
        UserMovie TGetUserMovie(int userId, int movieId);
        bool TAddOrUpdateUserMovie(int userId, int movieId, string status);
        bool TRemoveUserMovie(int userId, int movieId);
    }
}