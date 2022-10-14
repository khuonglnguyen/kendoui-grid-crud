using TelerikMvcAppDemo.Interfaces;
using TelerikMvcAppDemo.Models;

namespace TelerikMvcAppDemo.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(EmployeeEntities context) : base(context)
        {
        }
    }
}