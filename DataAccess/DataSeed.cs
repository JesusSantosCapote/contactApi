using DataAccess.DataContexts;
using DataAccess.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class DataSeed
    {
        public static async Task Seed(UserManagerContext context) 
        {
            UserEntity user1 = new UserEntity()
            {
                Id = Guid.Parse("A6768765-57D8-4D34-B5FE-49517787E2E2"),
                FirstName = "Johnny",
                LastName = "Rocket",
                UserName = "jrocket@example.com",
                Password = "Password",
                Roles = "Administrator"
            };

            UserEntity user2 = new UserEntity()
            {
                Id = Guid.Parse("F26B9D01-E1A4-4F23-89D9-422F43426A89"),
                FirstName = "Italian",
                LastName = "Rocket",
                UserName = "italian@example.com",
                Password = "Password",
                Roles = "Administrator"
            };

            UserEntity user3 = new UserEntity()
            {
                Id = Guid.Parse("532426C3-72E0-4E7C-AAFD-C15C27042F0A"),
                FirstName = "Larian",
                LastName = "Studio",
                UserName = "larian@example.com",
                Password = "Password",
                Roles = ""
            };

            var resultUser1 = await context.Users.FindAsync(user1.Id);
            var resultUser2 = await context.Users.FindAsync(user2.Id);
            var resultUser3 = await context.Users.FindAsync(user3.Id);

            if (resultUser1 == null)
            { 
                await context.Users.AddAsync(user1);
            }
            if (resultUser2 == null)
            {
                await context.Users.AddAsync(user2);
            }
            if (resultUser3 == null)
            {
                await context.Users.AddAsync(user3);
            }
            await context.SaveChangesAsync();

            ContactEntity contactToGet = new ContactEntity()
            {
                Id = Guid.Parse("E4131FF0-3242-4DE6-840C-E2E437373EA9"),
                FirstName = "Lazael",
                LastName = "Githyanki",
                Email = "lazael@email.com",
                DateOfBirth = DateTime.Parse("2001-01-01"),
                Phone = "123455",
                Owner = Guid.Parse("A6768765-57D8-4D34-B5FE-49517787E2E2"),
            };

            ContactEntity contactToUpdate = new ContactEntity()
            {
                Id = Guid.Parse("5AB1DDB3-F6D3-47DC-97FF-3B837D7CA3E7"),
                FirstName = "Karlach",
                LastName = "Elthurel",
                Email = "karlach@email.com",
                DateOfBirth = DateTime.Parse("2004-01-01"),
                Phone = "123455",
                Owner = Guid.Parse("A6768765-57D8-4D34-B5FE-49517787E2E2"),
            };

            ContactEntity contactToDelete = new ContactEntity()
            {
                Id = Guid.Parse("9051BC69-9D26-4D6F-8CC6-63F5F2C31550"),
                FirstName = "Gale",
                LastName = "Waterdeep",
                Email = "gale@email.com",
                DateOfBirth = DateTime.Parse("2002-01-01"),
                Phone = "123455",
                Owner = Guid.Parse("A6768765-57D8-4D34-B5FE-49517787E2E2"),
            };

            var resultContactToGet = await context.Contacts.FindAsync(contactToGet.Id);
            var resultContactToUpdate = await context.Contacts.FindAsync(contactToUpdate.Id);
            var resultContactToDelete = await context.Contacts.FindAsync(contactToDelete.Id);

            if (resultContactToGet == null)
            {
                await context.Contacts.AddAsync(contactToGet);
            }
            if (resultContactToUpdate == null) 
            {
                await context.AddAsync(contactToUpdate);
            }
            if (resultContactToDelete == null)
            {
                await context.AddAsync(contactToDelete);
            }

            await context.SaveChangesAsync();
        }
    }
}
