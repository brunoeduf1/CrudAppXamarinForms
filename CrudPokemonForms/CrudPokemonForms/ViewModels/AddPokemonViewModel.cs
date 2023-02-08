using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using CrudPokemonForms.Database;
using CrudPokemonForms.Models;
using CrudPokemonForms.Services;
using CrudPokemonForms.Views;
using System.Threading.Tasks;

namespace CrudPokemonForms.ViewModels
{
    public class AddPokemonViewModel : INotifyPropertyChanged
    {
        public Command btnSavePokemon { get; set; }
        public Command btnClearPokemon { get; set; }
        private Pokemon _pokemon { get; set; }
        public Pokemon pokemon
        {
            get { return _pokemon; }
            set { _pokemon = value; OnPropertyChanged(); }
        }

        private string _lblInfo { get; set; }
        public string lblInfo
        {
            get { return _lblInfo; }
            set { _lblInfo = value; OnPropertyChanged(); }
        }

        private string _btnSaveLabel { get; set; }
        public string btnSaveLabel
        {
            get { return _btnSaveLabel; }
            set { _btnSaveLabel = value; OnPropertyChanged(); }
        }

        private bool _isLoading { get; set; }
        public bool isLoading
        {
            get { return _isLoading; }
            set { _isLoading = value; OnPropertyChanged(); }
        }

        public AddPokemonViewModel(Pokemon obj)
        {
            if (obj == null || obj.ID == 0)
            {
                ClearPokemon();
                btnSavePokemon = new Command(SavePokemon);
            }

            btnClearPokemon = new Command(ClearPokemon);
        }

        public async void SavePokemon()
        {
            try
            {
                lblInfo = "";
                PokemonApi pokemonApi = new PokemonApi();

                isLoading = true;
                var pokemonFromApi = await pokemonApi.GetPokemonFromApi(pokemon.Name);
                isLoading = false;

                if (!string.IsNullOrEmpty(pokemonFromApi.Name))
                {
                    pokemon.Name = pokemonFromApi.Name;
                    pokemon.base_experience = pokemonFromApi.base_experience;
                    pokemon.height = pokemonFromApi.height;
                    pokemon.weight = pokemonFromApi.weight;

                    PokemonDatabase pokemonDatabase = new PokemonDatabase();
                    int item = pokemonDatabase.SavePokemonInDatabase(pokemon).Result;

                    if (item == 1)
                    {
                        ClearPokemon();
                        lblInfo = "Your Pokemon saved successfully.";
                        await Task.Delay(3000);
                        lblInfo = "";
                    }
                    else
                        lblInfo = "Cannot save Pokemon information";
                }
                else
                    lblInfo = "The entered value does not correspond to a Pokemon";
            }

            catch (Exception ex)
            {
                lblInfo = "The entered value does not correspond to a Pokemon \n\n" + ex.Message;
            }
        }

        public void ClearPokemon()
        {
            pokemon = new Pokemon();
            pokemon.ID = 0;
            pokemon.Name = "";
            lblInfo = "";
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
