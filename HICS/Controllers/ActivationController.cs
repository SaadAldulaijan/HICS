using System;
using System.Collections.Generic;
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
        private readonly IUnitOfWork<Activation> _activation;
        private readonly IUnitOfWork<Code> _code;
        private readonly IUnitOfWork<Location> _location;
        private readonly IUnitOfWork<Employee> _employee;

        public ActivationController(IUnitOfWork<Activation> activation , 
            IUnitOfWork<Code> code, 
            IUnitOfWork<Location> location,
            IUnitOfWork<Employee> employee)
        {
            _activation = activation;
            _code = code;
            _location = location;
            _employee = employee;
        }
        public IActionResult Index()
        {
            ActivationVM activationVM = new ActivationVM()
            {
                Codes = _code.Entity.GetAll().ToList()
            };
            return View(activationVM);
        }
        
        public IActionResult Activate(int badgeNo, string codeName, string locationName)
        {
            if (IsValidBadgeNo(badgeNo))
            {

            }
            return RedirectToAction(nameof(Index));
        }

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
    }
}