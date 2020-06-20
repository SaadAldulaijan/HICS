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
    public class GroupController : ControllerBase
    {
        #region Properties and Constructor
        private readonly IUnitOfWork<Group> _group;
        public GroupController(IUnitOfWork<Group> group)
        {
            _group = group;
        }

        #endregion

        #region CRUD 
        [HttpGet]
        public ActionResult<List<Group>> GetAll()
        {
            var data = _group.Entity.GetAll().ToList();
            return data;
        }

        [HttpGet("{id}")]
        public ActionResult<Group> GetById(int id)
        {
            var group = _group.Entity.GetById(id);
            if (group == null)
            {
                return NotFound();
            }
            return group;
        }

        [HttpPost]
        public ActionResult Post(Group group)
        {
            _group.Entity.Insert(group);
            _group.Save();
            return Ok("Created Successfully");
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, Group group)
        {
            if (id != group.GroupId)
            {
                return BadRequest();
            }
            _group.Entity.Update(group);
            _group.Save();
            return Ok("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var group = _group.Entity.GetById(id);
            if (group == null)
            {
                return NotFound();
            }
            _group.Entity.Delete(id);
            _group.Save();
            return Ok("Deleted Successfully");
        }
        #endregion
    }
}
