using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using ProyectoAngular.Models;

namespace ProyectoAngular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {
        private readonly PruebasContext _pruebasContext;

        public TareaController(PruebasContext pruebasContext)
        {
            _pruebasContext = pruebasContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var listarTareas = await _pruebasContext.Tareas.ToListAsync();
            return Ok(listarTareas);
        }

        [HttpPost]
        [Route("Agregar")]
        public async Task<IActionResult> Agregar([FromBody] Tarea request)
        {
            await _pruebasContext.Tareas.AddAsync(request);
            await _pruebasContext.SaveChangesAsync();
            return Ok(request);
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var tareaEliminar = await _pruebasContext.Tareas.FindAsync(id);

            if (tareaEliminar == null)
            {
                return BadRequest("No existe la tarea");
            }
            else 
            {
                _pruebasContext.Tareas.Remove(tareaEliminar);
                await _pruebasContext.SaveChangesAsync();
                return Ok();
            }
        }
    }
}
