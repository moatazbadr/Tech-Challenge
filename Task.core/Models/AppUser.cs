using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.core.Models
{
    public class AppUser:IdentityUser
    {

        public ICollection<AddressBook> AddressBooks { get; set; } = new List<AddressBook>();


    }
}
