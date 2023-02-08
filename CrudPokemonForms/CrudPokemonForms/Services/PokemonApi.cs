using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CrudPokemonForms.Models;

namespace CrudPokemonForms.Services
{
    public class PokemonApi
    {
        const string url = "https://pokeapi.co/api/v2/pokemon/";

        private HttpClient GetClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("accept", "application/json");
            client.DefaultRequestHeaders.Add("connection", "close");
            return client;
        }

        public async Task<Pokemon> GetPokemonFromApi(string pokemonId)
        {
            HttpClient client = GetClient();

            var response = await client.GetAsync(url + pokemonId);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Pokemon>(content);
            }

            return new Pokemon();
        }
    }
}
