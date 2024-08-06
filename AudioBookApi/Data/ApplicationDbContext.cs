using AudioBookApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AudioBookApi.Data
{
    public class ApplicationDbContext: IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {
            
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<PersonalPage> PersonalPages { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            
            builder.Entity<PersonalPage>(p => p.HasKey(u => new { u.UserId, u.BookId }));
            builder.Entity<PersonalPage>()
                .HasOne(u => u.User)
                .WithMany(u => u.Pages)
                .HasForeignKey(p => p.UserId);

            builder.Entity<PersonalPage>()
              .HasOne(u => u.Book)
              .WithMany(u => u.Pages)
              .HasForeignKey(p => p.BookId);
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Customer",
                    NormalizedName= "CUST",
                },
                new IdentityRole
                {
                    Name = "Narrator",
                    NormalizedName= "NAR",
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);

        }


    }
}
