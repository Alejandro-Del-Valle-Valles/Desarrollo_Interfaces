using ModoConectado.Interfaces;
using ModoConectado.Model;
using ModoConectado.Repository;
using ModoConectado.Service;

namespace ModoConectado.ViewController
{
    public partial class MainPage : ContentPage
    {

        private static readonly IService<Department, int> _departmentService =
            new DepartmentService(new DepartmentRepository());

        private static readonly IService<Employee, int>
            _employeeService = new EmployeeService(new EmployeeRepository());

        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                Result result = await _departmentService.InitializeRepository();
                if (!result.IsSuccess)
                {
                    await DisplayAlert("Error al inicializar la BBDD", result.Exception?.Message ?? "Error desconocido", "Ok");
                    return;
                }
                result = await  _employeeService.InitializeRepository();
                if (!result.IsSuccess) await DisplayAlert("Error al inicializar la BBDD", result.Exception?.Message ?? "Error desconocido", "Ok");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error inesperado", ex.Message, "Ok");
            }
        }

        //CRUD CALL METHODS
        private void OnLabelSearchTapped(object? sender, TappedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnSaveClicked(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnUpdateClicked(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnDeleteClicked(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        //FORM METHODS
        private void OnClearClicked(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnSearchClicked(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnSurnameTapped(object? sender, TappedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
