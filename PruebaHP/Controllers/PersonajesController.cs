using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PruebaHP.Data;
using PruebaHP.Model;

namespace PruebaHP.Controllers

{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class PersonajesController : ControllerBase
    {
        private readonly PersonajeContext _context;

        public PersonajesController(PersonajeContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(await _context.Personajes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personaje = await _context.Personajes
                .FirstOrDefaultAsync(m => m.PersonajeID == id);
            if (personaje == null)
            {
                return NotFound();
            }

            return Ok(personaje);
        }


        [HttpPost]
        public async Task<IActionResult> InsertPersonaje(Personaje personaje)
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC InsertarPersonaje @p0, @p1, @p2",
                    personaje.Nombre ?? "",
                    personaje.Apodo ?? "",
                    personaje.Imagen ?? ""
                );

                return Ok(new { mensaje = "Personaje insertado correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Personaje>> EliminarPersonaje(int id)
        {
            var perelim = await _context.Personajes.FindAsync(id);
            if (perelim == null)
            {
                return NotFound();
            }
            _context.Personajes.Remove(perelim);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonaje(Personaje personaje, int id)
        {
            if (id != personaje.PersonajeID)
            {
                return BadRequest();
            }

            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC ActualizarPersonaje @p0, @p1, @p2, @p3",
                    id, personaje.Nombre ?? "", personaje.Apodo ?? "", personaje.Imagen ?? ""
                );
            }
            catch (Exception ex)
            {
                if (!_context.Personajes.Any(e => e.PersonajeID == id))
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(500, "Error al actualizar el personaje: " + ex.Message);
                }
            }

            return NoContent();
        }

    }
}
