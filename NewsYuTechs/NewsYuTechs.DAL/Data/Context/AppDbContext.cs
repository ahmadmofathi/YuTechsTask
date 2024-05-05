using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;

namespace NewsYuTechs.DAL
{
    public class AppDbContext : IdentityDbContext<Admin, IdentityRole<string>, string>
    {
        public AppDbContext()
        {

        }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<News> News { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(a => a.AuthorId);
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.HasKey(a => a.NewsId);
            });
        }
    }
}
