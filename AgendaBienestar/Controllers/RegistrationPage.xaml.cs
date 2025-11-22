using AgendaBienestar.Interfaces;
using AgendaBienestar.Model;
using AgendaBienestar.Repository;
using AgendaBienestar.Service;
using Microsoft.Maui.Controls.Shapes;

namespace AgendaBienestar.Controllers;

public partial class RegistrationPage : ContentPage
{
    private static readonly IGenericService<Register, Guid> RegisterService =
        new RegisterService(new RegisterJsonRepository());
	public RegistrationPage()
	{
		InitializeComponent();
        ShowAllRegisters();
	}

    /// <summary>
    /// Show a Scroll view aith all registers ordered by date from newest to oldest.
    /// </summary>
    private void ShowAllRegisters()
    {
        RegistersView.Children.Clear();
        List<Register>? registers = RegisterService.GetAll().Data?.ToList();
        if (registers != null) registers.ForEach(CreateViewForRegister);
    }

    /// <summary>
    /// Create a view for a register and add it to the VerticaStackLayout.
    /// </summary>
    /// <param name="register">Register to add.</param>
    private void CreateViewForRegister(Register register)
    {
        HorizontalStackLayout layout = new HorizontalStackLayout();
        Button deleteButton = new Button
        {
            Text = "Eliminar",
            BackgroundColor = Color.FromRgb(255, 0, 0),
            Padding = 10
        };
        Label date = new Label
        {
            Text = register.Date.ToString("dd/MM/yyyy"),
            Padding = 10
        };
        Label comment = new Label
        {
            Text = register.Comment,
            Padding = 10
        };
        Label activity = new Label
        {
            Text = $"Actividad Física: {register.ActivityLevel}",
            FontAttributes = FontAttributes.Bold,
            Padding = 10
        };
        Label energy = new Label
        {
            Text = $"Nivel de energía: {register.Energy}",
            FontAttributes = FontAttributes.Bold,
            Padding = 5
        };
        layout.Children.Add(deleteButton);
        layout.Children.Add(date);
        layout.Children.Add(comment);
        layout.Children.Add(activity);
        layout.Children.Add(energy);

        Border border = new Border
        {
            BackgroundColor = Color.FromRgb(230, 230, 230),
            Padding = 10,
            Margin = 10,
            StrokeShape = new RoundRectangle
            {
                CornerRadius = 10
            },
            Stroke = Colors.DarkGray,
            StrokeThickness = 2.5,
            Content = layout
        };

        //Listener to the delete button to delete the register.
        deleteButton.Clicked += (sender, e) =>
        {
            HandleDeleteClicked(register, border);
        };
        RegistersView.Children.Add(border);
    }

    /// <summary>
    /// Handles the deletion request for a register and updates the UI.
    /// </summary>
    /// <param name="registerToDelete">The Register object to delete.</param>
    /// <param name="uiElementToRemove">The Border element representing the register in the UI.</param>
    private void HandleDeleteClicked(Register registerToDelete, Border uiElementToRemove)
    {

        Result deleted = RegisterService.Delete(registerToDelete.Id);

        if (deleted.IsSuccess)
        {
            if (RegistersView.Children.Contains(uiElementToRemove))
            {
                RegistersView.Children.Remove(uiElementToRemove);
            }
            DisplayAlert("Éxito", "El registro ha sido eliminado.", "OK");
        }
        else
        {
            DisplayAlert("Error", "No se pudo eliminar el registro.", "OK");
        }
    }
}