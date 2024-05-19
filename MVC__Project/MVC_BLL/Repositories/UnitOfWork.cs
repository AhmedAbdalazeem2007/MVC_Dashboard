

namespace MVC_BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MVCDataBase _mVCDataBase;

        public IDepartmentRepository DepartmentRepository { get; set; }
        public IEmployeeRepository EmployeeRepository { get; set; }
        public UnitOfWork(MVCDataBase mVCDataBase)
        {
            _mVCDataBase = mVCDataBase;
            DepartmentRepository = new DepartmentRepository(_mVCDataBase);
            EmployeeRepository = new EmployeeRepository(_mVCDataBase);
        }

        public async Task<int> Complete()
        {
            return await _mVCDataBase.SaveChangesAsync();
        }
        public void Dispose()
        {
            _mVCDataBase.Dispose();
        }

    }
}
