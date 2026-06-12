var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Base de datos simulada en memoria (lista estática)
var coleccionNodos = new List<NodoElemento>
{
    new NodoElemento { Id = 50, Valor = "Raíz" },
    new NodoElemento { Id = 30, Valor = "Hijo Izquierdo" },
    new NodoElemento { Id = 70, Valor = "Hijo Derecho" },
    new NodoElemento { Id = 20, Valor = "Subárbol Izquierdo" },
    new NodoElemento { Id = 40, Valor = "Subárbol Derecho de 30" },
};

app.MapGet("/", () =>
{
    return Results.Redirect("/api/nodos");
});

//obtiene todos los nodos
app.MapGet("/api/nodos", () =>
{
    return Results.Ok(coleccionNodos);
});

//inserta los nodos
app.MapPost("/api/nodos", (NodoElemento nuevoNodo) =>
{
    // Validación simple
    if (nuevoNodo.Id <= 0 || string.IsNullOrEmpty(nuevoNodo.Valor))
    {
        return Results.BadRequest("Datos del nodo inválidos. El Id debe ser positivo y el Valor no vacío.");
    }

    //por si hay duplicados 
    if (coleccionNodos.Any(n => n.Id == nuevoNodo.Id))
    {
        return Results.Conflict($"Ya existe un nodo con Id {nuevoNodo.Id}.");
    }

    coleccionNodos.Add(nuevoNodo);
    return Results.Created($"/api/nodos/{nuevoNodo.Id}", nuevoNodo);
});

app.Run();