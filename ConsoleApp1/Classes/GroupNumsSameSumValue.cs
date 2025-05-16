
namespace ConsoleApp1.Classes;

public class GroupNumsSameSumValue
{
    public int[] Solution(int[] numbers)
    {
        Dictionary<int, List<int>> dictGrp = new();

        foreach (var number in numbers)
        {
            int sum = SumDigits(number);

            if (!dictGrp.ContainsKey(sum))
            {
                dictGrp[sum] = new List<int>();
            }

            dictGrp[sum].Add(number);
        }

        int maxSum = dictGrp.Keys.Max();
        int maxCount = dictGrp.Values.Max(x => x.Count());
        int nCount = 0;
        List<int> nList = new();

        foreach (var grp in dictGrp)
        {
            if (grp.Value.Count() == maxCount)
            {
                nCount++;
                nList = grp.Value;
            }
        }

        if (nCount > 1)
        {
            return dictGrp[maxSum].ToArray();
        }

        return nList.ToArray();
    }

    private int SumDigits(int n)
    {
        int sum = 0;
     
        while (n > 0)
        {
            sum = sum + n % 10; // sum += n % 10;
            n = n / 10;         // n /= 10;
        }

        return sum;
    }
}
