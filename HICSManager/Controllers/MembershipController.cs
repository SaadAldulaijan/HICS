using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HICS.Library.Models;
using HICS.Library.Services;
using HICSManager.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HICSManager.Controllers
{
    // please document this controller so you cannot forget. 
    public class MembershipController : Controller
    {
        #region Properties and Constructor
        private readonly IMembershipService _membership;
        private readonly IEmployeeService _employee;
        private readonly IGroupService _group;
        
        public MembershipController(IMembershipService membership,
                                    IEmployeeService employee,
                                    IGroupService group)
        {
            _membership = membership;
            _employee = employee;
            _group = group;
        }
        #endregion


        // GET: Membership
        // id = GroupId received from Group controller, go to Views => Index => last line add members hyperlink
        // Done
        public async Task<ActionResult> Index(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var selectedGroup = await _group.GetById(id);
            var members = GetMembers(id);
            var notMembers = GetEmployeesNotInGroup(id);

            GroupEmpVM vm = new GroupEmpVM()
            {
                Members = await members,
                NotMembers = await notMembers,
                Group = selectedGroup
            };

            return View(vm);
        }


        // Reached here, i cannot pass employee to this action.
        // Maybe i need javascript to do this task.
        // Done without Javascript
        // try using linq instead of foreach and if. 

        [HttpPost]
        public async Task<IActionResult> AddMembers(IEnumerable<int> employeeIds, int groupId)
        {
            //Validation
            if (employeeIds.Count() != 0 && groupId != 0)
            {
                // get selected employeeIds from the table
                // add selected employees as a member of the group.
                // Insert into Membership table employeeId, groupId

                foreach (var employeeId in employeeIds)
                {
                    await _membership.Post(new Membership()
                    {
                        EmployeeId = employeeId,
                        GroupId = groupId,
                        AssignDate = DateTime.Now
                    });
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
        public async Task<IActionResult> RemoveMember(int employeeId, int groupId)
        {
            if (employeeId != 0 && groupId != 0)
            {
                await _membership.DeleteComposite(employeeId, groupId);
            }
            else
            {
                return BadRequest();
            }
            return RedirectToAction("Index", new { id = groupId });
        }

        #region Helper Methods
        public async Task<List<Employee>> GetMembers(int groupId)
        {
            var members = (from m in await _membership.Get()
                           join e in await _employee.Get() on m.EmployeeId equals e.ID
                           join g in await _group.Get() on m.GroupId equals g.GroupId
                           where g.GroupId == groupId
                           select e).ToList();
            return members;

        }
        public async Task<List<Employee>> GetEmployeesNotInGroup(int groupId)
        {
            var memberships = await _membership.Get();

            var userNotInGroup = (from e in await _employee.Get()
                                  where !(from g in memberships
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