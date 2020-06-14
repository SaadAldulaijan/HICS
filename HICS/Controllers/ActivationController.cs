using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Entities;
using CoreLibrary.Interfaces;
using HICS.ViewModels;
using Microsoft.AspNetCore.Mvc;

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

        private readonly IUnitOfWork<Activation> _activation;
        private readonly IUnitOfWork<Code> _code;
        private readonly IUnitOfWork<Location> _location;
        private readonly IUnitOfWork<Employee> _employee;

        public ActivationController(IUnitOfWork<Activation> activation,
            IUnitOfWork<Code> code,
            IUnitOfWork<Location> location,
            IUnitOfWork<Employee> employee)
        {
            _activation = activation;
            _code = code;
            _location = location;
            _employee = employee;
        }
        #endregion


        public IActionResult Index()
        {
            ActivationVM activationVM = new ActivationVM()
            {
                Codes = _code.Entity.GetAll().ToList()
            };
            return View(activationVM);
        }
        // needs refactor
        [HttpPost]
        public IActionResult Activate(ActivationVM activationVM)
        {
            // check if badge is correct.
            if (IsValidBadgeNo(activationVM.Employee.ID))
            {
                Activation activation = new Activation()
                {
                    CodeId = activationVM.Code.CodeId,
                    LocationId = Int32.Parse(Request.Cookies["LocationId"]), //Cookie Expiry should be considered
                    EmployeeId = activationVM.Employee.ID,
                    //status means code is activated or cleared
                    Status = true,
                    ActivationTime = DateTime.Now
                };

                //Sound is working !
                string codeName = _code.Entity.GetById(activation.CodeId).CodeName;
                string locationName = _location.Entity.GetById(activation.LocationId).LocationName;
                PlaySound(codeName, locationName);

                _activation.Entity.Insert(activation);
                _activation.Save();
            }
            else
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        
        #region Helper Methods
        public bool IsValidBadgeNo(int BadgeNo)
        {
            var result = _employee.Entity.GetById(BadgeNo).ID;

            if (result != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
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