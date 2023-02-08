using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CrudPokemonForms.Models;
using CrudPokemonForms.ViewModels;

namespace CrudPokemonForms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdatePokemon : ContentPage
    {
        public UpdatePokemon(Pokemon pokemon)
        {
            InitializeComponent();
            UpdatePokemonViewModel vm = new UpdatePokemonViewModel(pokemon);
            this.BindingContext = vm;
        }
    }  
}