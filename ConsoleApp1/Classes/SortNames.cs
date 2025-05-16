namespace ConsoleApp1.Classes;

public class SortNames
{
    public string[] Solution(string[] names)
    {
        for (int i = 0; i < names.Length - 1; i++)
        {
            for (int j = 0; j < names.Length - 1 - i; j++)
            {
                if (names[j].CompareTo(names[j + 1]) > 0)
                {
                    string temp = names[j];
                    names[j] = names[j + 1];
                    names[j + 1] = temp;
                }
            }
        }

        return names;
    }
}
