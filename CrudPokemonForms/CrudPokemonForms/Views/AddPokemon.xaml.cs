using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CrudPokemonForms.Models;
using CrudPokemonForms.ViewModels;

namespace CrudPokemonForms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPokemon : ContentPage
    {
        public AddPokemon(Pokemon pokemon)
        {
            InitializeComponent();
            AddPokemonViewModel vm = new AddPokemonViewModel(pokemon);
            this.BindingContext = vm; 
        }
    }
}