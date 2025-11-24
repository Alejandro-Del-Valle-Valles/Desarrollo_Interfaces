namespace SwitchesDinamicos.Views
{
    public partial class MainPage : ContentPage
    {
        private int count = 0;
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnAddSwitchClicked(object sender, EventArgs e)
        {
            string? name = await DisplayPromptAsync("Introduce un nombre.", "Debes dar un nombre a la etiqueta del switch.");
            if (string.IsNullOrEmpty(name?.Trim()))
            {
                await DisplayAlert("No se puede introducir un nombre vacío.", "Debes introducir un nombre para crear el switch.", "Ok");
            }
            else
            {
                CreateSwitch(name);
            }
        }

        private void CreateSwitch(string name)
        {
            count++;
            HorizontalStackLayout layout = new HorizontalStackLayout();
            Switch newSwitch = new Switch { StyleId = name, Margin = 15 };
            Label newLabel = new Label { Text = $"Switch {count}", Margin = 15};
            layout.Children.Add(newSwitch);
            layout.Children.Add(newLabel);
            DynamicVerticalLayout.Children.Add(layout);
        }

        private void OnShowStatsClicked(object sender, EventArgs e)
        {
            StatusVerticalLayout.Clear();
            foreach (IView child in DynamicVerticalLayout.Children)
            {
                if (child is HorizontalStackLayout rowLayout)
                {
                    Switch? targetSwitch = rowLayout.Children.OfType<Switch>().FirstOrDefault();
                    if (targetSwitch != null)
                    {
                        HorizontalStackLayout layout = new HorizontalStackLayout();
                        Label id = new Label { Text = targetSwitch.StyleId };
                        Label value = new Label { Text = targetSwitch.IsToggled.ToString() };
                        layout.Children.Add(id);
                        layout.Children.Add(value);
                        StatusVerticalLayout.Children.Add(layout);
                    }
                }
            }
        }
    }
}
