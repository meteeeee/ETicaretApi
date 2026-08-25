using Microsoft.AspNetCore.Identity;
using System;

namespace ETicaretApi.Persistence.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Gender { get; set; }
        public string Address { get; set; }
    }
}
