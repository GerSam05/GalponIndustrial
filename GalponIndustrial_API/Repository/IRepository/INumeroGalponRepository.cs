using GalponIndustrial_API.Models;

namespace GalponIndustrial_API.Repository.IRepository
{
    public interface INumeroGalponRepository : IRepository<NumeroGalpon> 
    {
        Task<NumeroGalpon> Actualizar(NumeroGalpon entity);
    }
}
