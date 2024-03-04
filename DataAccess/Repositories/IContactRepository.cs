using DataAccess.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IContactRepository
    {
        public Task<ContactEntity?> GetAsync(Guid id);
        public Task<IEnumerable<ContactEntity?>> GetAllAsync(string username);
        public Task<ContactEntity> AddAsync(ContactEntity contact);
        public Task<ContactEntity?> DeleteAsync(Guid id);
        public Task<ContactEntity?> UpdateAsync(Guid id, ContactEntity updatedContact);
        public Task<bool> EmailExistsAsync(string email);
    }
}
