using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Entities;
using CoreLibrary.Interfaces;
using HICSManager.ViewModel;
using InfrastructureLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HICSManager.Controllers
{
    /// <summary>
    /// TODO: 
    /// 1. List of Groups.
    /// 2. Add members to a group: 
    ///     a. Only add member who doesn't exist already in group.
    ///     b. Member can be added to many groups.
    ///     c. 
    /// 3. Associate Code with a group
    /// 4. 
    /// </summary>
    public class GroupMainController : Controller
    {
        private readonly DataContext _context;
        private readonly IUnitOfWork<Code> _code;
        private readonly IUnitOfWork<Group> _group;
        private readonly IUnitOfWork<Employee> _employee;
        private readonly IUnitOfWork<CodeGroup> _codeGroup;
        private readonly IUnitOfWork<Membership> _membership;


        public GroupMainController(DataContext context,
            IUnitOfWork<Code> code,
            IUnitOfWork<Group> group,
            IUnitOfWork<Employee> employee,
            IUnitOfWork<CodeGroup> codeGroup,
            IUnitOfWork<Membership> membership)
        {
            _group = group;
            _employee = employee;
            _context = context;
            _code = code;
            _codeGroup = codeGroup;
            _membership = membership;
        }
        // GET: GroupMain
        public ActionResult Index()
        {
            //var query = from photo in context.Set<PersonPhoto>()
            //            join person in context.Set<Person>()
            //                on photo.PersonPhotoId equals person.PhotoId
            //            select new { person, photo };


            //var emps = _employee.Entity.GetAll().ToList();
            //var grps = _group.Entity.GetAll().ToList();
            //var empgrps = _membership.Entity.GetAll().ToList();

            //var qry = from e in emps
            //          from g in grps
            //          join ge in empgrps on e.ID equals ge.EmployeeId
            //          join gr in empgrps on g.GroupId equals gr.GroupId;

            //GroupVM vm = new GroupVM()
            //{
            //    Members = qry.ToList()
            //};
            return View();
        }

        // GET: GroupMain/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GroupMain/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GroupMain/Create
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

        // GET: GroupMain/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GroupMain/Edit/5
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

        // GET: GroupMain/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GroupMain/Delete/5
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