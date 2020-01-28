using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VisualAcademy.Models
{
    public class IdeaRepository : IIdeaRepository
    {
        private readonly IdeaDbContext _context;

        public IdeaRepository(IdeaDbContext context)
        {
            this._context = context;
        }

        public async Task<Idea> AddIdea(Idea idea)
        {
            _context.Ideas.Add(idea);
            await _context.SaveChangesAsync();
            return idea; 
        }

        public async Task<List<Idea>> GetIdeas()
        {
            return await _context.Ideas.ToListAsync();
        }
    }
}
