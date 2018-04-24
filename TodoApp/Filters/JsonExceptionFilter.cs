using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using TodoApp.Models;

namespace TodoApp.Filters
{
    public class JsonExceptionFilter:IExceptionFilter
    {
        private readonly IHostingEnvironment _env;
        public JsonExceptionFilter(IHostingEnvironment env)
        {
            _env = env;
        }
        public void OnException(ExceptionContext context)
        {
            var error = new ErrorApi();
            if (_env.IsDevelopment())
            {
                error.Message = context.Exception.Message;
                error.Detail = context.Exception.StackTrace;
            }
            else
            {
                error.Message = "A server error has occured";
                error.Detail = context.Exception.StackTrace;
            }
            context.Result = new ObjectResult(error)
            {
                StatusCode = 500
            };
        }
    }
}
