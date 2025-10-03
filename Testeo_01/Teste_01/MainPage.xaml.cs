namespace Teste_01
{
    public partial class MainPage : ContentPage
    {
        public static readonly BindableProperty ValorNumericoProperty =
        BindableProperty.Create(nameof(ValorNumerico), typeof(int), typeof(MainPage), 50, BindingMode.TwoWay);

        public int ValorNumerico
        {
            get => (int)GetValue(ValorNumericoProperty);
            set
            {
                int nuevoValor = Math.Max(0, Math.Min(100, value));
                SetValue(ValorNumericoProperty, nuevoValor);
            }
        }


        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            Valor.TextChanged += (s, e) =>
            {
                if (int.TryParse(Valor.Text, out int valor))
                {
                    if (valor < 0)
                        Valor.Text = "0";
                    else if (valor > 100)
                        Valor.Text = "100";
                }
                else if (!string.IsNullOrEmpty(Valor.Text))
                {
                    // Si no es un número válido, lo dejamos vacío
                    Valor.Text = "";
                }
            };

        }
    }
}
