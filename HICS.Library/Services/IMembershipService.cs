using HICS.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HICS.Library.Services
{
    public interface IMembershipService
    {
        Task DeleteComposite(int firstKey, int secondKey);
        Task<List<Membership>> Get();
        Task Post(Membership membership);
    }
}