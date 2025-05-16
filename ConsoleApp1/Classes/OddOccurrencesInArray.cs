namespace ConsoleApp1.Classes;

//✅ 3.Odd Occurrences in Array
//Contexto:
//Se te da un arreglo en el que todos los elementos aparecen un número par de veces, excepto uno que aparece solo una vez. Encuentra ese número.
//Ejemplo:
//A = [9, 3, 9, 3, 9, 7, 9]
//Resultado: 7

public class OddOccurrencesInArray
{
    public int solution(int[] A)
    {
        Dictionary<int, int> occurrences = new();

        foreach(int n in A)
        {
            if (occurrences.ContainsKey(n))
            {
                occurrences[n]++;
            }
            else
            {
                occurrences[n] = 1;
            }
        }

        foreach (var e in occurrences)
        {
            if (e.Value == 1)
            {
                return e.Key;
            }
        }

        return -1;
    }
}
