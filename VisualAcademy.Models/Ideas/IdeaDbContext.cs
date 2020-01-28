using Microsoft.EntityFrameworkCore;

namespace VisualAcademy.Models
{
    public class IdeaDbContext : DbContext
    {
        public IdeaDbContext(DbContextOptions<IdeaDbContext> options)
            : base(options)
        {
        }

        public DbSet<Idea> Ideas { get; set; }
    }
}
