using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HICS.Models;
using CoreLibrary.Interfaces;
using CoreLibrary.Entities;

namespace HICS.Controllers
{
    public class HomeController : Controller
    {
        /*TODO:
         * The goal is to insert into table activation a record. : DONE.
         * We should know who inserted this record. : DONE.
         * We should know when the record is inserted : DONE.
         * We should play a sound : DONE.
         * We should send pager.
         * We should send notification.
         */


        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork<Activation> _activation;
        private readonly IUnitOfWork<Employee> _employee;
        private readonly IUnitOfWork<Code> _code;
        private readonly IUnitOfWork<Location> _location;

        public HomeController(
            ILogger<HomeController> logger, 
            IUnitOfWork<Activation> activation, 
            IUnitOfWork<Employee> employee,
            IUnitOfWork<Code> code,
            IUnitOfWork<Location> location)
        {
            _logger = logger;
            _activation = activation;
            _employee = employee;
            _code = code;
            _location = location;
        }

        public IActionResult Index()
        {
            return View(_code.Entity.GetAll());
        }

        [HttpPost]
        public IActionResult Activation(int badge)
        {
            if (ModelState.IsValid)
            {
                Activation activation = new Activation()
                {
                    CodeId = 1,
                    LocationId = 1,
                    ActivationTime = DateTime.Now,
                    Status = false
                };
            }

            //Keep it clean, dry
            //Fisrt Priority to play the sound 
            //PlaySound("BUT HERE IS THE PROBLEM", "WE CAN GET THIS FROM COOKIE");

            // How to get the location ID, and Code ID
            
            //_activation.Entity.Insert(activation);
            //_activation.Save();
            return View("Privacy");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region Helper Methods
        public void PlaySound(string codeName, string locationName)
        {
            string args = codeName + " " + locationName;
            string filePath = @"C:\temp\Projects\HICS\Extension\obj\Debug\Extension.exe";
            Process.Start(filePath, args);
        }
        #endregion
    }
}
