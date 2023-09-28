using GalponIndustrial_API.Context;
using GalponIndustrial_API.Models;
using GalponIndustrial_API.Repository.IRepository;

namespace GalponIndustrial_API.Repository
{
    public class NumeroGalponRepository : Repository<NumeroGalpon>, INumeroGalponRepository
    {
        private readonly ApplicationDbContext _context;

        public NumeroGalponRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<NumeroGalpon> Actualizar(NumeroGalpon entity)
        {
            entity.FechaActualizacion = DateTime.Now;
            _context.NumeroGalpones.Update(entity);
            await Grabar();
            return entity;
        }
    }
}
