using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CrudPokemonForms.Database;
using CrudPokemonForms.Models;
using static CrudPokemonForms.Models.Pokemon;

namespace CrudPokemonForms.ViewModels
{
    public class PokemonViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Pokemon> _lstPokemon { get; set; }
        public ObservableCollection<Pokemon> lstPokemon
        {
            get { return _lstPokemon; }
            set { _lstPokemon = value; OnPropertyChanged(); }
        }
        private string _lblInfo { get; set; }
        public string lblInfo
        {
            get { return _lblInfo; }
            set { _lblInfo = value; OnPropertyChanged(); }
        }
        public PokemonViewModel()
        {
            lstPokemon = new ObservableCollection<Pokemon>();
        }

        public void GetPokemon()
        {
            try
            {
                PokemonDatabase pokemonDatabase = new PokemonDatabase();
                var pokemons = pokemonDatabase.GetPokemonFromDatabase().Result;

                if (pokemons != null && pokemons.Count > 0)
                {
                    lstPokemon = new ObservableCollection<Pokemon>();

                    foreach (var item in pokemons)
                    {
                        lstPokemon.Add(new Pokemon
                        {
                            ID = item.ID,
                            Name = item.Name,
                            base_experience = item.base_experience,
                            height = item.height,
                            weight = item.weight
                        });
                    }

                    lblInfo = "Total " + pokemons.Count.ToString() + " record(s) found";
                }
                else
                {
                    lstPokemon = null;
                    lblInfo = "No Pokemon records found. Please add one";
                }                
            }

            catch (Exception ex)
            {
                lblInfo = ex.Message.ToString();
            }
        }

        public void DeletePokemon(Pokemon pokemon)
        {
            try
            {
                PokemonDatabase pokemonDatabase = new PokemonDatabase();
                var result = pokemonDatabase.DeletePokemonFromDatabase(pokemon).Result;

                if (result == 1)
                    GetPokemon();
                else
                    lblInfo = "Cannot delete this Pokemon.";
            }

            catch (Exception ex)
            {
                lblInfo = ex.Message.ToString();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
