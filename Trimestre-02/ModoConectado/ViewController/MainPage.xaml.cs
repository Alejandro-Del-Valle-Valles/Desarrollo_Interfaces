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

        private static readonly IServiceEmployee
            _employeeService = new EmployeeService(new EmployeeRepository());

        private Employee? _selectedEmployee = null;
        private Department? _selectedDepartment = null;

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

        /// <summary>
        /// Load the departments info.
        /// </summary>
        /// <returns>Task</returns>
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
            //TODO: Implementar lógica de busqueda de empleados
        }

        /// <summary>
        /// Save the new employee if all data is filled and a department is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnSaveClicked(object? sender, EventArgs e)
        {
            try
            {
                if (CheckFields() && _selectedDepartment != null)
                {
                    Result result = await _employeeService.Save(CreateEmployee());
                    if (result.IsSuccess)
                        await DisplayAlert("Empleado Guardado", "El empleado se ha guardado correctamente.", "Ok");
                    else await DisplayAlert("Error", result.Exception?.Message ?? "Error Desconocido", "Ok");
                }
                else await DisplayAlert("Faltan datos", "Para crear un empleado deben estar todos los datos rellenos y un departamento creado.", "Ok");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error inesperado", "Ha ocurrido un error inesperado durante el guardado del empleado. Vuelva a intentarlo.", "Ok");
            }
        }

        /// <summary>
        /// Update the selected employee with the new data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnUpdateClicked(object? sender, EventArgs e)
        {
            try
            {
                if (CheckFields() && _selectedEmployee != null && _selectedDepartment != null)
                {
                    Employee employee = CreateEmployee();
                    employee.Id = _selectedEmployee.Id;
                    if (_employeeService.GetById(employee.Id).Result.IsSuccess)
                    {
                        Result result = await _employeeService.Update(employee);
                        if (result.IsSuccess)
                            await DisplayAlert("Empleado Guardado", "El empleado se ha actualizado correctamente.",
                                "Ok");
                        else await DisplayAlert("Error", result.Exception?.Message ?? "Error Desconocido", "Ok");
                    }
                    else
                        await DisplayAlert("Empleado no encontrado.",
                            "El empleado que tratas de actualizar no ha sido encontrado o no existe.", "Ok");
                }
                else
                    await DisplayAlert("Faltan datos",
                        "Para actualizar un empleado todos los datos deben estar correctamente rellenados y un departamento seleccionado",
                        "Ok");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error inesperado", "Ha ocurrido un error inesperado durante el guardado del empleado. Vuelva a intentarlo.", "Ok");
            }
        }

        /// <summary>
        /// Delete from the DB the selected employee.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnDeleteClicked(object? sender, EventArgs e)
        {
            try
            {
                if (_selectedEmployee != null)
                {
                    bool wantDelete = await DisplayAlert("Confirmar Eliminación",
                            $"¿Estás seguro de que quieres eliminar a {_selectedEmployee.Surname}? Esta acción es permanente.", "Confirmar", "Cancelar");
                    if (wantDelete)
                    {
                        Result result = await _employeeService.Delete(_selectedEmployee.Id);
                        if (result.IsSuccess)
                        {
                            await DisplayAlert("Empleado eliminado con éxito", "El empleado se ha eliminado con éxito", "Ok");
                            ClearFields();
                            _selectedEmployee = null;
                        }
                        else
                            await DisplayAlert("No se eliminó al empleado",
                                "No se ha podido eliminar al empleado, comprueba que el empleado exista.", "Ok");
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error inesperado", "Ha ocurrido un error inesperado al tratar de eliminar el empleado seleccionado.", "Ok");
            }
        }

        //FORM METHODS
        private void OnClearClicked(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private async void OnSearchClicked(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// When a surname is tapped, get the employee and set all his info on the fields. Also save the selected employee to use it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSurnameTapped(object? sender, TappedEventArgs e)
        {
            _selectedEmployee = e.Parameter as Employee;
            if (_selectedEmployee != null)
            {
                EtSurname.Text = _selectedEmployee.Surname;
                EtCraft.Text = _selectedEmployee.Craft;
                EtSalary.Text = _selectedEmployee.Salary.ToString();
                EtCommission.Text = _selectedEmployee.Commission.ToString();
                DpRegistrationDate.Date = _selectedEmployee.GetParsedDate();
            }
        }

        /// <summary>
        /// When a Department is clicked, load the employees from that department.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnDepartmentTapped(object? sender, TappedEventArgs e)
        {
            var department = e.Parameter as Department;
            if (department != null)
            {
                _selectedDepartment = department;
                var result = await _employeeService.GetAllByDepartmentId(department.Id);
                EmployeesCollection.ItemsSource = result.IsSuccess
                    ? result.Data
                    : new List<Employee>();
                if (!result.IsSuccess)
                    await DisplayAlert("Error al cargar los empleados",
                        $"Ha oucurrido un error al cargar los empleados del departamento {department.Name}", "Ok");
            }
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
            RegistrationDate = DpRegistrationDate.Date.ToString(Employee.DATE_FORMAT),
            IdDepartment = _selectedDepartment?.Id ?? null
        };

        /// <summary>
        /// Check that the fields are not empty and notify the user which field is wrong.
        /// </summary>
        /// <returns>bool, true if all fields are correct, false otherwise.</returns>
        private bool CheckFields()
        {
            bool surnameIsEmpty = string.IsNullOrWhiteSpace(EtSurname.Text);
            if (surnameIsEmpty) DisplayAlert("Apellido vacío", "El apellido no puede estar vacío.", "Ok");
            bool craftIsEmpty = string.IsNullOrWhiteSpace(EtCraft.Text);
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
        /// Clear the Entrys and set the DatePicker with the actual date.
        /// </summary>
        private void ClearFields()
        {
            EtSurname.Text = string.Empty;
            EtCraft.Text = string.Empty;
            EtCommission.Text = string.Empty;
            EtSalary.Text = string.Empty;
            DpRegistrationDate.Date = DateTime.Now;
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
