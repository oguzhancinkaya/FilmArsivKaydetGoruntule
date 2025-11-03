using Izle.BusinessLayer.Abstract;
using Izle.DataAccessLayer.Abstract;
using Izle.DataAccessLayer.EntityFramework;
using Izle.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Izle.BusinessLayer.Concrete
{
    public class MovieManager : IMovieService
    {
        private readonly IMovieDal _movieDal;

        public MovieManager(IMovieDal movieDal)
        {
            _movieDal = movieDal;
        }

        public void TDelete(Movie t)
        {
            _movieDal.Delete(t);
        }

        public Movie TGetByID(int id)
        {
            return _movieDal.GetByID(id);
        }

        public List<Movie> TGetList()
        {
            return _movieDal.GetList();
        }

        public void TInsert(Movie t)
        {
            _movieDal.Insert(t);
        }

        public void TUpdate(Movie t)
        {
            _movieDal.Update(t);
        }
    }
}
