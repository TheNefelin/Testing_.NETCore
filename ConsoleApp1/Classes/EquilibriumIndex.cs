namespace ConsoleApp1.Classes;

public class EquilibriumIndex
{
    public List<int> Solution(int[] numbers)
    {
        int totalSum = numbers.Sum();
        int leftSum = 0;
        List<int> equilibriumIndexes = new();

        for (int i = 0; i < numbers.Length; i++)
        {
            totalSum -= numbers[i];

            if (leftSum == totalSum)
            {
                equilibriumIndexes.Add(i);
            }

            leftSum += numbers[i];
        }

        return equilibriumIndexes;
    }
}
