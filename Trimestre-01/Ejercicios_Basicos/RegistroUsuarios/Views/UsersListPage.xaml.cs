using RegistroUsuarios.Interfaces;
using RegistroUsuarios.Model;
using RegistroUsuarios.Repository;
using RegistroUsuarios.Service;

namespace RegistroUsuarios.Views;

public partial class UsersListPage : ContentPage
{
    private static readonly IGenerciService<Register, string> RegisterService =
        new RegistersService(new RegistersJsonRepository());

	public UsersListPage()
	{
		InitializeComponent();
        ShowRegisters();
	}

    /// <summary>
    /// Show all registers from the repository.
    /// </summary>
    private void ShowRegisters()
    {
        Result<IEnumerable<Register>?> result = RegisterService.GetAll();
        if (result.IsSuccess)
        {
            IEnumerable<Register>? registers = result.Value;
            if (registers != null)
            {
                clvCustomers.ItemsSource = registers;
            }
            else DisplayAlert("Sin Registros", "Aún no se han creado registros.", "Ok");
        }
        else DisplayAlert("Sin Registros", "Aún no se han creado registros o no se ha podido acceder a ellos.", "Ok");
    }
}