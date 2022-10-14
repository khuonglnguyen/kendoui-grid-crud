using System;
using TelerikMvcAppDemo.Interfaces;

namespace TelerikMvcAppDemo.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employees { get; }
        int Complete();
    }
}
