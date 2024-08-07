using Microsoft.EntityFrameworkCore;
using Task = TrackingCalendar.Models.Entity.Task;

namespace TrackingCalendar.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Task> Tasks { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Task>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<Task>()
            .Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(100);
    }
}