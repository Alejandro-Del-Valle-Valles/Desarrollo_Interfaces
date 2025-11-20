using AgendaBienestar.Interfaces;
using AgendaBienestar.Model;
using AgendaBienestar.Repository;
using AgendaBienestar.Service;

namespace AgendaBienestar.Controllers;

public partial class DailyRegistrationPage : ContentPage
{
    private static readonly IGenericService<Register, Guid> RegisterService =
        new RegisterService(new RegisterJsonRepository());
	public DailyRegistrationPage()
	{
		InitializeComponent();
	}

    /// <summary>
    /// Update the Progress Bar when the Slider is changed.
    /// </summary>
    /// <param name="sneder"></param>
    /// <param name="e"></param>
    public void OnSliderChanged(object sneder, ValueChangedEventArgs e)
    {
        double progress = e.NewValue / 10.0;

        UpdateProgressBar(progress);
    }

    /// <summary>
    /// Update the Progress Bar with new Value.
    /// </summary>
    /// <param name="value">double new Value.</param>
    private void UpdateProgressBar(double value)
    {
        if (pbActivity == null) return;

		MainThread.BeginInvokeOnMainThread(async () =>
        {
            await pbActivity.ProgressTo(value, 100, Easing.Linear);
        });
    }

    /// <summary>
    /// When the Save Button is clicked, check that the comment isn't null and save the data.
    /// Then reset the fields.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(edComment.Text))
        {
            Result result = RegisterService.Create(CreateRegister());
            if (result.IsSuccess)
            {
                await DisplayAlert("Registro Guardado", "El registro se ha guardado correctamente.",
                    "Ok");
                ClearData();
            }
            else 
                await DisplayAlert("Registro No Guardado", $"El registro no se ha podido guardar debido al siguiente error: {result.Exception?.Message}",
                "Ok");
        }
        else
        {
            await DisplayAlert("Faltan Datos", "Debes introducir todos los datos para guardar el registro.",
                "Ok");
        }
    }

    /// <summary>
    /// Clear all data fields.
    /// </summary>
    private void ClearData()
    {
        dpDate.Date = DateTime.Now;
        edComment.Text = string.Empty;
        sdActivity.Value = 5;
        spEnergy.Value = 1;
    }

    /// <summary>
    /// Create a Register from the Data of the fields.
    /// </summary>
    /// <returns>Regsiter</returns>
    private Register CreateRegister() => new (dpDate.Date, edComment.Text.Trim(), 
            (int)Math.Round(sdActivity.Value), (int)Math.Round(spEnergy.Value));
    
}