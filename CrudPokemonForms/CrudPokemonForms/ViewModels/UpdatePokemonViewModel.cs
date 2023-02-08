using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using CrudPokemonForms.Database;
using CrudPokemonForms.Models;
using System.Threading.Tasks;

namespace CrudPokemonForms.ViewModels
{
    public class UpdatePokemonViewModel : INotifyPropertyChanged
    {
        public Command btnUpdatePokemon { get; set; }
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

        private string _btnUpdateLabel { get; set; }
        public string btnUpdateLabel
        {
            get { return _btnUpdateLabel; }
            set { _btnUpdateLabel = value; OnPropertyChanged(); }
        }

        public UpdatePokemonViewModel(Pokemon obj)
        {
            pokemon = obj;

            btnUpdatePokemon = new Command(UpdatePokemonAsync);
            btnClearPokemon = new Command(ClearPokemon);
        }

        public async void UpdatePokemonAsync()
        {
            try
            {
                lblInfo = "";
                PokemonDatabase pokemonDatabase = new PokemonDatabase();
                int item = pokemonDatabase.SavePokemonInDatabase(pokemon).Result;

                if (item == 1)
                {
                    lblInfo = "Your Pokemon updated successfully.";
                    await Task.Delay(3000);
                    lblInfo = "";
                }
                   
                else
                    lblInfo = "Cannot update Pokemon information";
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
            pokemon.base_experience = "";
            pokemon.height = "";
            pokemon.weight = "";
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
