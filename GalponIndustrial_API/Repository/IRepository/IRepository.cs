using System.Linq.Expressions;

namespace GalponIndustrial_API.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task Crear (T entity);
        Task<List<T>> ObtenerTodos(Expression<Func<T,bool>> filtro=null);
        Task<T> Obtener(Expression<Func<T, bool>> filtro = null, bool Tracked = true);
        Task Borrar (T entity);
        Task Grabar();

    }
}
