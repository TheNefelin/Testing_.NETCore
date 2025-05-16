namespace ConsoleApp1.Classes;

//✅ 4.Tape Equilibrium
//Contexto:
//Tienes una cinta (arreglo de enteros) y quieres cortarla en dos partes. Debes encontrar el punto donde la diferencia absoluta entre las sumas de ambos lados sea mínima.
//Ejemplo:
//A = [3, 1, 2, 4, 3]
//Cortes posibles → mínima diferencia es 1

public class TapeEquilibrium
{
    public int Solution(int[] A)
    {
        var sumTotal = A.Sum();
        int leftSum = 0;
        int rightSum = sumTotal;
        int diff = 0;
        int minDiff = sumTotal;

        foreach (int e in A)
        {
            leftSum += e;
            rightSum -= e;
            diff = Math.Abs(leftSum - rightSum);

            if (diff < minDiff)
            {
                minDiff = diff;
            }
        }

        return minDiff;
    }
}
