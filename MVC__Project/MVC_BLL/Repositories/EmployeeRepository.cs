

namespace MVC_BLL.Repositories
{
    public class EmployeeRepository:GenericRepository<Employee>,IEmployeeRepository
    {
        private readonly MVCDataBase _mVCDataBase;

        public EmployeeRepository(MVCDataBase mVCDataBase):base(mVCDataBase) 
        {
            _mVCDataBase = mVCDataBase;
        }

        public IQueryable<Employee> GetEmployeesByAddress(string address)
        {
            return _mVCDataBase.Employees.Where(p => p.Address == address);
        }

        public IQueryable<Employee> GetEmployeesByName(string name)
        {
            return _mVCDataBase.Employees.Where(p => p.Name.ToLower().Contains(name.ToLower()));
        }
    }
}
