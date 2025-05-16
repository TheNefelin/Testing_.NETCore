namespace ConsoleApp1.Classes;

//✅ 1.Binary Gap
//Contexto:
//Estás trabajando con números binarios y quieres encontrar la secuencia más larga de ceros consecutivos que esté rodeada por unos en ambos extremos.

//Ejemplo de entrada:
//N = 529 → binario: 1000010001
//Resultado esperado: 4(hay una secuencia de 4 ceros entre unos)

public class BinaryGap
{
    public int Solution(int number)
    {
        // Convert the number to binary and remove the trailing zeros
        string binary = Convert.ToString(number, 2).TrimEnd('0');

        // Split the binary string by '1' and find the maximum length of the gaps
        var gaps = binary.Split('1');
        return gaps.Length > 1 ? gaps.Max(gap => gap.Length) : 0;
    }

    private int ToBinary(int number)
    {
        int binary = 0;
        int multiplier = 1;

        while (number > 0)
        {
            int remainder = number % 2;

            binary += remainder * multiplier;
            multiplier *= 10;
            number /= 2;
        }

        return binary;
    }
}
