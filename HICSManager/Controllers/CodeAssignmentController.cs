using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using CoreLibrary.Entities;
using CoreLibrary.Interfaces;
using HICSManager.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HICSManager.Controllers
{
    public class CodeAssignmentController : Controller
    {
        #region Properties and Constructor
        private readonly IUnitOfWork<CodeGroup> _codeGroup;
        private readonly IUnitOfWork<Code> _code;
        private readonly IUnitOfWork<Group> _group;

        public CodeAssignmentController(IUnitOfWork<CodeGroup> codeGroup,
                                        IUnitOfWork<Code> code,
                                        IUnitOfWork<Group> group)
        {
            _codeGroup = codeGroup;
            _code = code;
            _group = group;
        }
        #endregion

        // the goal here is to assign code with a specific group 
        // CodeGroup table has the following attributes: 
        // CodeId, GroupId both as a foregin keys
        public IActionResult Index(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var selectedCode = _code.Entity.GetById(id);
            var assignedGroups = GetAssignedGroups(id);
            var unassignedGroups = GetUnassignedGroups(id);

            CodeGroupVM vm = new CodeGroupVM()
            {
                Code = selectedCode,
                AssignedGroups = assignedGroups,
                UnassignedGroups = unassignedGroups
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult AssignGroups(IEnumerable<int> groupIds, int codeId)
        {
            if (groupIds.Count() != 0 && codeId != 0)
            {
                foreach (var item in groupIds)
                {
                    _codeGroup.Entity.Insert(new CodeGroup()
                    {
                        CodeId = codeId,
                        GroupId = item
                    });
                    _codeGroup.Save();
                }
            }
            else
            {
                return BadRequest();
            }
            return RedirectToAction("Index", new { id = codeId });
        }


        [HttpPost]
        public IActionResult UnassignGroup(int groupId, int codeId)
        {
            if (groupId != 0 && codeId != 0)
            {
                _codeGroup.Entity.DeleteComposite(groupId, codeId);
                _codeGroup.Save();
            }
            else
            {
                return BadRequest();
            }
            return RedirectToAction("Index", new { id = codeId });
        }


        #region Helper Methods

        public List<Group> GetAssignedGroups(int codeId)
        {
            var assignedGroups = (from cg in _codeGroup.Entity.GetAll()
                                  join c in _code.Entity.GetAll() on cg.CodeId equals c.CodeId
                                  join g in _group.Entity.GetAll() on cg.GroupId equals g.GroupId
                                  where c.CodeId == codeId
                                  select g).ToList();
            return assignedGroups;
        }

        public List<Group> GetUnassignedGroups(int codeId)
        {
            //groups are not assigned to any code
            var unassignedGroups = (from g in _group.Entity.GetAll()
                                    where !(from cg in _codeGroup.Entity.GetAll()
                                            where cg.CodeId == codeId
                                            select cg.GroupId)
                                    .Contains(g.GroupId)
                                    select g)
                                    .ToList();
            return unassignedGroups;
        }



        #endregion
    }
}