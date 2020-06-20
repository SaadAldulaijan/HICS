using HICS.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HICS.Library.Services
{
    public interface ICodeGroupService
    {
        Task DeleteComposite(int firstKey, int secondKey);
        Task<List<CodeGroup>> Get();
        Task Post(CodeGroup codeGroup);
    }
}