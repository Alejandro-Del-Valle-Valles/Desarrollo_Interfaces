namespace ComponentePersonalizado
{
    // All the code in this file is included in all platforms.
    public class TarjetaPerfil : ContentView
    {

        public static readonly BindableProperty NombreProperty =
            BindableProperty.Create(nameof(Nombre), typeof(string), typeof(TarjetaPerfil), string.Empty);

        public static readonly BindableProperty DescripcionProperty =
            BindableProperty.Create(nameof(Descripcion), typeof(string), typeof(TarjetaPerfil), string.Empty);

        public static readonly BindableProperty ImagenProperty =
            BindableProperty.Create(nameof(Imagen), typeof(string), typeof(TarjetaPerfil), string.Empty);

        public string Nombre
        {
            get => (string)GetValue(NombreProperty);
            set => SetValue(NombreProperty, value);
        }

        public string Descripcion
        {
            get => (string)GetValue(DescripcionProperty);
            set => SetValue(DescripcionProperty, value);
        }

        public ImageSource Imagen
        {
            get => (string)GetValue(ImagenProperty);
            set => SetValue(ImagenProperty, value);
        }

        public TarjetaPerfil()
        {

        }
    }
}
