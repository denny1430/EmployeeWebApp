using Employewebapp.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using System;



namespace Employewebapp.Filters
{
    public class LogExceptionFilter : IExceptionFilter
    {
        private readonly Empdbcontext _context;

        public LogExceptionFilter(Empdbcontext context)
        {
            _context = context;
        }

        public void OnException(ExceptionContext context)
        {
            try
            {
                var log = new ExceptionLog
                {
                    ActionName = context.ActionDescriptor.RouteValues["action"],
                    ControllerName = context.ActionDescriptor.RouteValues["controller"],
                    ExceptionMessage = context.Exception.Message,
                    StackTrace = context.Exception.StackTrace,
                    LogTime = DateTime.Now
                };

                _context.ExceptionLogs.Add(log);
                _context.SaveChanges();
                // Do NOT set context.ExceptionHandled = true unless you want to stop normal error pipeline.
            }
            catch
            {
                // Swallow logging errors to avoid masking the original exception.
            }
        }
    }
}

