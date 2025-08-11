using Employewebapp.Models;
using X.PagedList;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using X.PagedList.Extensions;



namespace Employewebapp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Empdbcontext _context;
        public HomeController(ILogger<HomeController> logger, Empdbcontext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Index()
        {
            var employees = _context.Employees
        .OrderBy(e => e.Id)
        .ToList();

            return View(employees);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult CreateEmployee(int? id)
        {
            if (id == null || id == 0)
            {
                // Return empty form for create
                return View(new CreateEmployee());
            }

            // Load employee for editing
            var emp = _context.Employees.FirstOrDefault(e => e.Id == id);
            if (emp == null)
            {
                return NotFound();
            }

            return View(emp);
        }
    

        public IActionResult DeleteEmployee(int? Id)
        {
            var empobject=_context.Employees.FirstOrDefault(x => x.Id == Id);
            _context.Employees.Remove(empobject);
            _context.SaveChanges();
            return RedirectToAction("Employe");
        }


        [HttpPost]
        [HttpPost]
        public IActionResult CreateEmployeeForm(CreateEmployee emp)
        {
            if (ModelState.IsValid)
            {
                var existingEmp = _context.Employees.FirstOrDefault(x => x.Id == emp.Id);

                if (existingEmp == null)
                {
                    // Add new
                    _context.Employees.Add(emp);
                }
                else
                {
                    // Update existing
                    existingEmp.Name = emp.Name;
                    existingEmp.Phone = emp.Phone;
                    existingEmp.Salary = emp.Salary;
                    existingEmp.Description = emp.Description;

                    _context.Employees.Update(existingEmp);
                }

                _context.SaveChanges();
                return RedirectToAction("Employe");
            }

            return View("CreateEmployee", emp);
        }

        public async Task<IActionResult> Employe(string searchString, int page = 1)
        {
            int pageSize = 2;

            var employeesQuery = _context.Employees.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                employeesQuery = employeesQuery
                    .Where(e => e.Name.Contains(searchString));
            }

            var pagedList = employeesQuery
             .OrderBy(e => e.Id)
              .ToPagedList(page, pageSize);

            return View(pagedList);
        }




    }
}
    

