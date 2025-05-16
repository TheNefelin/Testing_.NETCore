using System.Text.RegularExpressions;

namespace ConsoleApp1.Classes;

public class MaxSumFirsLastDigit
{
    public int Solution(int[] numbers)
    {
        if (numbers.Length == 0)
        {
            throw new ArgumentException("Array must not be empty");
        }

        Dictionary<(int, int), List<int>> grps = new();

        foreach (int number in numbers)
        {
            int firstDigit = FirstDigit(number);
            int lastDigit = LastDigit(number);
            var key = (firstDigit, lastDigit);

            if (!grps.ContainsKey(key))
            {
                grps[key] = new List<int>();
            }

            grps[key].Add(number);
        }

        Dictionary<(int, int), int> res = new();

        foreach (var grp in grps) 
        {
            if (grp.Value.Count > 1)
            {
                res[grp.Key] = grp.Value.Sum();
            }
        }

        if (res.Count == 0)
        {
            return -1;
        }

        return res.Values.Max();
    }

    private int FirstDigit(int n)
    {
        while (n >= 10)
        {
            n /= 10;
        }

        return n;
    }

    private int LastDigit(int n)
    {
        return n % 10;
    }
}
