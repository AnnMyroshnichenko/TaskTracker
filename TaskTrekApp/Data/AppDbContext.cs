using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskTrekApp.Models;

namespace TaskTrekApp.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public DbSet<TaskCard> Tasks {  get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Column> Columns { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
