using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProductApi
{
    public class MijnAttribute : Attribute, IActionFilter, IResultFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            System.Console.WriteLine("Na Action");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            System.Console.WriteLine("Voor Action");
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            System.Console.WriteLine("Na Result");
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            System.Console.WriteLine("Voor Result");
        }
    }
}