﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task.core.Models;
using Task.repository.Data;
using Task.repository.Data.Repos;
using Task.application.Dtos;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Task.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AddressBookController : ControllerBase
    {
        private readonly TaskDbContext _dbContext;
        private readonly IaddressBookRepo iaddressBookRepo;
        public AddressBookController(TaskDbContext dbContext,IaddressBookRepo repo)
        {
            _dbContext = dbContext;
            iaddressBookRepo = repo;
            
        }
        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task <IActionResult> getAllAddressBooks()
        {
            var Addressbooks= await iaddressBookRepo.GetAllAdressBooks();
            var AddressbooksDto = new List<AddressBookDto>();
            foreach (var addressbook in Addressbooks) {
                AddressbooksDto.Add(new AddressBookDto
                {
                    FirstName = addressbook.FirstName,
                    LastName = addressbook.LastName,
                    PhoneNumber = addressbook.PhoneNumber,
                    Email = addressbook.Email,
                    Address = addressbook.Address,
                    BirthDate = addressbook.BirthDate
                });
            }
            return Ok(AddressbooksDto);

        }
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> createAddressBook([FromForm] AddressBookDto addressBookDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not found");
            }
            var user = await _dbContext.Users.FindAsync(userId);

            if (user == null) {
                return NotFound("User not found");
            }


            if (addressBookDto == null)
            {
                return BadRequest("Address book data is null");
            }


            var addressBook = new AddressBook
            {
                FirstName = addressBookDto.FirstName,
                LastName = addressBookDto.LastName,
                PhoneNumber = addressBookDto.PhoneNumber,
                Email = addressBookDto.Email,
                Address = addressBookDto.Address,
                BirthDate = addressBookDto.BirthDate,
                UserId = userId,
               // User = user

            };
            var createdAddressBook = await iaddressBookRepo.CreateAddressBook(addressBook);
            return Ok( new { id = createdAddressBook.Id });
        }
        [HttpGet]
        [Route("{id:int}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetById(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not found");
            }
            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }
            if (id <= 0)
            {
                return BadRequest("Invalid address book ID");
            }


            var addressBook = await iaddressBookRepo.GetAddressBookById(id);
            if (addressBook == null)
            {
                return NotFound();
            }
            if (addressBook.UserId != userId)
            {
                return Unauthorized("You are not authorized to access this address book");
            }

            var addressBookDto = new AddressBookDto
            {
                FirstName = addressBook.FirstName,
                LastName = addressBook.LastName,
                PhoneNumber = addressBook.PhoneNumber,
                Email = addressBook.Email,
                Address = addressBook.Address,
                BirthDate = addressBook.BirthDate
            };
            return Ok(addressBookDto);

        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteAddressBook(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not found");
            }

            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }
            if (id <= 0)
            {
                return BadRequest("Invalid address book ID");
            }

            var addressBook = await _dbContext.addressBooks.FindAsync(id);

            if (addressBook == null)
            {
                return NotFound();
            }
            if (addressBook.UserId != userId)
            {
                return Unauthorized("You are not authorized to delete this address book");
            }

            await iaddressBookRepo.DeleteAddressBook(id);
          
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
        [HttpGet("paginated")]
        public async Task<IActionResult> GetPaginatedContacts(
        [FromQuery] string? sortBy,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
        {
            var result = await iaddressBookRepo.GetPaginatedContactsAsync( sortBy, pageNumber, pageSize);
            return Ok(result);
        }
    }
}
