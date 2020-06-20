using HICS.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HICS.Library.Services
{
    public interface ILocationService
    {
        Task<List<Location>> Get();
        Task<Location> GetById(int id);
        Task Post(Location location);
        Task Update(int id, Location location);
        Task Delete(int id);
    }
}
