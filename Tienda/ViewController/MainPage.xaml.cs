using System;

namespace Tienda.ViewController
{
    public partial class MainPage : ContentPage
    {
        private IDispatcherTimer Timer;
        public MainPage()
        {
            InitializeComponent();
            Timer = Dispatcher.CreateTimer();
            Timer.Interval = TimeSpan.FromSeconds(1);
            Timer.Tick += (s, e) => UpdateClock();
            UpdateClock();
        }

        private void UpdateClock()
        {
            ClockLbl.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Timer.Start();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Timer.Stop();
        }
    }
}
