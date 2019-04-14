using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PokeApiBattleSimulator
{
    class Program
    {
        static Random r = new Random();

        public class Pokemon
        {      
            public string name { get; set; }
            public List<Move> moves { get; set; }
            public List<Stat> stats { get; set; }

            public class Move2
            {
                public string name { get; set; }
                public string url { get; set; }
            }

            public class Move
            {
                public Move2 move { get; set; }
            }

            public class Stat2
            {
                public string name { get; set; }
                public string url { get; set; }
            }

            public class Stat
            {
                public int base_stat { get; set; }
                public Stat2 stat { get; set; }
            }

            public static Pokemon getNewPokemon(string ID)
            {
                var client = new RestClient($"https://pokeapi.co/api/v2/pokemon/{ID}/");
                var request = new RestRequest(Method.GET);
                request.AddHeader("cache-control", "no-cache");
                IRestResponse response = client.Execute(request);

                var pokeName = JsonConvert.DeserializeObject<Pokemon>(response.Content);
                return pokeName;
            }

            // sub method to get moves of a pokemon


            // createPokemon (populates the values for the pokemon from the response


            // doDamage(pokemon1, pokemon2)

        }

        static void Main(string[] args)
        {
            var pokeID = r.Next(1, 785).ToString();
            var p1 = Pokemon.getNewPokemon(pokeID);
            Console.WriteLine(p1.name);

            pokeID = r.Next(1, 785).ToString();
            var p2 = Pokemon.getNewPokemon(pokeID);
            Console.WriteLine(p2.name);
        }
    }
}
