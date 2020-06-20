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
    public class LocationController : ControllerBase
    {
        #region Properties and Constructor
        private readonly IUnitOfWork<Location> _location;
        public LocationController(IUnitOfWork<Location> location)
        {
            _location = location;
        }

        #endregion

        #region CRUD 
        [HttpGet]
        public ActionResult<List<Location>> GetAll()
        {
            var data = _location.Entity.GetAll().ToList();
            return data;
        }

        [HttpGet("{id}")]
        public ActionResult<Location> GetById(int id)
        {
            var location = _location.Entity.GetById(id);
            if (location == null)
            {
                return NotFound();
            }
            return location;
        }

        [HttpPost]
        public ActionResult Post(Location location)
        {
            _location.Entity.Insert(location);
            _location.Save();
            return Ok("Created Successfully");
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, Location location)
        {
            if (id != location.LocationId)
            {
                return BadRequest();
            }
            _location.Entity.Update(location);
            _location.Save();
            return Ok("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var location = _location.Entity.GetById(id);
            if (location==null)
            {
                return NotFound();
            }
            _location.Entity.Delete(id);
            _location.Save();
            return Ok("Deleted Successfully");
        }
        #endregion
    }
}
