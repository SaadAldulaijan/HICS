using HICS.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HICS.Library.Services
{
    public interface ICodeService
    {
        Task Delete(int id);
        Task<List<Code>> Get();
        Task<Code> GetById(int id);
        Task Post(Code code);
        Task Update(int id, Code code);
    }
}