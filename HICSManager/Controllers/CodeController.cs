using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreLibrary.Entities;
using InfrastructureLibrary;
using CoreLibrary.Interfaces;

namespace HICSManager.Controllers
{
    public class CodeController : Controller
    {
        #region Properties and Constructor
        private readonly IUnitOfWork<Code> _code;

        public CodeController(IUnitOfWork<Code> code)
        {
            _code = code;
        }
        #endregion

        #region Select
        // GET: Code
        public IActionResult Index()
        {
            return View(_code.Entity.GetAll());
        }

        // GET: Code/Details/5
        public IActionResult Details(int id)
        {
            var code = _code.Entity.GetById(id);
            if (code == null)
            {
                return NotFound();
            }

            return View(code);
        }
        #endregion

        #region Insert
        // GET: Code/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Code/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CodeId,CodeName,CodeColor,PagerNo")] Code code)
        {
            if (ModelState.IsValid)
            {
                _code.Entity.Insert(code);
                _code.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(code);
        }
        #endregion

        #region Update
        // GET: Code/Edit/5
        public IActionResult Edit(int id)
        {
            var code = _code.Entity.GetById(id);
            if (code == null)
            {
                return NotFound();
            }
            return View(code);
        }

        // POST: Code/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("CodeId,CodeName,CodeColor,PagerNo")] Code code)
        {
            if (id != code.CodeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _code.Entity.Update(code);
                    _code.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CodeExists(code.CodeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(code);
        }
        #endregion

        #region Delete
        // GET: Code/Delete/5
        public IActionResult Delete(int id)
        {
            var code = _code.Entity.GetById(id);
            if (code == null)
            {
                return NotFound();
            }

            return View(code);
        }

        // POST: Code/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var code = _code.Entity.GetById(id);
            _code.Entity.Delete(code.CodeId);
            _code.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool CodeExists(int id)
        {
            return _code.Entity.GetAll().Any(e => e.CodeId == id);
        }
        #endregion
    }
}
