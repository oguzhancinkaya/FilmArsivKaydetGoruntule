using Izle.DataAccessLayer.Abstract;
using Izle.DataAccessLayer.Concrete;
using Izle.DataAccessLayer.Repositories;
using Izle.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Izle.DataAccessLayer.EntityFramework
{
    public class EfMovieDal : GenericRepository<Movie>, IMovieDal
    {
        public EfMovieDal(Context context) : base(context)
        {

        }
    }
}
