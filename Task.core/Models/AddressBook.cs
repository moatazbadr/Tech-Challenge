using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.core.Models
{
    public class AddressBook
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateOnly BirthDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);


        public string UserId { get; set; } = string.Empty;
        public AppUser User
        {
            get; set;
        }
    }
}
