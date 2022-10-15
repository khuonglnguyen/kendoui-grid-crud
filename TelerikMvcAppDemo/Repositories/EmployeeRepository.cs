using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TelerikMvcAppDemo.Interfaces;
using TelerikMvcAppDemo.Models;

namespace TelerikMvcAppDemo.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(EmployeeEntities context) : base(context)
        {
        }


        public Employee CheckLogin(string email, string password)
        {
            return _context.Employees.FirstOrDefault(x => x.Email == email && x.Password == password);
        }
    }
}