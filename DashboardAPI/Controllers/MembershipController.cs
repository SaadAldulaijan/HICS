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
    public class MembershipController : ControllerBase
    {
        #region Properties and Constructor
        private readonly IUnitOfWork<Membership> _membership;
        public MembershipController(IUnitOfWork<Membership> membership)
        {
            _membership = membership;
        }

        #endregion

        #region CRUD 
        [HttpGet]
        public ActionResult<List<Membership>> GetAll()
        {
            var data = _membership.Entity.GetAll().ToList();
            return data;
        }

        [HttpPost]
        public ActionResult Post(Membership membership)
        {
            _membership.Entity.Insert(membership);
            _membership.Save();
            return Ok("Created Successfully");
        }

        

        [HttpDelete("{firstKey}/{secondKey}")]
        public ActionResult DeleteComposite(int firstKey, int secondKey)
        {
            _membership.Entity.DeleteComposite(firstKey, secondKey);
            _membership.Save();
            return Ok("Deleted Successfully");
        }
        #endregion
    }
}
