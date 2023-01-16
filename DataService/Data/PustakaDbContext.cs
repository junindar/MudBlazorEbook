using DataService.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Data
{
    public class PustakaDbContext : DbContext
    {
       
        public PustakaDbContext(DbContextOptions<PustakaDbContext>
            options) : base(options)
        {

        }


        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
         public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.ID)
                    .HasName("PRIMARY");

                entity.ToTable("Categories");

               
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.ID)
                    .HasName("PRIMARY");

                entity.ToTable("Books");


            });
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.ID)
                    .HasName("PRIMARY");

                entity.ToTable("Users");


            });
        }
    }
}
