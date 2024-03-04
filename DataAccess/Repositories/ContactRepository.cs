using DataAccess.DataContexts;
using DataAccess.Entitys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ContactRepository: IContactRepository
    {
        private readonly UserManagerContext _userManagerContext;

        public ContactRepository(UserManagerContext _dbContext)
        {
            _userManagerContext = _dbContext;
        }

        public async Task<ContactEntity> AddAsync(ContactEntity contact)
        {
            var result = await _userManagerContext.Contacts.AddAsync(contact);
            await _userManagerContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<ContactEntity?> DeleteAsync(Guid id)
        {
            var contactToRemove = await _userManagerContext.Contacts.FindAsync(id);

            if (contactToRemove != null)
            {
                var result = _userManagerContext.Contacts.Remove(contactToRemove);
                await _userManagerContext.SaveChangesAsync();
                return result.Entity;
            }
            else return null;
        }

        public async Task<IEnumerable<ContactEntity?>> GetAllAsync(string username)
        {
            var contacts = await _userManagerContext.Contacts
                .Include("OwnerUser")
                .Where(c => c.OwnerUser.UserName == username)
                .ToListAsync();

            return contacts;
        }

        public async Task<ContactEntity?> GetAsync(Guid id)
        {
            var contact = await _userManagerContext.Contacts.FindAsync(id);
            if (contact != null)
            {
                return contact;
            }
            else return null;

        }

        public async Task<ContactEntity> UpdateAsync(Guid id, ContactEntity updatedContact)
        {
            var contact = await _userManagerContext.Contacts.FindAsync(id);

            if (contact != null)
            {
                contact.FirstName = updatedContact.FirstName;
                contact.LastName = updatedContact.LastName;
                contact.Email = updatedContact.Email;
                contact.Phone = updatedContact.Phone;
                contact.DateOfBirth = updatedContact.DateOfBirth;

                var result = _userManagerContext.Contacts.Update(contact);
                await _userManagerContext.SaveChangesAsync();
                return result.Entity;
            }

            else return null;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            var result = await _userManagerContext.Contacts.AnyAsync(c => c.Email == email);
            return result;
        }
    }
}
