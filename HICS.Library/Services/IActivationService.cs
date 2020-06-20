using HICS.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HICS.Library.Services
{
    public interface IActivationService
    {
        Task Delete(int id);
        Task<List<Activation>> Get();
        Task<Activation> GetById(int id);
        Task Post(Activation activation);
        Task Update(int id, Activation activation);
    }
}