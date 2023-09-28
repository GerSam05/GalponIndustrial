using GalponIndustrial_API.Context;
using GalponIndustrial_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GalponIndustrial_API.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            this._dbSet = _context.Set<T>();
        }
        public async Task Borrar(T entity)
        {
            _dbSet.Remove(entity);
            await Grabar();
        }

        public async Task Crear(T entity)
        {
            await _dbSet.AddAsync(entity);
            await Grabar();
        }

        public async Task Grabar()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<T> Obtener(Expression<Func<T, bool>> filtro = null, bool Tracked = true)
        {
            IQueryable<T> query = _dbSet;
            if (!Tracked)
            {
                query = query.AsNoTracking();
            }
            if (filtro != null)
            {
                query = query.Where(filtro);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> ObtenerTodos(Expression<Func<T, bool>> filtro = null)
        {
            IQueryable<T> query = _dbSet;
            if(filtro != null)
            {
                query = query.Where(filtro);
            }
            return await query.ToListAsync();
        }
    }
}
