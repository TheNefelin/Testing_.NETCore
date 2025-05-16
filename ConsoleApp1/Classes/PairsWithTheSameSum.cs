namespace ConsoleApp1.Classes;

public class PairsWithTheSameSum
{
    public ((int, int),(int, int)) Solution(int[] numbers)
    {
        Dictionary<int, List<(int, int)>> pairs = new();

        for (int i = 0; i < numbers.Length -1; i++)
        {
            for(int j = i + 1; j < numbers.Length; j++)
            {
                int sum = numbers[i] + numbers[j];

                if (!pairs.ContainsKey(sum))
                {
                    pairs[sum] = new List<(int, int)>();
                }

                pairs[sum].Add((i, j));
            }
        }

        int maxSum = 0;
        List<(int, int)> resultPairs = new();

        foreach (var pair in pairs)
        {
            if (pair.Value.Count > 1 && pair.Key > maxSum)
            {
                maxSum = pair.Key;
                resultPairs = pair.Value;
            }
        }

        return (resultPairs[0], resultPairs[1]);
    }
}
