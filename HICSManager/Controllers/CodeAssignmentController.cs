using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using HICS.Library.Models;
using HICS.Library.Services;
using HICSManager.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;

namespace HICSManager.Controllers
{
    public class CodeAssignmentController : Controller
    {
        #region Properties and Constructor
        private readonly ICodeGroupService _codeGroup;
        private readonly ICodeService _code;
        private readonly IGroupService _group;
        public CodeAssignmentController(ICodeGroupService codeGroup,
                                        ICodeService code,
                                        IGroupService group)
        {
            _codeGroup = codeGroup;
            _code = code;
            _group = group;
        }
        #endregion

        // the goal here is to assign code with a specific group 
        // CodeGroup table has the following attributes: 
        // CodeId, GroupId both as a foregin keys
        public async Task<IActionResult> Index(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var selectedCode = await _code.GetById(id);
            var assignedGroups = GetAssignedGroups(id);
            var unassignedGroups = GetUnassignedGroups(id);

            CodeGroupVM vm = new CodeGroupVM()
            {
                Code = selectedCode,
                AssignedGroups = await assignedGroups,
                UnassignedGroups = await unassignedGroups
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AssignGroups(IEnumerable<int> groupIds, int codeId)
        {
            if (groupIds.Count() != 0 && codeId != 0)
            {
                foreach (var item in groupIds)
                {
                    await _codeGroup.Post(new CodeGroup()
                    {
                        CodeId = codeId,
                        GroupId = item
                    });
                }
            }
            else
            {
                return BadRequest();
            }
            return RedirectToAction("Index", new { id = codeId });
        }


        [HttpPost]
        public async Task <IActionResult> UnassignGroup(int groupId, int codeId)
        {
            if (groupId != 0 && codeId != 0)
            {
                await _codeGroup.DeleteComposite(groupId, codeId);
            }
            else
            {
                return BadRequest();
            }
            return RedirectToAction("Index", new { id = codeId });
        }


        #region Helper Methods

        public async Task<List<Group>> GetAssignedGroups(int codeId)
        {
            var assignedGroups = (from cg in await _codeGroup.Get()
                                  join c in await _code.Get() on cg.CodeId equals c.CodeId
                                  join g in await _group.Get() on cg.GroupId equals g.GroupId
                                  where c.CodeId == codeId
                                  select g).ToList();
            return assignedGroups;
        }

        public async Task<List<Group>> GetUnassignedGroups(int codeId)
        {
            //groups are not assigned to any code
            var codeGroups = await _codeGroup.Get();
            var unassignedGroups = (from g in await _group.Get()
                                    where !(from cg in codeGroups
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