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

        public ActivationController(IUnitOfWork<Activation> activation , 
            IUnitOfWork<Code> code, 
            IUnitOfWork<Location> location)
        {
            _activation = activation;
            _code = code;
            _location = location;
        }
        public IActionResult Index()
        {
            ActivationVM activationVM = new ActivationVM()
            {
                Codes = _code.Entity.GetAll().ToList()
            };
            return View(activationVM);
        }
        
        public IActionResult Activate()
        {

            return RedirectToAction(nameof(Index));
        }
    }
}