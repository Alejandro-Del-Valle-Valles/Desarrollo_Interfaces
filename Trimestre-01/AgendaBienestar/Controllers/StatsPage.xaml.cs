using AgendaBienestar.Extensions;
using AgendaBienestar.Interfaces;
using AgendaBienestar.Model;
using AgendaBienestar.Repository;
using AgendaBienestar.Service;

namespace AgendaBienestar.Controllers;

public partial class StatsPage : ContentPage
{
    private static readonly IGenericService<Register, Guid> RegisterService =
        new RegisterService(new RegisterJsonRepository());
	public StatsPage()
	{
		InitializeComponent();
        ShowAverageActivityOfWeek();
        ShowAvergaEnergyOfWeek();
        ShowProgressWeek();
	}

    /// <summary>
    /// Show the average Physical Activity of the last week.
    /// </summary>
    private void ShowAverageActivityOfWeek()
    {
        AverageActivityLabel.Text = $"Promedio actividad física: {CalculateAverageActivityWeek():F2}";
    }

    /// <summary>
    /// Show the Average Energy of the last week.
    /// </summary>
    private void ShowAvergaEnergyOfWeek()
    {
        AverageEnergyLabel.Text = $"Promedio energía: {CalculateAverageEnergyWeek():F2}";
    }

    /// <summary>
    /// Show the general progress of the last week based on the average Activity and Energy.
    /// </summary>
    private void ShowProgressWeek()
    {
        float activity = CalculateAverageActivityWeek();
        float energy = CalculateAverageEnergyWeek();
        float activityPlusEnergy = activity + energy;
        GeneralProgressBar.Progress = activityPlusEnergy <= 0
            ? 0
            : (activityPlusEnergy / 2) / 10.0;
    }

    /// <summary>
    /// Calculate the average Activity of the last week.
    /// </summary>
    /// <returns>float with the average activity of the last week.</returns>
    private float CalculateAverageActivityWeek()
    {
        List<Register>? registers = RegisterService.GetAll().Data?
            .Where(r => r.Date.IsInLastWeek()).ToList();
        float averageActivity = 0;
        if (registers != null)
        {
            registers.ForEach(r => averageActivity += r.ActivityLevel);
            averageActivity = averageActivity / registers.Count();
        }

        return averageActivity;
    }

    /// <summary>
    /// Calculate the average energy of the last week.
    /// </summary>
    /// <returns>float with the average energy of the last week.</returns>
    private float CalculateAverageEnergyWeek()
    {
        List<Register>? registers = RegisterService.GetAll().Data?
            .Where(r => r.Date.IsInLastWeek()).ToList();
        float averageEnergy = 0;
        if (registers != null)
        {
            registers.ForEach(r => averageEnergy += r.Energy);
            averageEnergy = averageEnergy / registers.Count();
        }
        return averageEnergy;
    }
}