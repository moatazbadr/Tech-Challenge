using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.application.Response;
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


        public async Task<PaginatedResult<AddressBook>> GetPaginatedContactsAsync(string? sortBy, int pageNumber, int pageSize)
        {
            var query = dbContext.addressBooks.AsQueryable();
            if (!string.IsNullOrEmpty(sortBy))
            {
                query = sortBy.ToLower() switch
                {
                    "firstname" => query.OrderBy(c => c.FirstName),
                    "lastname" => query.OrderBy(c => c.LastName),
                    "email" => query.OrderBy(c => c.Email),
                    _ => query
                };
            }
            var totalCount = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedResult<AddressBook>
            {
                TotalCount = totalCount,
                Items = items
            };
        }
    }





}
