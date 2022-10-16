using AngularTest.Data;
using AngularTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactosController : ControllerBase
    {
        private List<Contacto> contactos = new List<Contacto>();
        private readonly ApplicationDataContext _context;

        public ContactosController(ApplicationDataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetContactos()
        {
            var results = await _context.Contactos.ToListAsync();
            return Ok(results);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactoById(int id)
        {
            var result = await _context.Contactos.FirstOrDefaultAsync(x => x.Id == id);
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateContacto(Contacto contacto)
        {

            _context.Add(contacto);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateContacto(Contacto contacto)
        {
            var resultDb = await _context.Contactos.FirstOrDefaultAsync(x => x.Id == contacto.Id);

            if (resultDb is null) {
                return NotFound();
            }

            resultDb.Nombre = contacto.Nombre;
            resultDb.Direccion = contacto.Direccion;
            resultDb.Telefono = contacto.Telefono;
            resultDb.Curp = contacto.Curp;
            resultDb.FechaRegistro = contacto.FechaRegistro;


            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContacto(int id)
        {
            var result = await _context.Contactos.FirstOrDefaultAsync(x => x.Id == id);
            if (result is null)
            {
                return NotFound();
            }
            _context.Remove(result);
            await _context.SaveChangesAsync();

            return Ok();

        }
    }
}
