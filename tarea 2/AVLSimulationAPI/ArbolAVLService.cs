using System.Collections.Generic;

namespace AVLSimulationAPI;

public class ArbolAVLService
{
    private List<NodoAVL> _estadoArbol = new List<NodoAVL>
    {
        new NodoAVL { Id = 30, Etiqueta = "Nodo Raíz (Abuelo) - FE: -2", Altura = 2 },
        new NodoAVL { Id = 10, Etiqueta = "Hijo Izquierdo - FE: +1", Altura = 1 }
    };

    public List<NodoAVL> ObtenerEstadoActual()
    {
        return _estadoArbol;
    }

    public (bool exito, string mensaje, List<NodoAVL>? estructura) InsertarNodo(NodoAVL nuevoNodo)
    {
        if (nuevoNodo.Id <= 0)
            return (false, "ID de nodo inválido.", null);

        // Simulación de rotación RID para el nodo 20
        if (nuevoNodo.Id == 20)
        {
            _estadoArbol.Clear();
            _estadoArbol.Add(new NodoAVL { Id = 20, Etiqueta = "Nueva Raíz Balanceada (RID) - FE: 0", Altura = 2 });
            _estadoArbol.Add(new NodoAVL { Id = 10, Etiqueta = "Hijo Izquierdo - FE: 0", Altura = 1 });
            _estadoArbol.Add(new NodoAVL { Id = 30, Etiqueta = "Hijo Derecho - FE: 0", Altura = 1 });

            return (true, "Rotación RID ejecutada con éxito. Estabilidad total lograda.", _estadoArbol);
        }

        _estadoArbol.Add(nuevoNodo);
        return (true, "Nodo insertado sin rotación.", _estadoArbol);
    }
}