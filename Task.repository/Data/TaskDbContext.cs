using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.core.Models;

namespace Task.repository.Data
{
    public class TaskDbContext :IdentityDbContext<AppUser>
    {
        public DbSet<AddressBook> addressBooks { get; set; }

        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<AddressBook>()
               .HasOne(ab => ab.User) 
               .WithMany(u => u.AddressBooks) 
               .HasForeignKey(ab => ab.UserId) 
               .OnDelete(DeleteBehavior.Cascade); 


        }
    }
}
