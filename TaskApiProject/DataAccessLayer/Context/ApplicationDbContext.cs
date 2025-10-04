using DataAccessLayer.Entites.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Contact relationship
            builder.Entity<Contact>()
                .HasOne(contact => contact.Owner)
                .WithMany(user => user.Contacts)
                .HasForeignKey(contact => contact.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(builder);
        }
        public DbSet<Contact> Contacts { get; set; }

    }
}
