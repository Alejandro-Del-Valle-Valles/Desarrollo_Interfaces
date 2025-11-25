using RegistroUsuarios.Interfaces;
using RegistroUsuarios.Model;
using RegistroUsuarios.Repository;
using RegistroUsuarios.Service;

namespace RegistroUsuarios.Views;

public partial class RegisterPage : ContentPage
{
    private static readonly IGenerciService<Register, string> RegisterService = 
        new RegistersService(new RegistersJsonRepository());
	public RegisterPage()
	{
		InitializeComponent();
	}

    /// <summary>
    /// When Save button is clicked, this save the new register if someone with the entered email isn't registered yet.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnSaveButton(object sender, EventArgs e)
    {
        if (CheckData())
        {
            try
            {
                Register register = new(NameEntry.Text, EmailEntry.Text, PasswordEntry.Text);
                Result result = RegisterService.Create(register);
                if (result.IsSuccess)
                {
                    DisplayAlert("Registrado", "La persona se ha registrado con éxito.", "Ok");
                    ClearData();
                }
                else DisplayAlert("Algo fue mal", $"La persona no se ha podio registrar: {result.Exception?.Message ?? "Error desconocido"}.", "Ok");
            }
            catch(Exception ex)
            {
                DisplayAlert("Error inesperado", $"Ha ocurrido un error inesperado: {ex.Message}", "Ok");
            }
        }
    }

    /// <summary>
    /// Clear the data fields.
    /// </summary>
    private void ClearData()
    {
        NameEntry.Text = string.Empty;
        EmailEntry.Text = string.Empty;
        PasswordEntry.Text = string.Empty;
    }

    /// <summary>
    /// Check that the data is correct and not empty.
    /// Also shows an Alert with the wrong data.
    /// </summary>
    /// <returns>boo, true if is correct, false otherwise.</returns>
    private bool CheckData()
    {
        bool isCorrect = true;
        string name = NameEntry.Text.Trim();
        string email = EmailEntry.Text.Trim();
        string password = PasswordEntry.Text.Trim();

        if (string.IsNullOrEmpty(name))
        {
            DisplayAlert("Nombre vacío", "Es necesario que introduzca un nombre.", "Ok");
            isCorrect = false;
        }
        if (string.IsNullOrWhiteSpace(email)
            || !email.Contains('@') || !email.Contains('.'))
        {
            DisplayAlert("Email vacío", "Es necesario que introduzca un email (Con dominio) que no esté registrado ya.", "Ok");
            isCorrect = false;
        }
        if (string.IsNullOrEmpty(password))
        {
            DisplayAlert("Contraseña vacía", "Es necesario que introduzca una contraseña (Trate que sea segura).", "Ok");
            isCorrect = false;
        }

        return isCorrect;
    }
}