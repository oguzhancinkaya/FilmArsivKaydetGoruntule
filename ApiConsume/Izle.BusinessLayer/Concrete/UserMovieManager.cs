using Izle.BusinessLayer.Abstract;
using Izle.DataAccessLayer.Abstract;
using Izle.EntityLayer.Concrete;
using System;
using System.Collections.Generic;

namespace Izle.BusinessLayer.Concrete
{
    public class UserMovieManager : IUserMovieService
    {
        private readonly IUserMovieDal _userMovieDal;

        public UserMovieManager(IUserMovieDal userMovieDal)
        {
            _userMovieDal = userMovieDal;
        }

        public void TDelete(UserMovie t)
        {
            _userMovieDal.Delete(t);
        }

        public UserMovie TGetByID(int id)
        {
            return _userMovieDal.GetByID(id);
        }

        public List<UserMovie> TGetList()
        {
            return _userMovieDal.GetList();
        }

        public void TInsert(UserMovie t)
        {
            _userMovieDal.Insert(t);
        }

        public void TUpdate(UserMovie t)
        {
            _userMovieDal.Update(t);
        }

        public List<UserMovie> TGetUserMoviesWithMovie(int userId)
        {
            return _userMovieDal.GetUserMoviesWithMovie(userId);
        }

        public List<UserMovie> TGetUserMoviesByStatus(int userId, string status)
        {
            return _userMovieDal.GetUserMoviesByStatus(userId, status);
        }

        public UserMovie TGetUserMovie(int userId, int movieId)
        {
            return _userMovieDal.GetUserMovie(userId, movieId);
        }

        public bool TAddOrUpdateUserMovie(int userId, int movieId, string status)
        {
            try
            {
                var existingEntry = _userMovieDal.GetUserMovie(userId, movieId);

                if (existingEntry != null)
                {
                    existingEntry.Status = status;
                    existingEntry.AddedDate = DateTime.Now;
                    _userMovieDal.Update(existingEntry);
                }
                else
                {
                    var newEntry = new UserMovie
                    {
                        UserId = userId,
                        MovieId = movieId,
                        Status = status,
                        AddedDate = DateTime.Now
                    };
                    _userMovieDal.Insert(newEntry);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool TRemoveUserMovie(int userId, int movieId)
        {
            try
            {
                var entry = _userMovieDal.GetUserMovie(userId, movieId);
                if (entry != null)
                {
                    _userMovieDal.Delete(entry);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}