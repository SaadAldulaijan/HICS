using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using HICS.Library.Models;
using HICS.Library.Services;
using HICS.ViewModels;

namespace HICS.Controllers
{
    public class ActivationController : Controller
    {

        /*TODO:
* The goal is to insert into table activation a record. : DONE.
* We should know who inserted this record. : DONE.
* We should know when the record is inserted : DONE.
* We should play a sound : DONE.
* We should send record to Dashboard.
* We should send pager.
* We should send notification.
*/

        #region Properties and Constructor
        private readonly IActivationService _activation;
        private readonly ICodeService _code;
        private readonly ILocationService _location;
        private readonly IEmployeeService _employee;

        public ActivationController(IActivationService activation,
                                    ICodeService code,
                                    ILocationService location,
                                    IEmployeeService employee)
        {
            _activation = activation;
            _code = code;
            _location = location;
            _employee = employee;
        }
        #endregion

        public async Task<IActionResult> Index()
        {
            ActivationVM activationVM = new ActivationVM()
            {
                Codes = await _code.Get()
            };
            return View(activationVM);
        }

        [HttpPost]
        public async Task<IActionResult> Activate(ActivationVM activationVM)
        {
            if (activationVM.Employee.ID != 0)
            {
                Activation activation = new Activation()
                {
                    CodeId = activationVM.Code.CodeId,
                    LocationId = Int32.Parse(Request.Cookies["LocationId"]), //Cookie Expiry should be considered
                    EmployeeId = activationVM.Employee.ID,
                    Status = true,
                    ActivationTime = DateTime.Now
                };
                // use try catch
                await _activation.Post(activation);

                // Sound: 
                var code = await _code.GetById(activation.CodeId);
                var location = await _location.GetById(activation.LocationId);
                PlaySound(code.CodeName, location.LocationName);

                // Send Notification

                // Send Pager

            }
            else return NotFound();
            return RedirectToAction(nameof(Index));
        }

        // OLD CODE
        //[HttpPost]
        //public async Task<IActionResult> Activate(ActivationVM activationVM)
        //{
        //    // check if badge is correct.
        //    if (IsValidBadgeNo(activationVM.Employee.ID))
        //    {
        //        Activation activation = new Activation()
        //        {
        //            CodeId = activationVM.Code.CodeId,
        //            LocationId = Int32.Parse(Request.Cookies["LocationId"]), //Cookie Expiry should be considered
        //            EmployeeId = activationVM.Employee.ID,
        //            //status means code is activated or cleared
        //            Status = true,
        //            ActivationTime = DateTime.Now
        //        };

        //        //Sound is working !
        //        string codeName = _code.Entity.GetById(activation.CodeId).CodeName;
        //        string locationName = _location.Entity.GetById(activation.LocationId).LocationName;
        //        PlaySound(codeName, locationName);

        //        
        //        //Insert and Save to DB
        //        //_activation.Entity.Insert(activation);
        //        //_activation.Save();

        //        // SignalR ERROR HERE
        //        //HubConnection hub = new HubConnectionBuilder()
        //        //    .WithUrl("https://localhost:44366/reportinghub")
        //        //    .Build();
        //        //await hub.StartAsync();
        //        //await hub.SendAsync("Reports");
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //    return RedirectToAction(nameof(Index));
        //}


        #region Helper Methods
        public bool IsValidBadgeNo(int badgeNo)
        {
            // TODO: this method should be changed when going to production 
            var result = _employee.GetById(badgeNo).Id;
            if (result != 0) { return true; }
            else return false;
        }
        

        public void PlaySound(string codeName, string locationName)
        {
            string args = codeName + " " + locationName;
            string filePath = @"C:\temp\Projects\HICS\Extension\obj\Debug\Extension.exe";
            Process.Start(filePath, args);
        }
        #endregion
    }
}