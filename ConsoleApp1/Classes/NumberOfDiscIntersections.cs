namespace ConsoleApp1.Classes;

//✅ 8.Number of Disc Intersections
//Contexto:
//Imagina que tienes varios discos en una línea (como círculos con centro en i y radio A[i]). Tu tarea es contar cuántos pares de discos se intersectan.
//Ejemplo:
//A = [1, 5, 2, 1, 4, 0]
//Resultado: 11(hay 11 pares de discos que se tocan o se superponen)

public class NumberOfDiscIntersections
{
    public int Solution(int[] A)
    {
        int n = A.Length;
        int[] start = new int[n];
        int[] end = new int[n];

        for (int i = 0; i < n; i++)
        {
            start[i] = i - A[i];
            end[i] = i + A[i];
        }

        Array.Sort(start);
        Array.Sort(end);

        int intersections = 0;
        int openDiscs = 0;
        int j = 0;

        for (int i = 0; i < n; i++)
        {
            while (j < n && start[j] <= end[i])
            {
                openDiscs++;
                j++;
            }

            intersections += openDiscs - i - 1;

            if (intersections > 10000000)
                return -1;
        }

        return intersections;
    }
}
