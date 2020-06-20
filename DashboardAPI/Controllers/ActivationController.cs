using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Entities;
using CoreLibrary.Interfaces;
using InfrastructureLibrary.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace DashboardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivationController : ControllerBase
    {
        // for security reason, there will be no delete option. 
        // and update option should be in activation.status only for clear.
        private readonly IUnitOfWork<Activation> _activation;
        private readonly IHubContext<ReportingHub> _hub;

        public ActivationController(IUnitOfWork<Activation> activation,
                                    IHubContext<ReportingHub> hub)
        {
            _activation = activation;
            _hub = hub;
        }

        // I think i dont need this controller. 
        // we will decide when we refacotr HICS Manager.
        [HttpGet]
        public ActionResult<List<Activation>> GetAll()
        {
            var data = _activation.Entity.GetAll().ToList();
            return data;
        }

        public ActionResult<Activation> GetById(int id)
        {
            var activation = _activation.Entity.GetById(id);
            return activation;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Activation activation)
        {
            _activation.Entity.Insert(activation);
            _activation.Save();
            await _hub.Clients.All.SendAsync("Reports");
            return Ok("Successfully Created");
        }
    }
}
