namespace ConsoleApp1.Classes;

//✅ 6.Missing Integer
//Contexto:
//Dado un arreglo de enteros, encuentra el número entero positivo más pequeño que no aparece en el arreglo.
//Ejemplo:
//A = [1, 3, 6, 4, 1, 2]
//Resultado: 5

public class MissingInteger
{
    public int Solution(int[] A)
    {
        HashSet<int> grp = new HashSet<int>();

        foreach (int n in A)
        {
            if (n > 0)
            {
                grp.Add(n);
            }
        }

        for (int i = 1; i <= A.Length + 1; i++)
        {
            if (!grp.Contains(i))
            {
                return i;
            }
        }

        return 1;
    }
}
