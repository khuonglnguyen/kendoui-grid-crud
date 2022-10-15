using TelerikMvcAppDemo.Models;
using TelerikMvcAppDemo.Repositories;

namespace TelerikMvcAppDemo.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Employee CheckLogin(string email, string password);
    }
}
