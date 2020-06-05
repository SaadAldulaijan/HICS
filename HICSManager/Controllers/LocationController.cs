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
    public class LocationController : Controller
    {
        #region Properties and Constructor
        private readonly IUnitOfWork<Location> _location;
        public LocationController(IUnitOfWork<Location> location)
        {
            _location = location;
        }
        #endregion

        #region Select
        // GET: Location
        public IActionResult Index()
        {
            return View(_location.Entity.GetAll());
        }

        // GET: Location/Details/5
        public IActionResult Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var location = _location.Entity.GetById(id);
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
        public IActionResult Create([Bind("LocationId,LocationName,AreaCode")] Location location)
        {
            if (ModelState.IsValid)
            {
                _location.Entity.Insert(location);
                _location.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(location);
        }
        #endregion

        #region Update
        // GET: Location/Edit/5
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var location = _location.Entity.GetById(id);
            if (location == null)
            {
                return NotFound();
            }
            return View(location);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("LocationId,LocationName,AreaCode")] Location location)
        {
            if (id != location.LocationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _location.Entity.Update(location);
                    _location.Save();
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
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }


            var location = _location.Entity.GetById(id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // POST: Location/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var location = _location.Entity.GetById(id);
            _location.Entity.Delete(id);
            _location.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool LocationExists(int id)
        {
            return _location.Entity.GetAll().Any(e => e.LocationId == id);
        }
        #endregion
    }
}
