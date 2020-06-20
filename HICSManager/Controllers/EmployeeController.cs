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
    public class EmployeeController : Controller
    {
        #region Properties and Constructor
        private readonly IEmployeeService _employee;

        public EmployeeController(IEmployeeService employee)
        {
            _employee = employee;
        }

        #endregion

        #region Select
        // GET: Employee
        public async Task<IActionResult> Index()
        {
            var data = await _employee.Get();
            return View(data);
        }

        // GET: Employee/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var employee = await _employee.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }
        #endregion

        #region Insert
        // GET: Employee/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                await _employee.Post(employee);
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }
        #endregion

        #region Update
        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employee.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (id != employee.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _employee.Update(id, employee);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.ID))
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
            return View(employee);
        }
        #endregion

        #region Delete
        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employee.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _employee.GetById(id);
            await _employee.Delete(employee.ID);
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            List<Employee> employees = new List<Employee>();
            Task.Run(async () =>
            {
                employees = await _employee.Get();
            });
            return employees.Any(e => e.ID == id);
        }

        #endregion
    }
}
