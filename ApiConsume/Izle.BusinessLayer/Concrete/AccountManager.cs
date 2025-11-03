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
    public class AccountManager : IAccountService
    {
        private readonly IAccountDal _accountDal;

        public AccountManager(IAccountDal accountDal)
        {
            _accountDal = accountDal;
        }

        public void TDelete(Account t)
        {
            _accountDal.Delete(t);
        }

        public Account TGetByID(int id)
        {
            return _accountDal.GetByID(id);
        }

        public List<Account> TGetList()
        {
            return _accountDal.GetList();
        }

        public void TInsert(Account t)
        {
            _accountDal.Insert(t);
        }

        public void TUpdate(Account t)
        {
            _accountDal.Update(t);
        }
    }
}
