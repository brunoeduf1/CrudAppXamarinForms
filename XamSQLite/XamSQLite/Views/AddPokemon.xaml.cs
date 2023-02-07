using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamSQLite.Models;
using XamSQLite.ViewModels;

namespace XamSQLite.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPokemon : ContentPage
    {
        public AddPokemon(Pokemon pokemon)
        {
            try
            {
                InitializeComponent();
                AddPokemonViewModel vm = new AddPokemonViewModel(pokemon);
                this.BindingContext = vm;
            }
            catch(Exception ex)
            {
               
            }
            
        }
    }
}