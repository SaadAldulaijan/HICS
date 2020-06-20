using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using HICS.Library.Services;
using HICS.Library.Models;
using Microsoft.EntityFrameworkCore;

namespace HICSManager.Controllers
{
    // Dependent on API : Done
    public class LocationController : Controller
    {
        #region Properties and Constructor
        private readonly ILocationService _location;
        public LocationController(ILocationService location)
        {
            _location = location;
        }
        #endregion

        #region Select
        // GET: Location
        public async Task<IActionResult> Index()
        {
            var data = await _location.Get();
            return View(data);
        }

        // GET: Location/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var location = await _location.GetById(id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }
        #endregion

        #region Insert
        // GET: Location/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Location location)
        {
            if (ModelState.IsValid)
            {
                await _location.Post(location);
                return RedirectToAction(nameof(Index));
            }
            return View(location);
        }
        #endregion

        #region Update
        // GET: Location/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var location = await _location.GetById(id);
            if (location == null)
            {
                return NotFound();
            }
            return View(location);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Location location)
        {
            if (id != location.LocationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _location.Update(id, location);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationExists(location.LocationId))
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
            return View(location);
        }
        #endregion

        #region Delete
        // GET: Location/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var location = await _location.GetById(id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // POST: Location/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var location = await _location.GetById(id);
            await _location.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool LocationExists(int id)
        {
            List<Location> location = new List<Location>();
            Task.Run(async () =>
            {
                location = await _location.Get();
            });
            
            return location.Any(e => e.LocationId == id);
        }
        #endregion
    }
}
