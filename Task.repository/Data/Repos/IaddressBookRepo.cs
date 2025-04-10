using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.application.Response;
using Task.core.Models;

namespace Task.repository.Data.Repos
{
    public interface IaddressBookRepo
    {



        #region GetAll
        Task<List<AddressBook>> GetAllAdressBooks();
        #endregion
        
        #region CreateAddressBook 
        Task<AddressBook> CreateAddressBook(AddressBook addressBook);
        #endregion

        #region GetbyId
        Task<AddressBook?> GetAddressBookById(int id);
        #endregion

        #region Delete

        Task<AddressBook?> DeleteAddressBook(int id);
        #endregion

        #region Sorting
        Task<PaginatedResult<AddressBook>> GetPaginatedContactsAsync( string? sortBy, int pageNumber, int pageSize);

        #endregion


    }
}
