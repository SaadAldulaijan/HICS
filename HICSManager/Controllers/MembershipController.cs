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
    // please document this controller so you cannot forget. 
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
        // Done
        public ActionResult Index(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var selectedGroup = _group.Entity.GetById(id);
            var members = GetMembers(id);
            var notMembers = GetEmployeesNotInGroup(id);

            GroupEmpVM vm = new GroupEmpVM()
            {
                Members = members.ToList(),
                NotMembers = notMembers,
                Group = selectedGroup
            };

            return View(vm);
        }


        // Reached here, i cannot pass employee to this action.
        // Maybe i need javascript to do this task.
        // Done without Javascript
        // try using linq instead of foreach and if. 

        [HttpPost]
        public IActionResult AddMembers(IEnumerable<int> employeeIds, int groupId)
        {
            //Validation
            if (employeeIds.Count() != 0 && groupId != 0)
            {
                // get selected employeeIds from the table
                // add selected employees as a member of the group.
                // Insert into Membership table employeeId, groupId

                foreach (var employeeId in employeeIds)
                {
                    _membership.Entity.Insert(new Membership()
                    {
                        EmployeeId = employeeId,
                        GroupId = groupId,
                        AssignDate = DateTime.Now
                    });
                    _membership.Save();
                }
            }
            else
            {
                return BadRequest();
            }
            return RedirectToAction("Index", new { id = groupId });
        }


        // add method in repo to delete composite keys. DONE
        // JS to confirm delete. DONE
        // Working Fine. 
        [HttpPost]
        public IActionResult RemoveMember(int employeeId, int groupId)
        {
            if (employeeId != 0 && groupId != 0)
            {
                _membership.Entity.DeleteComposite(employeeId, groupId);
                _membership.Save();
            }
            else
            {
                return BadRequest();
            }
            return RedirectToAction("Index", new { id = groupId });
        }

        #region Helper Methods
        public List<Employee> GetMembers(int groupId)
        {
            var members = (from m in _membership.Entity.GetAll()
                           join e in _employee.Entity.GetAll() on m.EmployeeId equals e.ID
                           join g in _group.Entity.GetAll() on m.GroupId equals g.GroupId
                           where g.GroupId == groupId
                           select e).ToList();
            return members;

        }
        public List<Employee> GetEmployeesNotInGroup(int groupId)
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
        #endregion

    }
}