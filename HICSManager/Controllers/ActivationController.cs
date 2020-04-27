using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Entities;
using CoreLibrary.Interfaces;
using HICSManager.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HICSManager.Controllers
{
    public class ActivationController : Controller
    {
        private readonly IUnitOfWork<Code> _code;
        private readonly IUnitOfWork<Activation> _activation;

        public ActivationController(IUnitOfWork<Code> code, IUnitOfWork<Activation> activation)
        {
            _code = code;
            _activation = activation;
        }
        public IActionResult Index()
        {
            return View(_code.Entity.GetAll());
        }
        [HttpPost]
        public IActionResult Activate(Code code)
        {

            int codeId = code.CodeId;
            return RedirectToAction("Index");
        }
    }
}