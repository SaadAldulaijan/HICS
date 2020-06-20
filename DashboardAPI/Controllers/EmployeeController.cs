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
    public class EmployeeController : ControllerBase
    {
        #region Properties and Constructor
        private readonly IUnitOfWork<Employee> _employee;
        public EmployeeController(IUnitOfWork<Employee> employee)
        {
            _employee = employee;
        }

        #endregion

        #region CRUD 
        [HttpGet]
        public ActionResult<List<Employee>> GetAll()
        {
            var data = _employee.Entity.GetAll().ToList();
            return data;
        }

        [HttpGet("{id}")]
        public ActionResult<Employee> GetById(int id)
        {
            var code = _employee.Entity.GetById(id);
            if (code == null) return NotFound();
            return code;
        }

        [HttpPost]
        public ActionResult Post(Employee code)
        {
            _employee.Entity.Insert(code);
            _employee.Save();
            return Ok("Created Successfully");
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, Employee employee)
        {
            if (id != employee.ID)
            {
                return BadRequest();
            }
            _employee.Entity.Update(employee);
            _employee.Save();
            return Ok("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var code = _employee.Entity.GetById(id);
            if (code == null)
            {
                return NotFound();
            }
            _employee.Entity.Delete(id);
            _employee.Save();
            return Ok("Deleted Successfully");
        }
        #endregion
    }
}
