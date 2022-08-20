using AngleSharp;
using AngleSharp.Dom;

namespace FindAllPokemon
{
    internal class Searcher
    {
        List<String> numbers = new();
        string url = "https://www.serebii.net/pokemon/nationalpokedex.shtml";
        string content = "";

        public List<Pokemon> parsedMon = new();
        public List<string> pokemon = new();
        public async void FindData()
        {
            HttpClient client = new();
            Uri uri = new Uri(url);
            var response = await client.GetAsync(uri);
            Console.WriteLine("Client found at " + response);
            content = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Writing content...");
            ParseWebData();
        }

        private async void ParseWebData()
        {
            var context = BrowsingContext.New(Configuration.Default);
            var document = await context.OpenAsync(req => req.Content(content));

            var tableBody = document.QuerySelector("tbody");
            var monsters = document.QuerySelectorAll("*").Where(e => e.LocalName == "tr" && e.Children.HasClass("fooinfo")).ToList();

            foreach (var m in monsters)
            {
                var lines = m.Text().Split("\n").Select(s => s.Trim()).Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();

                parsedMon.Add(new Pokemon(
                    lines[0].Trim(), //number
                    lines[1].Trim() //name
                ));
            }
            DisplayFullList();
        }

        private void DisplayFullList()
        {
            foreach (var mon in parsedMon)
            {
                Console.WriteLine(mon.Number + " : " + mon.Name);
                pokemon.Add(mon.Number + "/" + mon.Name);
                
            }
            Console.WriteLine("Type 'save' to serialize the file to an TXT");
        }
        public record Pokemon(string Number, string Name);

    }
}
