using Tienda.DAO;
using Tienda.Extensions;
using Tienda.Service;
using Tienda.Model;
using Tienda.Interfaces;

namespace Tienda.ViewController;

public partial class RegistrationPage : ContentPage
{
    private static readonly IGenericService<Customer, string> CustomerService = new CustomerService(new CustomersTextFileDao());

    public RegistrationPage()
	{
		InitializeComponent();
        InitializeCustomers();
	}

    /// <summary>
    /// Show the customers from the repository.
    /// </summary>
    private void InitializeCustomers()
    {
        clvCustomers.ItemsSource = CustomerService.GetAll().Result.Data! ?? new List<Customer>();
    }

    /// <summary>
    /// Get the selected customer from the list.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnCustomerSelected(object sender, SelectionChangedEventArgs e)
    {
        Customer? customer = e.CurrentSelection.FirstOrDefault() as Customer;

        if (customer == null) return;
        etyName.Text = customer.Name;
        etySurname.Text = customer.Surname;
        etyCity.Text = customer.City;
        etyEmail.Text = customer.Email;
        etyComents.Text = customer.Comment;
        chbIsVip.IsChecked = customer.IsVip;
    }

    /// <summary>
    /// Save a new client if not exists.
    /// </summary>
    /// <param name="sender">The source of the event, typically the control that was clicked.</param>
    /// <param name="e">An <see cref="EventArgs"/> instance containing the event data.</param>
    private async void OnSaveClicked(object sender, EventArgs e)
    {
        try
        {
            List<Customer>? customers = CustomerService.GetAll().Result.Data?.ToList() ?? new List<Customer>();
            if (await CheckParamsAndInform())
            {
                if (customers.FirstOrDefault(c => c.Email == etyEmail.Text.ToLower()) == null)
                {
                    Task<ServiceResult> isCreated = CustomerService.Create(
                        new(etyName.Text, etySurname.Text, etyCity.Text, etyEmail.Text, etyComents.Text, chbIsVip.IsChecked)
                        );
                    if (isCreated.Result.IsSuccess)
                    {
                        await DisplayAlert("Cliente creado.", "El cliente se ha creado con éxito.", "Ok");
                        InitializeCustomers();
                    }
                    else await DisplayAlert("Ha ocurrido un error.", isCreated.Result.ErrorMessage, "Ok");
                }
                else await DisplayAlert("Cliente existente.", "El cliente que se trata de crear ya existe o existe otro con el mismo email.", "Ok");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error inesperado.", $"Ha ocurrido un error inesperado : {ex.Message}", "Ok");
        }
    }

    /// <summary>
    /// Modify the data of a user if exists.
    /// </summary>
    /// <param name="sender">The source of the event, typically the control that was clicked.</param>
    /// <param name="e">An <see cref="EventArgs"/> instance containing the event data.</param>
    private async void OnModifyClicked(object sender, EventArgs e)
    {
        try
        {
            List<Customer>? customers = CustomerService.GetAll().Result.Data?.ToList() ?? new List<Customer>();
            if (await CheckParamsAndInform())
            {
                if (customers.FirstOrDefault(c => c.Email == etyEmail.Text.ToLower()) != null)
                {
                    Task<ServiceResult> isCreated = CustomerService.Modify(
                        new(etyName.Text, etySurname.Text, etyCity.Text, etyEmail.Text, etyComents.Text, chbIsVip.IsChecked)
                    );
                    if (isCreated.Result.IsSuccess)
                    {
                        await DisplayAlert("Cliente actualizado.", "El cliente se ha actualizado con éxito.", "Ok");
                        InitializeCustomers();
                    }
                    else await DisplayAlert("Ha ocurrido un error.", isCreated.Result.ErrorMessage, "Ok");
                }
                else await DisplayAlert("Cliente no existente.", "El cliente que se trata de modificar no existe o se ha modificado el Email.", "Ok");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error inesperado.", $"Ha ocurrido un error inesperado : {ex.Message}", "Ok");
        }
    }

    /// <summary>
    /// Delete a user if exist.
    /// </summary>
    /// <param name="sender">The source of the event, typically the control that was clicked.</param>
    /// <param name="e">An <see cref="EventArgs"/> instance containing the event data.</param>
    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        try
        {
            List<Customer> customers = CustomerService.GetAll().Result.Data?.ToList() ?? new List<Customer>();
            if (!string.IsNullOrEmpty(etyEmail.Text))
            {
                if (customers.FirstOrDefault(c => c.Email == etyEmail.Text.ToLower()) != null)
                {
                    Task<ServiceResult> isCreated = CustomerService.Delete(etyEmail.Text.ToLower());
                    if (isCreated.Result.IsSuccess)
                    {
                        await DisplayAlert("Cliente eliminado.", "El cliente se ha eliminado con éxito.", "Ok");
                        InitializeCustomers();
                    }
                    else await DisplayAlert("Ha ocurrido un error.", isCreated.Result.ErrorMessage, "Ok");
                }
                else await DisplayAlert("Cliente no existente.", "El cliente que se trata de eliminar no existe.", "Ok");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error inesperado.", $"Ha ocurrido un error inesperado : {ex.Message}", "Ok");
        }
    }

    /// <summary>
    /// Clear the data from the entries.
    /// </summary>
    /// <param name="sender">The source of the event, typically the control that was clicked.</param>
    /// <param name="e">An <see cref="EventArgs"/> instance containing the event data.</param>
    private void OnClearClicked(object sender, EventArgs e)
    {
        etyName.Text = string.Empty;
        etySurname.Text = string.Empty;
        etyCity.Text = string.Empty;
        etyEmail.Text = string.Empty;
        etyComents.Text = string.Empty;
        chbIsVip.IsChecked = false;
    }

    /// <summary>
    /// Checks if each required param is null or empty and notify if they are.
    /// </summary>
    /// <returns>bool, true if the params are correct, false otherwise.</returns>
    private async Task<bool> CheckParamsAndInform()
    {
        bool areCorrect = true;
        if (string.IsNullOrEmpty(etyName.Text))
        {
            await DisplayAlert("Nombre vacío", "El nombre está vacío.", "Ok");
            areCorrect = false;
        }
        if (string.IsNullOrEmpty(etySurname.Text))
        {
            await DisplayAlert("Apellidos vacios.", "Los apellidos están vacios.", "Ok");
            areCorrect = false;
        }
        if (string.IsNullOrEmpty(etyCity.Text))
        {
            await DisplayAlert("Ciudad vacía.", "El nombre de la ciudad está vacío..", "Ok");
            areCorrect = false;
        }
        if (string.IsNullOrEmpty(etyEmail.Text))
        {
            await DisplayAlert("Email vacío.", "El email está vacío.", "Ok");
            areCorrect = false;
        }
        return areCorrect;
    }
}