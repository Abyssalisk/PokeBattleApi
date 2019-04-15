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
            public AttackInfo attack { get; set; }

            public class AttackInfo
            {
                public string name { get; set; }
                public string power { get; set; }
            }

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

            public void getMoveDamage(string ID)
            {
                var client = new RestClient($"https://pokeapi.co/api/v2/move/{ID}/");
                var request = new RestRequest(Method.GET);
                request.AddHeader("cache-control", "no-cache");
                IRestResponse response = client.Execute(request);

                var attack = JsonConvert.DeserializeObject<AttackInfo>(response.Content);
                this.attack = attack; 
            }
        }

        static void Main(string[] args)
        {
            var randomAttack = 0;
            int p1Attack;
            int p2Attack;

            var pokeID = r.Next(1, 785).ToString();
            var p1 = Pokemon.getNewPokemon(pokeID);
            int p1HP = p1.stats[5].base_stat * 10;

            pokeID = r.Next(1, 785).ToString();
            var p2 = Pokemon.getNewPokemon(pokeID);
            int p2HP = p2.stats[5].base_stat * 10;

            while (p1HP > 0 && p2HP > 0)
            {
                Console.WriteLine(p1.name + " HP" + p1HP + " " + p2.name + " HP" + p2HP + "\n");

                if (p1.stats[0].base_stat > p2.stats[0].base_stat)
                {
                    randomAttack = r.Next(p1.moves.Count);
                    p1.getMoveDamage(p1.moves[randomAttack].move.name);

                    if (string.IsNullOrEmpty(p1.attack.power))
                    {
                        p1Attack = 0;
                    }
                    else p1Attack = int.Parse(p1.attack.power);

                    p2HP = (p2HP - p1Attack);
                    Console.WriteLine(p1.name + " used " + p1.attack.name + " and did " + p1Attack + " damage.");

                    randomAttack = r.Next(p2.moves.Count);
                    p2.getMoveDamage(p2.moves[randomAttack].move.name);

                    if (string.IsNullOrEmpty(p2.attack.power))
                    {
                        p2Attack = 0;
                    }
                    else p2Attack = int.Parse(p2.attack.power);

                    p1HP = (p1HP - p2Attack);
                    Console.WriteLine(p2.name + " used " + p2.attack.name + " and did " + p2Attack + " damage.");

                }
                else
                {
                    randomAttack = r.Next(p2.moves.Count);
                    p2.getMoveDamage(p2.moves[randomAttack].move.name);

                    if (string.IsNullOrEmpty(p2.attack.power))
                    {
                        p2Attack = 0;
                    }
                    else p2Attack = int.Parse(p2.attack.power);

                    p1HP = (p1HP - p2Attack);
                    Console.WriteLine(p2.name + " used " + p2.attack.name + " and did " + p2Attack + " damage.");

                    randomAttack = r.Next(p1.moves.Count);
                    p1.getMoveDamage(p1.moves[randomAttack].move.name);

                    if (string.IsNullOrEmpty(p1.attack.power))
                    {
                        p1Attack = 0;
                    }
                    else p1Attack = int.Parse(p1.attack.power);

                    p2HP = (p2HP - p1Attack);
                    Console.WriteLine(p1.name + " used " + p1.attack.name + " and did " + p1Attack + " damage.");
                }
            }                     
            if(p1HP > p2HP)
            {
                Console.WriteLine("\n" + p1.name + " was victorious!");
            } else Console.WriteLine("\n" + p2.name + " was victorious!");
        }
    }
}
