
namespace MVC_BLL.Repositories
{
    public class DepartmentRepository:GenericRepository<Department>, IDepartmentRepository
    {
        private readonly MVCDataBase mVCDataBase;

        public DepartmentRepository(MVCDataBase _mVCDataBase):base(_mVCDataBase)
        {
            mVCDataBase = _mVCDataBase;
        }
    }
}
