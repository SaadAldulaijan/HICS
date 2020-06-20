using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Entities;
using CoreLibrary.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DashboardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodeGroupController : ControllerBase
    {
        private readonly IUnitOfWork<CodeGroup> _codeGroup;

        public CodeGroupController(IUnitOfWork<CodeGroup> codeGroup)
        {
            _codeGroup = codeGroup;
        }

        #region CRUD 
        [HttpGet]
        public ActionResult<List<CodeGroup>> GetAll()
        {
            var data = _codeGroup.Entity.GetAll().ToList();
            return data;
        }

        [HttpPost]
        public ActionResult Post(CodeGroup codeGroup)
        {
            _codeGroup.Entity.Insert(codeGroup);
            _codeGroup.Save();
            return Ok("Created Successfully");
        }



        [HttpDelete("{firstKey}/{secondKey}")]
        public ActionResult DeleteComposite(int firstKey, int secondKey)
        {
            _codeGroup.Entity.DeleteComposite(firstKey, secondKey);
            _codeGroup.Save();
            return Ok("Deleted Successfully");
        }
        #endregion
    }
}
