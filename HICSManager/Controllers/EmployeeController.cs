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
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork<Employee> _employee;

        public EmployeeController(IUnitOfWork<Employee> employee)
        {
            _employee = employee;
        }

        // GET: Employee
        public IActionResult Index()
        {
            return View(_employee.Entity.GetAll());
        }

        // GET: Employee/Details/5
        public IActionResult Details(int id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            var employee = _employee.Entity.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ID,FirstName,LastName,MoblieNo,Email")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employee.Entity.Insert(employee);
                _employee.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employee/Edit/5
        public IActionResult Edit(int id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            var employee = _employee.Entity.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID,FirstName,LastName,MoblieNo,Email")] Employee employee)
        {
            if (id != employee.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _employee.Entity.Update(employee);
                    _employee.Save();
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

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var employees = await _context.Employee
            //    .FirstOrDefaultAsync(m => m.ID == id);
            var employee = _employee.Entity.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            //var employee = await _context.Employee.FindAsync(id);
            var employee = _employee.Entity.GetById(id);
            //_context.Employee.Remove(employee);
            _employee.Entity.Delete(employee.ID);
            _employee.Save();
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            //return _context.Employee.Any(e => e.ID == id);
            return _employee.Entity.GetAll().Any(e => e.ID == id);
        }
    }
}
