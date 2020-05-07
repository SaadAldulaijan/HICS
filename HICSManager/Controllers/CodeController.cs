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
    /// <summary>
    /// TODO: 
    /// 1. list of codes: name, color, pagerNo
    /// 2. On Edit 
    /// </summary>
    public class CodeController : Controller
    {
        private readonly IUnitOfWork<Code> _code;

        public CodeController(IUnitOfWork<Code> code)
        {
            _code = code;
        }

        // GET: Code
        public IActionResult Index()
        {
            return View(_code.Entity.GetAll());
        }

        // GET: Code/Details/5
        public IActionResult Details(int id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var codes = await _context.Code
            //    .FirstOrDefaultAsync(m => m.CodeId == id);

            var code = _code.Entity.GetById(id);
            if (code == null)
            {
                return NotFound();
            }

            return View(code);
        }

        // GET: Code/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Code/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Code/Edit/5
        public IActionResult Edit(int id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var code = await _context.Code.FindAsync(id);
            var code = _code.Entity.GetById(id);
            if (code == null)
            {
                return NotFound();
            }
            return View(code);
        }

        // POST: Code/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    //_context.Update(code);
                    //await _context.SaveChangesAsync();
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

        // GET: Code/Delete/5
        public IActionResult Delete(int id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var code = await _context.Code
            //    .FirstOrDefaultAsync(m => m.CodeId == id);
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
            //var code = await _context.Code.FindAsync(id);
            //_context.Code.Remove(code);
            //await _context.SaveChangesAsync();

            var code = _code.Entity.GetById(id);
            _code.Entity.Delete(code.CodeId);
            _code.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool CodeExists(int id)
        {
            //return _context.Code.Any(e => e.CodeId == id);
            return _code.Entity.GetAll().Any(e => e.CodeId == id);
        }
    }
}
