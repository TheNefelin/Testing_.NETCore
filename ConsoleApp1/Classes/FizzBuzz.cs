namespace ConsoleApp1.Classes;

public class FizzBuzz
{
    public Dictionary<int, string> Solution(int start, int end)
    {
        if (start > end)
        {
            throw new ArgumentException("End most be greater then Start");
        }

        Dictionary<int, string> fizzBuzzMap = new Dictionary<int, string>();

        for (int i = start; i <= end; i++)
        {
            if (i % 3 == 0 && i % 5 == 0)
            {
                fizzBuzzMap[i] = "FizzBuzz";
            }
            else if (i % 3 == 0)
            {
                fizzBuzzMap[i] = "Fizz";    
            }
            else if (i % 5 == 0)
            {
                fizzBuzzMap[i] = "Buzz";
            }
            else
            {
                fizzBuzzMap[i] = i.ToString();
            }
        }

        return fizzBuzzMap;
    }
}
