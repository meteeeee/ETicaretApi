using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Domain.Entities
{
    public class User
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Gender { get; set; }
        public string Email { get; set; }
        public string UserName {  get; set; }
        public string Password { get; set; }
        public int PhoneNumber { get; set; }
        public string Adress { get; set; }
        public bool isAdmin { get; set; }
    }
}
