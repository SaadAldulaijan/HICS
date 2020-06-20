using HICS.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HICS.Library.Services
{
    public interface IGroupService
    {
        Task Delete(int id);
        Task<List<Group>> Get();
        Task<Group> GetById(int id);
        Task Post(Group group);
        Task Update(int id, Group group);
    }
}