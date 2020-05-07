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
    public class GroupController : Controller
    {
        private readonly IUnitOfWork<Group> _group;

        public GroupController(IUnitOfWork<Group> group)
        {
            _group = group;
        }

        // GET: Groups
        public IActionResult Index()
        {
            return View(_group.Entity.GetAll());
        }

        // GET: Groups/Details/5
        public IActionResult Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            //var group = await _context.Group
            //    .FirstOrDefaultAsync(m => m.GroupId == id);

            var group = _group.Entity.GetById(id);


            if (group == null)
            {
                return NotFound();
            }

            return View(group);
        }

        // GET: Groups/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GroupId,Name,Type,Description")] Group group)
        {
            if (ModelState.IsValid)
            {
                _group.Entity.Insert(group);
                _group.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(group);
        }

        // GET: Groups/Edit/5
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var group = _group.Entity.GetById(id);
            if (group == null)
            {
                return NotFound();
            }
            return View(group);
        }

        // POST: Group/Edit/group
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("GroupId,Name,Type,Description")] Group group)
        {
            if (id != group.GroupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _group.Entity.Update(group);
                    _group.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupExists(group.GroupId))
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
            return View(group);
        }

        // GET: Groups/Delete/5
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var group = _group.Entity.GetById(id);
            if (group == null)
            {
                return NotFound();
            }

            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var group = _group.Entity.GetById(id);
            _group.Entity.Delete(group.GroupId);
            _group.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupExists(int id)
        {
            return _group.Entity.GetAll().Any(e => e.GroupId == id);
        }
    }
}
