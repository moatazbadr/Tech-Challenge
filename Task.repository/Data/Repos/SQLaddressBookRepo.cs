using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.core.Models;

namespace Task.repository.Data.Repos
{
    public class SQLaddressBookRepo :IaddressBookRepo
    {
        private readonly TaskDbContext dbContext;

        public SQLaddressBookRepo(TaskDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<AddressBook>> GetAllAdressBooks()
        {
           return await dbContext.addressBooks.ToListAsync();

        }
        public async Task<AddressBook> CreateAddressBook(AddressBook addressBook)
        {
            await dbContext.addressBooks.AddAsync(addressBook);
            await dbContext.SaveChangesAsync();
            return addressBook;
        }
        public async Task<AddressBook?> GetAddressBookById(int id)
        {
            return await dbContext.addressBooks.FirstOrDefaultAsync(x=>x.Id==id);
        }

       public async Task<AddressBook?> DeleteAddressBook(int id)
        {
            var addressBook = await dbContext.addressBooks.FirstOrDefaultAsync(x => x.Id == id);
            if (addressBook != null)
            {
                dbContext.addressBooks.Remove(addressBook);
                await dbContext.SaveChangesAsync();
                return addressBook;
            }
            return null;


        }
    }
}
