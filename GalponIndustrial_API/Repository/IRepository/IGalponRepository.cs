using GalponIndustrial_API.Models;

namespace GalponIndustrial_API.Repository.IRepository
{
    public interface IGalponRepository : IRepository<Galpon> 
    {
        Task<Galpon> Actualizar(Galpon entity);
    }
}
