using DataAccess.Entitys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataContexts
{
    public class UserManagerContext: DbContext
    {
        public UserManagerContext(DbContextOptions<UserManagerContext> options) : base(options) { }

        public DbSet<ContactEntity> Contacts { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactEntity>()
                .HasOne(c => c.OwnerUser)
                .WithMany(u => u.Contacts)
                .HasForeignKey(c => c.Owner)
                .IsRequired();

            modelBuilder.Entity<ContactEntity>()
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder.Entity<UserEntity>()
                .HasIndex(u => u.UserName)
                .IsUnique();

            //UserEntity user1 = new UserEntity() 
            //{
            //    Id = Guid.Parse("A6768765-57D8-4D34-B5FE-49517787E2E2"),
            //    FirstName = "Johnny",
            //    LastName = "Rocket",
            //    UserName = "jrocket@example.com",
            //    Password = "Password",
            //    Roles = "Administrator"
            //};
            //modelBuilder.Entity<UserEntity>().HasData(user1);
        }
    }
}
