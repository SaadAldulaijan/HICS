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
    public class GroupController : Controller
    {
        #region Properties and Constructor
        private readonly IGroupService _group;
        public GroupController(IGroupService group)
        {
            _group = group;
        }
        #endregion

        #region Select
        // GET: Groups
        public async Task<IActionResult> Index()
        {
            var data = await _group.Get();
            return View(data);
        }

        // GET: Groups/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var group =await  _group.GetById(id);


            if (group == null)
            {
                return NotFound();
            }

            return View(group);
        }

        #endregion

        #region Insert
        // GET: Groups/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Group group)
        {
            if (ModelState.IsValid)
            {
                await _group.Post(group);
                return RedirectToAction(nameof(Index));
            }
            return View(group);
        }
        #endregion

        #region Update
        // GET: Groups/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var group = await _group.GetById(id);
            if (group == null)
            {
                return NotFound();
            }
            return View(group);
        }

        // POST: Group/Edit/group
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Group group)
        {
            if (id != group.GroupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _group.Update(id,group);
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
        #endregion

        #region Delete

        // GET: Groups/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var group = await _group.GetById(id);
            if (group == null)
            {
                return NotFound();
            }

            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var group = await _group.GetById(id);
            await _group.Delete(group.GroupId);
            return RedirectToAction(nameof(Index));
        }

        private bool GroupExists(int id)
        {

            List<Group> groups = new List<Group>();
            Task.Run(async () =>
            {
                groups = await _group.Get();
            });

            return groups.Any(e => e.GroupId == id);
        }
        #endregion
    }
}
