using EntityFramworkProject.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFramworkProject.Repository
{
    public class EmployeeRepository : IRepository<Employee>
    {
        private ProgramContext _context;

        public EmployeeRepository(ProgramContext context) 
        {
            _context = context;
        }
        public async Task<IEnumerable<Employee>> Get()
            => await _context.Employees.ToListAsync();

        public async Task<Employee> GetById(int id)
            => await _context.Employees.FindAsync(id);

        public async Task Add(Employee entity)
            => await _context.Employees.AddAsync(entity);

        public void Update(Employee entity)
        {
            _context.Employees.Attach(entity);
            _context.Employees.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Employee entity)
            => _context.Employees.Remove(entity);

        public async Task Save() => await _context.SaveChangesAsync();

    }
}
