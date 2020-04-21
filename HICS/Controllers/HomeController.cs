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
         * The goal is to insert into table activation a record. 
         * We should know who inserted this record. 
         * We should know when the record is inserted. 
         * We should play a sound : DONE.
         * We should send pager.
         * We should send notification.
         */


        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork<Activation> _activation;
        private readonly IUnitOfWork<Employee> _employee;
        private readonly IUnitOfWork<Code> _code;

        public HomeController(
            ILogger<HomeController> logger, 
            IUnitOfWork<Activation> activation, 
            IUnitOfWork<Employee> employee,
            IUnitOfWork<Code> code)
        {
            _logger = logger;
            _activation = activation;
            _employee = employee;
            _code = code;
        }

        public IActionResult Index()
        {
            return View(_code.Entity.GetAll());
        }

        public IActionResult Activation()
        {
            //Keep it clean, dry
            //Fisrt Priority to play the sound 
            PlaySound("BUT HERE IS THE PROBLEM", "WE CAN GET THIS FROM COOKIE");

            return View();
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
