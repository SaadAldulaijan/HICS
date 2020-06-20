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
    public class CodeController : ControllerBase
    {
        #region Properties and Constructor
        private readonly IUnitOfWork<Code> _code;
        public CodeController(IUnitOfWork<Code> code)
        {
            _code = code;
        }

        #endregion

        #region CRUD 
        [HttpGet]
        public ActionResult <List<Code>> GetAll()
        {
            var data = _code.Entity.GetAll().ToList();
            return data;
        }

        [HttpGet("{id}")]
        public ActionResult<Code> GetById(int id)
        {
            var code = _code.Entity.GetById(id);
            if (code == null) return NotFound();
            return code;
        }

        [HttpPost]
        public ActionResult Post(Code code)
        {
            _code.Entity.Insert(code);
            _code.Save();
            return Ok("Created Successfully");
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, Code code)
        {
            if (id != code.CodeId)
            {
                return BadRequest();
            }
            _code.Entity.Update(code);
            _code.Save();
            return Ok("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var code = _code.Entity.GetById(id);
            if (code == null)
            {
                return NotFound();
            }
            _code.Entity.Delete(id);
            _code.Save();
            return Ok("Deleted Successfully");
        }
        #endregion
    }
}
