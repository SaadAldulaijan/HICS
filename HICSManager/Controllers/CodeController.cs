using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HICS.Library.Models;
using HICS.Library.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HICSManager.Controllers
{
    public class CodeController : Controller
    {
        #region Properties and Constructor
        private readonly ICodeService _code;

        public CodeController(ICodeService code)
        {
            _code = code;
        }
        #endregion

        #region Select
        // GET: Code
        public async Task<IActionResult> Index()
        {
            var data = await _code.Get();
            return View(data);
        }

        // GET: Code/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var code = await _code.GetById(id);
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
        public async Task<IActionResult> Create(Code code)
        {
            if (ModelState.IsValid)
            {
                await _code.Post(code);
                return RedirectToAction(nameof(Index));
            }
            return View(code);
        }
        #endregion

        #region Update
        // GET: Code/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var code = await _code.GetById(id);
            if (code == null)
            {
                return NotFound();
            }
            return View(code);
        }

        // POST: Code/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Code code)
        {
            if (id != code.CodeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _code.Update(id,code);
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
        public async Task<IActionResult> Delete(int id)
        {
            var code = await _code.GetById(id);
            if (code == null)
            {
                return NotFound();
            }

            return View(code);
        }

        // POST: Code/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var code = await _code.GetById(id);
            await _code.Delete(code.CodeId);
            return RedirectToAction(nameof(Index));
        }

        private bool CodeExists(int id)
        {
            List<Code> codes = new List<Code>();
            Task.Run(async () =>
            {
                codes = await _code.Get();
            });
            return codes.Any(e => e.CodeId == id);
        }
        #endregion
    }
}
