using Microsoft.AspNetCore.Mvc;
using Employewebapp.Models;
using System.Linq;
using System.Collections.Generic;

namespace Employewebapp.Controllers
{
    public class UserController : Controller
    {
        private readonly Empdbcontext _context;

        public UserController(Empdbcontext context)
        {
            _context = context;
        }

        // GET: /User/Userlist
        public IActionResult Userlist()
        {
            var users = _context.Users.ToList();
            return View("/Views/UserManagement/Userlist.cshtml", users);
        }

        // GET: /User/ManageRole/5
        [HttpGet]
        public IActionResult ManageRole(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();

            var roles = _context.Roles.ToList();
            var assignedRoleIds = _context.UserRoles
                                          .Where(ur => ur.UserId == id)
                                          .Select(ur => ur.RoleId)
                                          .ToList();

            ViewBag.User = user;
            ViewBag.AllRoles = roles;
            ViewBag.AssignedRoleIds = assignedRoleIds;

            return View("/Views/UserManagement/ManageRole.cshtml");
        }

        // POST: /User/ManageRole
        [HttpPost]
        public IActionResult ManageRole(int userId, List<int> selectedRoles)
        {
            var existingRoles = _context.UserRoles.Where(ur => ur.UserId == userId);
            _context.UserRoles.RemoveRange(existingRoles);

            if (selectedRoles != null)
            {
                foreach (var roleId in selectedRoles)
                {
                    _context.UserRoles.Add(new UserRole
                    {
                        UserId = userId,
                        RoleId = roleId
                    });
                }
            }

            _context.SaveChanges();
            return RedirectToAction("Userlist");
        }
    }
}
