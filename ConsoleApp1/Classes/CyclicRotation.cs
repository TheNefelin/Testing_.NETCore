namespace ConsoleApp1.Classes;

//✅ 2.Cyclic Rotation
//Contexto:
//Tienes un arreglo de enteros y necesitas rotarlo hacia la derecha K veces. Cada rotación mueve el último elemento al principio del arreglo.
//Ejemplo:
//A = [3, 8, 9, 7, 6], K = 3
//Resultado: [9, 7, 6, 3, 8]

public class CyclicRotation
{
    public int[] Solution(int[] A, int K)
    {
        if (A.Length == 0)
        {
            return A;
        }

        K = K % A.Length; // Handle cases where K is greater than the length of the array

        int[] rotatedArray = new int[A.Length];

        for (int i = 0; i < A.Length; i++)
        {
            int newIndex = (i + K) % A.Length;
            rotatedArray[newIndex] = A[i];
        }

        return rotatedArray;
    }
}
