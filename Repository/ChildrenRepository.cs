using EntityFramworkProject.Models;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.EntityFrameworkCore;

namespace EntityFramworkProject.Repository
{
    public class ChildrenRepository : IRepository<Children>
    {
        private ProgramContext _context;

        public ChildrenRepository(ProgramContext context) 
        {
            _context = context;
        }

        public async Task Add(Children entity)
            => await _context.Childrens.AddAsync(entity);

        public async void Delete(Children entity)
            => _context.Remove(entity);

        public async Task<IEnumerable<Children>> Get()
            => await _context.Childrens.Include(x => x.Parent).ToListAsync();

        public async Task<Children> GetById(int id)
            => await _context.Childrens.FindAsync(id);
            //=> await _context.Childrens.Include(x => x.Parent).FindAsync(id);

        public async Task Save()
            => await _context.SaveChangesAsync();

        public IEnumerable<Children> Search(Func<Children, bool> filter)
            => _context.Childrens.Where(filter).ToList();

        public void Update(Children entity)
        {
            _context.Childrens.Attach(entity);
            _context.Childrens.Entry(entity).State = EntityState.Modified;
        }
    }
}
