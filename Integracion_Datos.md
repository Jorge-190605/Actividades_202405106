# Integracion_Datos

**Estudiante:** Jorge Velasquez  
**Curso:** Introducción a la Programación y Computación 2  
**Fecha:** 26 de junio de 2026  

## Parte 1

| Formato | Ventajas | Desventajas |
|---|---|---|
| CSV | Extremadamente ligero, fácil de generar desde Excel. | No soporta jerarquías complejas, solo datos planos. |
| XML | Estructurado, soporta tipos de datos y jerarquías. | Verboso, archivos más pesados que JSON o CSV. |

### 1. Serialización y Deserialización
**Serialización** es el proceso de convertir un objeto de C# en una representación de texto, como JSON, mediante `System.Text.Json.JsonSerializer.Serialize()`.

**Deserialización** es el proceso inverso: convertir un documento JSON en un objeto de C# utilizando `JsonSerializer.Deserialize<T>()`.

### 2. Antipatrón N+1
El problema N+1 consiste en realizar una operación de base de datos por cada registro leído de un archivo masivo, provocando un gran número de consultas o inserciones y disminuyendo el rendimiento.

La solución consiste en utilizar **Batching**, almacenando temporalmente los registros en una colección y realizando una única inserción mediante `AddRange()` seguida de una sola llamada a `SaveChangesAsync()`.

## Parte 2

 ### Desafío 1:Consumo de Endpoints y Deserialización


```
using System.Text.Json;

public async Task<Alumno?> ObtenerAlumnoAsync()
{
    using var client = new HttpClient();

    try
    {
        var response = await client.GetAsync("https://api.usac.edu/v1/alumnos");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        return JsonSerializer.Deserialize<Alumno>(json, options);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
        return null;
    }
}
```

### Desafío 2:Endpoint para Carga Masiva CSV

```
[HttpPost]
public async Task<IActionResult> CargarCsv(IFormFile archivo)
{
    if (archivo == null || archivo.Length == 0)
        return BadRequest("Archivo inválido.");

    var alumnos = new List<Alumno>();

    using var reader = new StreamReader(archivo.OpenReadStream());

    await reader.ReadLineAsync(); // Encabezado

    while (!reader.EndOfStream)
    {
        var linea = await reader.ReadLineAsync();
        if (string.IsNullOrWhiteSpace(linea))
            continue;

        var datos = linea.Split(',');

        alumnos.Add(new Alumno
        {
            Carnet = datos[0],
            Nombre = datos[1]
        });
    }

    _context.Alumnos.AddRange(alumnos);
    await _context.SaveChangesAsync();

    return Ok(new
    {
        Mensaje = "Carga masiva completada.",
        Registros = alumnos.Count
    });
}
```

## Parte 3. Referencia

Facultad de Ingeniería, USAC. (2026). *Sesión 20: Integración de Datos. Consumo de APIs Externas y Carga Masiva (CSV/XML).* Laboratorio del curso Introducción a la Programación y Computación 2. Guatemala.
