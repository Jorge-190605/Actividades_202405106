using Microsoft.AspNetCore.Mvc;
using Actividad3_202405106.Models;

namespace Actividad3_202405106.Controllers
{
    public class EstudianteController : Controller
    {
        // Simulación de base de datos en memoria
        private static readonly List<Estudiante> _baseDatosMemoria = new()
        {
            new Estudiante { Carne = 2026012, Nombre = "Fernando Velasquez", Promedio = 91.5 },
            new Estudiante { Carne = 2026045, Nombre = "Maria Mercedes", Promedio = 84.0 }
        };

        // GET: /Estudiante/Listar
        public IActionResult Listar()
        {
            return View(_baseDatosMemoria);
        }

        // POST: /Estudiante/Registrar 
        [HttpPost]
        public IActionResult Registrar(Estudiante nuevoEstudiante)  // Sin [FromBody]
        {
            if (nuevoEstudiante.Carne <= 0 || string.IsNullOrEmpty(nuevoEstudiante.Nombre))
            {
                // Para vista, mejor redirigir con mensaje
                TempData["Error"] = "Datos del estudiante inválidos.";
                return RedirectToAction("Listar");
            }

            _baseDatosMemoria.Add(nuevoEstudiante);
            TempData["Success"] = $"Estudiante {nuevoEstudiante.Nombre} registrado correctamente.";
            return RedirectToAction("Listar");
        }
    }
}