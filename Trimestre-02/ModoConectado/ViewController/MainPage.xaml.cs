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

                await LoadDepartmentsAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error inesperado", ex.Message, "Ok");
            }
        }

        //LOAD DATA METHODS
        private async Task LoadDepartmentsAsync()
        {
            Result<IEnumerable<Department>?> result = await _departmentService.GetAll();
            if (result.IsSuccess)
            {
                DepartmentsCollection.ItemsSource = result.Data;
            }
            else
                await DisplayAlert("Error al cargar los departamentos",
                    "Ha ocurrido un error al cargar los departamentos", "Ok");
        }

        //CRUD CALL METHODS
        private void OnLabelSearchTapped(object? sender, TappedEventArgs e)
        {
            DisplayAlert("Pulsado", "Busqueda Pulsada", "Ok");
        }

        private async void OnSaveClicked(object? sender, EventArgs e)
        {
            try
            {
                if (CheckFields())
                {
                    Result result = await _employeeService.Save(CreateEmployee());
                    if (result.IsSuccess)
                        DisplayAlert("Empleado Guardado", "El empleado se ha guardado correctamente.", "Ok");
                    else DisplayAlert("Error", result.Exception?.Message ?? "Error Desconocido", "Ok");
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error inesperado", "Ha ocurrido un error inesperado durante el guardado del empleado. Vuelva a intentarlo.", "Ok");
            }
        }

        private async void OnUpdateClicked(object? sender, EventArgs e)
        {
            try
            {
                if (CheckFields())
                {
                    Employee employee = CreateEmployee();
                    employee.Id = 0; //TODO: Get ID of the selected employee.
                    if (_employeeService.GetById(employee.Id).Result.IsSuccess)
                    {
                        Result result = await _employeeService.Update(employee);
                        if (result.IsSuccess)
                            await DisplayAlert("Empleado Guardado", "El empleado se ha actualizado correctamente.", "Ok");
                        else await DisplayAlert("Error", result.Exception?.Message ?? "Error Desconocido", "Ok");
                    }
                    else await DisplayAlert("Empleado no encontrado.", "El empleado que tratas de actualizar no ha sido encontrado o no existe.", "Ok");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error inesperado", "Ha ocurrido un error inesperado durante el guardado del empleado. Vuelva a intentarlo.", "Ok");
            }
        }

        private async void OnDeleteClicked(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        //FORM METHODS
        private void OnClearClicked(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private async  void OnSearchClicked(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private async void OnSurnameTapped(object? sender, TappedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnDepartmentTapped(object? sender, TappedEventArgs e)
        {
            DisplayAlert("Pulsado", "Departamentos pulsado", "Ok");
            //TODO: Obtener el ID del departamento y añadir un método que devuelva directamente los usuarios de ese departamento en vez de obtener todos y filtrarlo
        }

        /// <summary>
        /// Create a new Employee from the Entry fields.
        /// <remarks>Check fields before create the Employee. This method doesn't chek it.</remarks>
        /// </summary>
        /// <returns>Employee</returns>
        private Employee CreateEmployee() => new Employee
        {
            Surname = EtSurname.Text,
            Craft = EtCraft.Text,
            Salary = GetFloatValueFromEntry(EtSalary),
            Commission = GetFloatValueFromEntry(EtCommission),
            RegistrationDate = DpRegistrationDate.Date.ToString("dd-MM-YYYY")
        };

        /// <summary>
        /// Check that the fields are not empty and notify the user which field is wrong.
        /// </summary>
        /// <returns>bool, true if all fields are correct, false otherwise.</returns>
        private bool CheckFields()
        {
            bool surnameIsEmpty = string.IsNullOrWhiteSpace(EtSurname.Text);
            if (surnameIsEmpty) DisplayAlert("Apellido vacío", "El apellido no puede estar vacío.", "Ok");
            bool craftIsEmpty = string.IsNullOrWhiteSpace(EtCommission.Text);
            if(craftIsEmpty) DisplayAlert("Oficio vacío", "El oficio no puede estar vacío.", "Ok");
            bool salaryIsNegative = GetFloatValueFromEntry(EtSalary) < 1f;
            if(salaryIsNegative) DisplayAlert("Salario Incorrecto", "El salario no puede ser negativo, nulo o un texto.", "Ok");
            bool commissionIsNegative = GetFloatValueFromEntry(EtCommission) < 1f;
            if (commissionIsNegative) DisplayAlert("Comision Incorrecta", "La comisiona no puede ser negativa, nulo o un texto.", "Ok");
            bool dateIsFuture = DpRegistrationDate.Date > DateTime.Now;
            if(dateIsFuture) DisplayAlert("Fecha Incorrecta", "La fecha no puede ser futura.", "Ok");
            return !surnameIsEmpty && !craftIsEmpty && !salaryIsNegative && !commissionIsNegative && !dateIsFuture;
        }

        /// <summary>
        /// Try parse the value of an Entry
        /// </summary>
        /// <param name="entry">Entry to parse value to float</param>
        /// <returns>Float value</returns>
        private float GetFloatValueFromEntry(Entry entry)
        {
            float.TryParse(entry.Text, out float value);
            return value;
        }
    }
}
