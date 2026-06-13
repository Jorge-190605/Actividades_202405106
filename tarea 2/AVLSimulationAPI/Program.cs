using AVLSimulationAPI;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ArbolAVLService>();
var app = builder.Build();

app.MapGet("/", (HttpResponse res) => res.Redirect("/api/arbol"));

app.MapGet("/api/arbol", (ArbolAVLService s) => Results.Ok(s.ObtenerEstadoActual()));

app.MapPost("/api/arbol/insertar", (NodoAVL nodo, ArbolAVLService s) =>
{
    var (ok, msg, estructura) = s.InsertarNodo(nodo);
    return ok ? Results.Created("/api/arbol", new { Mensaje = msg, Estructura = estructura })
              : Results.BadRequest(msg);
});

app.Run();