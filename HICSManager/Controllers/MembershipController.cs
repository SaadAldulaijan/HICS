using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Entities;
using CoreLibrary.Interfaces;
using HICSManager.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HICSManager.Controllers
{
    public class MembershipController : Controller
    {
        #region Properties and Constructor
        private readonly IUnitOfWork<Membership> _membership;
        private readonly IUnitOfWork<Employee> _employee;
        private readonly IUnitOfWork<Group> _group;

        public MembershipController(IUnitOfWork<Membership> membership,
                                    IUnitOfWork<Employee> employee,
                                    IUnitOfWork<Group> group)
        {
            _membership = membership;
            _employee = employee;
            _group = group;
        }

        #endregion


        // GET: Membership
        // id = GroupId received from Group controller, go to Views => Index => last line add members hyperlink
        public ActionResult Index(int id)
        {
            if (id == 0)
            {
                id = 1;
            }
            var selectedGroup = _group.Entity.GetById(id);

            var members = from m in _membership.Entity.GetAll()
                          join e in _employee.Entity.GetAll() on m.EmployeeId equals e.ID
                          join g in _group.Entity.GetAll() on m.GroupId equals g.GroupId
                          where g.GroupId == selectedGroup.GroupId
                          select e;

            var notMembers = GetEmployeeNotInGroup(id);

            GroupEmpVM vm = new GroupEmpVM()
            {
                Members = members.ToList(),
                NotMembers = notMembers,
                Group = selectedGroup
            };

            return View(vm);
        }

        public List<Employee> GetEmployeeNotInGroup(int groupId)
        {
            var userNotInGroup = (from e in _employee.Entity.GetAll()
                                  where !(from g in _membership.Entity.GetAll()
                                          where g.GroupId == groupId
                                          select g.EmployeeId)
                                  .Contains(e.ID)
                                  select e)
                                 .ToList();



            return userNotInGroup;
        }

        [HttpPost]
        public IActionResult AddMember(Employee employee)
        {
            // get selected employee from the table


            // add selected employee as a member of the group.
            _employee.Entity.Insert(employee);


            return RedirectToAction(nameof(Index));
        }

        // GET: Membership/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Membership/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Membership/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Membership/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Membership/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Membership/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Membership/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}