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
        }
    }
}
