namespace Calculadora
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            const int newWidth = 375;
            const int newHeight = 475;

            var window = new Window(new AppShell())
            {
                Width = newWidth,
                Height = newHeight,
                MinimumWidth = newWidth,
                MaximumWidth = newWidth,
                MinimumHeight = newHeight,
                MaximumHeight = newHeight
            };

            return window;
        }

    }
}