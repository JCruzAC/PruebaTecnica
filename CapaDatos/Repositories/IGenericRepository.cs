namespace CapaDatos.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        bool _isMessageError { get; set; }
        string _message { get; set;  }
        Task<bool> Create(T model);
        IQueryable<T> GetAll();
        Task<T?> GetById(int id);
        Task<bool> Update(T model);
        Task<bool> Delete(int id);
    }
}
