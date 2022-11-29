using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LowLevel.Controllers.Models;
using LowLevel.Services;
using Microsoft.AspNetCore.Mvc;

namespace LowLevel.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController: ControllerBase
    {
        private Counter _counter;

        public HomeController(Counter counter)
        {
            _counter = counter;
        }

        [HttpGet("sub/{id:int}")]
        //[Route("sub/{id:int}")]
        public string Index(int id)
        {
            _counter.Increment();
            return $"Hello wereld {id}";
        }

        [HttpGet("person")]
        public PersonModel GetPerson(int id = 0)
        {
                return new PersonModel {Id = id, Name ="Jan", Age=42};
        }
        [HttpPost("person")]
        public IActionResult Post(PersonModel model)
        {
            model.Id = 42;
            return CreatedAtAction(nameof(GetPerson), routeValues: new {id=model.Id}, model);
        }
    }
}