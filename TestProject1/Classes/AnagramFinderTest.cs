using ConsoleApp1.Classes;

namespace TestProject1.Classes;

public class AnagramFinderTest
{
    [Fact]
    public void TestAnagramFinder1()
    {
        AnagramFinder anagramFinder = new AnagramFinder();
        string result = anagramFinder.Solution("listen silent");
        Assert.Equal("listen, silent", result);
    }

    [Fact]
    public void TestAnagramFinder2() {
        AnagramFinder anagramFinder = new AnagramFinder();
        string result = anagramFinder.Solution("MARY WORKS IN ARMY");
        Assert.Equal("MARY, ARMY", result);
    }

    [Fact]
    public void TestNoAnagrams()
    {
        AnagramFinder anagramFinder = new AnagramFinder();
        string result = anagramFinder.Solution("hello world");
        Assert.Equal("No hay anagramas", result);
    }
}
