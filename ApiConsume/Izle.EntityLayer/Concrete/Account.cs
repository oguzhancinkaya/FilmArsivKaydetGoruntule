using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Izle.EntityLayer.Concrete
{
    public class Account
    {
        [Key]
        public int UserId { get; set; }
        public string UserNameSurname { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }

    }
}
