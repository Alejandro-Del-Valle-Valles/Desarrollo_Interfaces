using SimulcaroExmane01.Model;
using SimulcaroExmane01.Repository;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SimulcaroExmane01.Views;

public partial class ListProductsPage : ContentPage
{
    private ObservableCollection<Producto> _productos;
    public ICommand DeleteProductCommand { get; }

    public ListProductsPage()
    {
        InitializeComponent();
        DeleteProductCommand = new Command<Producto>(ExecuteDeleteProductCommand);
        this.BindingContext = this;
        _productos = new ObservableCollection<Producto>();
        ProductosView.ItemsSource = _productos;

        MostrarProductos();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        MostrarProductos();
    }

    private void ExecuteDeleteProductCommand(Producto producto)
    {
        if (ProductosRepository.Eliminar(producto))
        {
            _productos.Remove(producto);
            DisplayAlert("Éxito", $"Producto '{producto.Nombre}' eliminado correctamente.", "Ok");
        }
        else
        {
            DisplayAlert("Error", $"No se pudo eliminar el producto '{producto.Nombre}'.", "Ok");
        }
    }

    private void OnOnlyAviableStockSwitched(object? sender, ToggledEventArgs e)
    {
        if (e.Value)
        {
            var allProducts = ProductosRepository.GetProductos();
            var filteredProducts = allProducts.Where(p => p.Stock > 0);
            UpdateProductList(filteredProducts);
        }
        else
        {
            MostrarProductos();
        }
    }
    private void UpdateProductList(IEnumerable<Producto> newProducts)
    {
        _productos.Clear();
        foreach (var p in newProducts)
        {
            _productos.Add(p);
        }
    }

    private void MostrarProductos()
    {
        List<Producto>? nuevosProductos = ProductosRepository.GetProductos()?.ToList();

        if (nuevosProductos != null && nuevosProductos.Count > 0)
        {
            UpdateProductList(nuevosProductos);
        }
        else
        {
            _productos.Clear();
            DisplayAlert("Sin registros", "No se encontraron productos.", "Ok");
        }
    }
}