

namespace MVC_BLL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IDepartmentRepository DepartmentRepository { get; set; }
        IEmployeeRepository EmployeeRepository { get; set; }
        Task<int> Complete();
    }
}
