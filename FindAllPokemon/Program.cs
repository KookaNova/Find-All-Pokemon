using FindAllPokemon;

//used to find all pokemon from Serebii's national dex, as well as save it to a txt file.
//I needed this for another project.

Searcher searcher = new();

searcher.FindData();
if(Console.ReadLine() == "save")
{
    WriteToTXT();
}

async void WriteToTXT()
{
    await File.WriteAllLinesAsync("../../../files/pokemon.txt", searcher.pokemon);
}

Console.ReadLine();