using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrudPokemonForms.Models;

namespace CrudPokemonForms.Database
{
    public class PokemonDatabase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public PokemonDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Pokemon).Name))
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Pokemon)).ConfigureAwait(false);

                initialized = true;
            }
        }

        public Task<List<Pokemon>> GetPokemonFromDatabase()
        {
            return Database.Table<Pokemon>().ToListAsync();
        }

        public Task<int> SavePokemonInDatabase(Pokemon pokemon)
        {
            if (pokemon.ID == 0)
                return Database.InsertAsync(pokemon);
            else
                return Database.UpdateAsync(pokemon);
        }

        public Task<int> DeletePokemonFromDatabase(Pokemon pokemon)
        {
            return Database.DeleteAsync(pokemon);
        }
    }
}
