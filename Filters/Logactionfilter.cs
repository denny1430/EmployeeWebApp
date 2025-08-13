using Employewebapp.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
namespace Employewebapp.Filters
{
    public class LogActionFilter:IActionFilter
    {
        private readonly ILogger<LogActionFilter> _logger;
        private readonly Empdbcontext _context;
        public LogActionFilter(ILogger<LogActionFilter> logger, Empdbcontext context)
        {
            _logger = logger;
            _context = context;

        }

        // Before the action executes
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var actionName = context.ActionDescriptor.DisplayName;
            var parameters = context.ActionArguments;

            _logger.LogInformation("Executing action: {ActionName} at {Time}", actionName, DateTime.UtcNow);

            foreach (var param in parameters)
            {
                _logger.LogInformation("Parameter: {Key} = {Value}", param.Key, param.Value);
            }

        }

        // After the action executes
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var actionName = context.ActionDescriptor.DisplayName;

            var logEntry = new Logviewmodel
            {
                Remarks = $"Executed action: {actionName}",
                TimeStamp = DateTime.Now,
                UserName = context.HttpContext.User.Identity?.Name ?? "Anonymous",
                Severity = context.Exception == null ? "Info" : "Error"
            };

            // Save to DB
            _context.Logviewmodel.Add(logEntry);
            _context.SaveChanges();

            // Also log to console/output
            _logger.LogInformation("Executed action: {ActionName} at {Time}", actionName, DateTime.UtcNow);
            if (context.Exception != null)
            {
                _logger.LogError(context.Exception, "An error occurred in action {ActionName}", actionName);
            }
        }
    }
}

