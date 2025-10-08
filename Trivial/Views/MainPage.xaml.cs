namespace Trivial.Views
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When clicked, shows the capitals page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnCapitalsClicked(object sender, EventArgs e) => await Navigation.PushAsync(new CapitalsPage());
        
        /// <summary>
        /// When clicked, shows the Countrys Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnCountrysClicked(object sender, EventArgs e) => await Navigation.PushAsync(new CountrysPage());

    }
}
