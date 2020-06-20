using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Entities;
using CoreLibrary.Interfaces;
using DashboardAPI.ViewModels;
using InfrastructureLibrary.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace DashboardAPI.Controllers
{
    /// <summary>
    /// the goal of this controller is to get the details about an activation
    /// and send real time data to blazor app. 
    /// there will be no inserting, updating, deleting actions here.
    /// dashboard is for read-only.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        /// <summary>
        /// TODO:  
        /// </summary>
        #region Properties and Constructor
        private readonly IUnitOfWork<Activation> _activation;
        private readonly IUnitOfWork<Code> _code;
        private readonly IUnitOfWork<Location> _location;
        private readonly IUnitOfWork<Employee> _employee;
        private readonly IHubContext<ReportingHub> _hub;

        public DashboardController(IUnitOfWork<Activation> activation,
                                   IUnitOfWork<Code> code,
                                   IUnitOfWork<Location> location,
                                   IUnitOfWork<Employee> employee,
                                   IHubContext<ReportingHub> hub)
        {
            _activation = activation;
            _code = code;
            _location = location;
            _employee = employee;
            _hub = hub;
        }
        #endregion 

        [HttpGet]
        public ActionResult<IEnumerable<Activation>> Index()
        {
            //await _hub.Clients.All.SendAsync("Reports");
            return _activation.Entity.GetAll().ToList();
        }

        [HttpGet]
        [Route("/AllDetails")]
        public async Task<ActionResult<List<DashboardVM>>> AllDetails()
        {
            var activations = _activation.Entity.GetAll();
            DashboardVM vm;
            List<DashboardVM> vms = new List<DashboardVM>();

            foreach (var item in activations)
            {
                vm = new DashboardVM()
                {
                    Id = item.ActivationId,
                    CodeName = _code.Entity.GetById(item.CodeId).CodeName,
                    LocationName = _location.Entity.GetById(item.LocationId).LocationName,
                    EmployeeName = _employee.Entity.GetById(item.EmployeeId).FullName,
                    DateTime = item.ActivationTime,
                    Status = item.Status
                };
                vms.Add(vm);
            }
            await _hub.Clients.All.SendAsync("Reports");
            return vms;
        }

        //[HttpPost]
        //[Route("/PostActivation")]
        //public async Task<ActionResult> Post(Activation activation)
        //{
        //    _activation.Entity.Insert(activation);
        //    _activation.Save();
        //    await _hub.Clients.All.SendAsync("Reports");
        //    return Ok("Successfully Created");
        //}
    }
}
