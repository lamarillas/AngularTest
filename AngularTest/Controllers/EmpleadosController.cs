using AngularTest.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AngularTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmpleadosController : ControllerBase
    {

        private List<Empleado> empleados = new List<Empleado>();


        [HttpGet]
        public IActionResult Index()
        {
            empleados.Add(new Empleado() { 
                Id = 1,
                Nombre = "Leo Amarillas",
                Edad = 39
            });

            return Ok(empleados);
        }
    }
}
