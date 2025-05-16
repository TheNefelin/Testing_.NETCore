namespace ConsoleApp1.Classes;

//✅ 7.Max Product of Three
//Contexto:
//Se te da un arreglo de enteros. Encuentra el producto más grande posible de cualquier tres números (pueden ser negativos o positivos).
//Ejemplo:
//A = [-3, 1, 2, -2, 5, 6]
//Resultado: 60 (producto de 2 * 5 * 6)

public class MaxProductOfThree
{
    public int Solution(int[] A)
    {
        Array.Sort(A);
        int n = A.Length;

        // Dos posibles opciones:
        // 1. Tres números más grandes (al final del arreglo)
        int product1 = A[n - 1] * A[n - 2] * A[n - 3];

        // 2. Dos negativos muy pequeños (al inicio) y el más grande (al final)
        int product2 = A[0] * A[1] * A[n - 1];

        return Math.Max(product1, product2);
    }
}
