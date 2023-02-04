using Xamarin.Forms;
using XamSQLite.Views;

namespace XamSQLite
{
    public partial class App : Application
    {
        public App()
        {
           
            InitializeComponent();
           
            MainPage = new NavigationPage(new ProductPage());
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}