using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using XamSQLite.Database;
using XamSQLite.Models;
using XamSQLite.Services;
using XamSQLite.Views;

namespace XamSQLite.ViewModels
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

        private string _title;
        public string title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
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

        private bool _lblExperience;
        public bool lblExperience
        {
            get { return _lblExperience; }
            set { _lblExperience = value; OnPropertyChanged(); }
        }

        private bool _etyExperience;
        public bool etyExperience
        {
            get { return _etyExperience; }
            set { _etyExperience = value; OnPropertyChanged(); }
        }

        private bool _lblHeight;
        public bool lblHeight
        {
            get { return _lblHeight; }
            set { _lblHeight = value; OnPropertyChanged(); }
        }

        private bool _etyHeight;
        public bool etyHeight
        {
            get { return _etyHeight; }
            set { _etyHeight = value; OnPropertyChanged(); }
        }

        private bool _lblWeight;
        public bool lblWeight
        {
            get { return _lblWeight; }
            set { _lblWeight = value; OnPropertyChanged(); }
        }

        private bool _etyWeight;
        public bool etyWeight
        {
            get { return _etyWeight; }
            set { _etyWeight = value; OnPropertyChanged(); }
        }

        public AddPokemonViewModel(Pokemon obj)
        {
            if (obj == null || obj.ID == 0)
            {
                ClearPokemon();
                btnSaveLabel = "ADD";
                title = "ADD POKEMON";
                btnSavePokemon = new Command(SavePokemon);

                ShowEntryUpdate(false);
            }

            else
            {
                pokemon = obj;
                btnSaveLabel = "UPDATE";
                title = "UPDATE POKEMON";
                btnSavePokemon = new Command(UpdatePokemon);

                ShowEntryUpdate(true);
            }

            btnClearPokemon = new Command(ClearPokemon);
        }

        public void ShowEntryUpdate(bool visibility)
        {
            lblExperience = visibility;
            etyExperience = visibility;
            lblHeight = visibility;
            etyHeight = visibility;
            lblWeight = visibility;
            etyWeight = visibility;
        }

        public void UpdatePokemon()
        {
            try
            {
                lblInfo = "";
                PokemonApi pokemonApi = new PokemonApi();

                PokemonDatabase pokemonDatabase = new PokemonDatabase();
                int item = pokemonDatabase.SavePokemonInDatabase(pokemon).Result;

                if (item == 1)
                {
                    ClearPokemon();
                    lblInfo = "Your Pokemon updated successfully.";
                }

                else
                    lblInfo = "Cannot update Pokemon information";
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async void SavePokemon()
        {
            try
            {
                lblInfo = "";
                PokemonApi pokemonApi = new PokemonApi();

                var pokemonFromApi = await pokemonApi.GetPokemonFromApi(pokemon.Name);

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
                    }

                    else
                        lblInfo = "Cannot save Pokemon information";
                }

                else
                    lblInfo = "The entered value does not correspond to a Pokemon";
            }

            catch (Exception ex)
            {
                lblInfo = "The entered value does not correspond to a Pokemon";
                Console.WriteLine(ex.Message);
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
