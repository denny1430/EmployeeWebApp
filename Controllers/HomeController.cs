using Employewebapp.Filters;
using Employewebapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using X.PagedList;
using X.PagedList.Extensions;
using Employewebapp.Filters;



namespace Employewebapp.Controllers
{
    [ServiceFilter(typeof(LogActionFilter))]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Empdbcontext _context;
        public HomeController(ILogger<HomeController> logger, Empdbcontext context)
        {
            _logger = logger;
            _context = context;
        }
        [ServiceFilter(typeof(LogExceptionFilter))]
        public IActionResult Crash()
        {
            var employees = _context.Employees
    .OrderBy(e => e.Id)
    .ToList();

            if (employees.Count < 5)
            {
                var exceptionLog = new ExceptionLog
                {
                    ActionName= $"There are only {employees.Count} employees in the database.",
                    LogTime = DateTime.Now,
                    UserName = User.Identity?.Name ?? "System",
                    Severity = "Info",
                    StackTrace = null // Optional, only if your ExceptionLog model has it
                };

                _context.ExceptionLogs.Add(exceptionLog);
                _context.SaveChanges();

                // Optional: also write to console or file
                Console.WriteLine($"[{exceptionLog.LogTime}] {exceptionLog.UserName} - {exceptionLog.Severity}");
            }

            return View(employees);
        }

        //    try
        //    {
        //        // Force an exception
        //        throw new Exception("Test exception logging");
        //    }
        //    catch (Exception ex)
        //    {
        //        var log = new ExceptionLog
        //        {
        //            ControllerName = nameof(HomeController),
        //            ActionName = nameof(Crash),
        //            ExceptionMessage = ex.Message,
        //            StackTrace = ex.StackTrace,
        //            LogTime = DateTime.Now,
        //            UserName = User.Identity?.Name ?? "System",
        //            Severity = "Error",
        //            Remarks = $"An error occurred in {nameof(Crash)}: {ex.Message}"
        //        };

            //        _context.ExceptionLogs.Add(log);
            //        _context.SaveChanges();

            //        // Console log for debugging (optional)
            //        Console.WriteLine($"[{log.LogTime}] {log.UserName} - {log.Remarks}");
            //    }

            //    return View("Error");
            //}



        public IActionResult About()
        {
            return View();
        }

        public IActionResult Index()
        {

            var employees = _context.Employees
        .OrderBy(e => e.Id)
        .ToList();
            if (employees.Count < 5)
            {
                var log = new Logviewmodel
                {
                    Remarks = $"There are {employees.Count} employees in the database.",
                    TimeStamp = DateTime.Now,
                    UserName = User.Identity?.Name ?? "System",
                    Severity = "Info"
                };
                _context.Logviewmodel.Add(log);
                _context.SaveChanges();

                // Store in memory, DB, or write to console
                Console.WriteLine($"[{log.TimeStamp}] {log.UserName} - {log.Remarks}");
            }

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
    

