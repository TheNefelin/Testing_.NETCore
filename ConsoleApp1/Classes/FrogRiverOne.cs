namespace ConsoleApp1.Classes;

//✅ 5. Frog River One
//Contexto:
//Una rana quiere cruzar un río saltando sobre hojas que caen en posiciones del 1 al X. Dado un arreglo donde cada índice representa un segundo y el valor la posición donde cae una hoja, determina el primer segundo en que la rana puede cruzar el río (cuando todas las posiciones del 1 al X están cubiertas).
//Ejemplo:
//X = 5, A = [1, 3, 1, 4, 2, 3, 5, 4]
//Resultado: 6

public class FrogRiverOne
{
    public int Solution(int X, int[] A)
    {
        // Crear un conjunto para rastrear las posiciones de las hojas
        HashSet<int> positions = new HashSet<int>();

        // Recorrer el arreglo A
        for (int second = 0; second < A.Length; second++)
        {
            // Agregar la posición de la hoja al conjunto
            positions.Add(A[second]);

            // Verificar si todas las posiciones del 1 al X están cubiertas
            if (positions.Count == X)
            {
                return second; // Retornar el segundo en que la rana puede cruzar
            }
        }

        return -1; // Si no se puede cruzar, retornar -1
    }
}
