



namespace MVC_BLL.Repositories
{
    public class GenericRepository <T>: IGenericRepository<T> where T : class
    {
        private readonly MVCDataBase _mVCDataBase;

        public GenericRepository(MVCDataBase mVCDataBase)
        {
            this._mVCDataBase = mVCDataBase;
        }
        public async Task Add(T entity)
        {
            await _mVCDataBase.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _mVCDataBase.Set<T>().Remove(entity);
        }

        public async Task<T> Get(int id)
        {
            return await _mVCDataBase.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            if (typeof(T) == typeof(Employee))
                return await _mVCDataBase.Employees.Include(p => p.Department).ToListAsync() as IEnumerable<T>;
            return await _mVCDataBase.Set<T>().ToListAsync();
            /*         return    await _mVCDataBase.Set<T>().ToListAsync() as List<T>()*/
        }
 
        public void Update(T entity)
        {
            _mVCDataBase.Set<T>().Update(entity);
        }

    }
}
