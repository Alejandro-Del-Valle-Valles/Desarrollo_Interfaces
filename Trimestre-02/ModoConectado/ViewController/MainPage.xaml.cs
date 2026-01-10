using ModoConectado.Enums;
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
        private SearchTypes? _selectedSearchTypes = null;

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
                SearchCollection.ItemsSource = Enum.GetValues(typeof(SearchTypes));
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

        /// <summary>
        /// Save the attribute to search employees
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSearchTypeChanged(object? sender, SelectionChangedEventArgs e)
        {
            _selectedSearchTypes = (SearchTypes)e.CurrentSelection.FirstOrDefault()!;
        }

        /// <summary>
        /// Clear the list of employees to show nothing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClearClicked(object? sender, EventArgs e)
        {
            EmployeesCollection.ItemsSource = null;
        }

        /// <summary>
        /// Handles the search logic by identifying the search type and updating the UI.
        /// </summary>
        private async void OnSearchClicked(object? sender, EventArgs e)
        {
            string searchText = EtSearch.Text?.Trim() ?? string.Empty;
            if (_selectedSearchTypes == null || string.IsNullOrWhiteSpace(searchText))
            {
                await DisplayAlert("Error", "Debes seleccionar un tipo e introducir un texto.", "Ok");
                return;
            }

            Result<IEnumerable<Employee>?> result;

            try
            {
                result = _selectedSearchTypes switch
                {
                    SearchTypes.Apellido => await _employeeService.GetBySurname(searchText),
                    SearchTypes.Oficio => await _employeeService.GetByCraft(searchText),
                    SearchTypes.Fecha_Alt => await _employeeService.GetByRegistrationDate(searchText),
                    SearchTypes.Salario => await FetchByNumericValue(searchText, isSalary: true),
                    SearchTypes.Comision => await FetchByNumericValue(searchText, isSalary: false),
                    _ => Result<IEnumerable<Employee>?>.Failure(new Exception("Tipo no soportado"))
                };

                HandleSearchResult(result, searchText);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        /// <summary>
        /// Helper to handle numeric parsing and service calls for Salary and Commission.
        /// </summary>
        private async Task<Result<IEnumerable<Employee>?>> FetchByNumericValue(string text, bool isSalary)
        {
            if (!float.TryParse(text, out float value) || value <= 0)
                throw new Exception($"Para buscar por {(isSalary ? "salario" : "comisión")} debes introducir un número positivo.");
            
            return isSalary
                ? await _employeeService.GetBySalary(value)
                : await _employeeService.GetByCommission(value);
        }

        /// <summary>
        /// Updates the CollectionView or shows an alert based on the Result.
        /// </summary>
        private async void HandleSearchResult(Result<IEnumerable<Employee>?> result, string searchedValue)
        {
            if (result.IsSuccess && result.Data != null && result.Data.Any()) EmployeesCollection.ItemsSource = result.Data;
            else
            {
                EmployeesCollection.ItemsSource = null; // Clear list if no results
                await DisplayAlert("Sin datos", $"No se han encontrado empleados para: {searchedValue}", "Ok");
            }
        }

        /// <summary>
        /// When a surname is tapped, get the employee and set all his info on the fields. Also save the selected employee to use it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSurnameChanged(object? sender, SelectionChangedEventArgs e)
        {
            _selectedEmployee = (Employee)e.CurrentSelection.FirstOrDefault()!;
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
        private async void OnDepartmentChanged(object? sender, SelectionChangedEventArgs e)
        {
            _selectedDepartment = (Department)e.CurrentSelection.FirstOrDefault()!;
            if (_selectedDepartment != null)
            {
                var result = await _employeeService.GetAllByDepartmentId(_selectedDepartment.Id);
                EmployeesCollection.ItemsSource = result.IsSuccess
                    ? result.Data
                    : new List<Employee>();
                if (!result.IsSuccess)
                    await DisplayAlert("Error al cargar los empleados",
                        $"Ha oucurrido un error al cargar los empleados del departamento {_selectedDepartment.Name}", "Ok");
            }
        }

        //AUXILIARY METHODS

        /// <summary>
        /// Create a new Employee from the Entry fields.
        /// <remarks>Check fields before create the Employee. This method doesn't chek it.</remarks>
        /// </summary>
        /// <returns>Employee</returns>
        private Employee CreateEmployee() => new Employee
        {
            Surname = EtSurname.Text,
            Craft = EtCraft.Text,
            Salary = GetFloatValueFromEntry(EtSalary) ?? 0f,
            Commission = GetFloatValueFromEntry(EtCommission) ?? 0f,
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
            if (commissionIsNegative) DisplayAlert("Comisión Incorrecta", "La comisiona no puede ser negativa, nulo o un texto.", "Ok");
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
        private float? GetFloatValueFromEntry(Entry entry)
        {
            bool isNumeric = float.TryParse(entry.Text, out float value);
            return isNumeric ? value : null;
        }
    }
}
