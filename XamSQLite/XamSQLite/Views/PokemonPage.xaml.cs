using System;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamSQLite.Models;
using XamSQLite.ViewModels;

namespace XamSQLite.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PokemonPage : ContentPage
    {
        PokemonViewModel vm;
        public PokemonPage()
        {
            InitializeComponent();
            vm = new PokemonViewModel();
            this.BindingContext = vm;
        }

        protected override void OnAppearing()
        {
            if (vm != null)
                vm.GetPokemons();

            base.OnAppearing();
        }

        private async void btnAddPokemon_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddPokemon(null));
        }

        private async void lstPokemon_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                if (lstPokemon.SelectedItem != null)
                {
                    var pokemon = (Pokemon)lstPokemon.SelectedItem;
                    lstPokemon.SelectedItem = null;
                    await Navigation.PushAsync(new AddPokemon(pokemon));
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async void btnDeletePokemon_Clicked(object sender, EventArgs e)
        {
            try
            {
                string ID = (sender as Button).CommandParameter.ToString();
                if (!string.IsNullOrWhiteSpace(ID))
                {
                    var pokemon = vm.lstPokemon.Where(x => x.ID.ToString() == ID);
                    var result = await DisplayAlert("Confirm", "Do you want to delete Pokemon " + pokemon.FirstOrDefault().Name + "?", "Yes", "No");
                    if (result)
                    {
                        vm.DeletePokemon(pokemon.FirstOrDefault());
                    }                 
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}