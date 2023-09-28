using GalponIndustrial_API.Context;
using GalponIndustrial_API.Models;
using GalponIndustrial_API.Repository.IRepository;

namespace GalponIndustrial_API.Repository
{
    public class GalponRepository : Repository<Galpon>, IGalponRepository
    {
        private readonly ApplicationDbContext _context;

        public GalponRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Galpon> Actualizar(Galpon entity)
        {
            entity.FechaActualizacion = DateTime.Now;
            _context.Galpons.Update(entity);
            await Grabar();
            return entity;
        }
    }
}
