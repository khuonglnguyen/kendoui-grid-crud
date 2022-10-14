using TelerikMvcAppDemo.Interfaces;
using TelerikMvcAppDemo.Models;
using TelerikMvcAppDemo.Repositories;

namespace TelerikMvcAppDemo.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EmployeeEntities _context;
        public UnitOfWork(EmployeeEntities context)
        {
            _context = context;
            Employees = new EmployeeRepository(_context);
        }
        public IEmployeeRepository Employees { get; private set; }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
