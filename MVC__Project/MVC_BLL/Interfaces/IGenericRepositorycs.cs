

namespace MVC_BLL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task Add(T entity);
        Task<T> Get(int id);
        void Update(T entity);
        void Delete(T entity);

    }
}
