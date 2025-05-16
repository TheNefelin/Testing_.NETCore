namespace ConsoleApp1.Classes;

public class AnagramFinder
{
    public string Solution(string sentence)
    {
        string[] palabras = sentence.Split(' ');
        Dictionary<string, List<string>> mapaAnagramas = new();

        foreach (string palabra in palabras)
        {
            string palabraOrdenada = OrdenarLetras(palabra);

            if (!mapaAnagramas.ContainsKey(palabraOrdenada))
            {
                mapaAnagramas[palabraOrdenada] = new List<string>();
            }

            mapaAnagramas[palabraOrdenada].Add(palabra);
        }

        foreach (var grp in mapaAnagramas)
        {
            if (grp.Value.Count > 1)
            {
                return string.Join(", ", grp.Value);
            }
        }

        return "No hay anagramas";
    }

    private string OrdenarLetras(string palabra)
    {
        return new string(palabra
            .ToLower()
            .OrderBy(c => c)
            .ToArray());
    }
}
