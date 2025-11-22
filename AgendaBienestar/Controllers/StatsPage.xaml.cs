using AgendaBienestar.Extensions;
using AgendaBienestar.Interfaces;
using AgendaBienestar.Model;
using AgendaBienestar.Repository;
using AgendaBienestar.Service;

namespace AgendaBienestar.Controllers;

public partial class StatsPage : ContentPage
{
    private static readonly IGenericService<Register, Guid> registerService =
        new RegisterService(new RegisterJsonRepository());
	public StatsPage()
	{
		InitializeComponent();
        ShowAverageActivityOfWeek();
        ShowAvergaEnergyOfWeek();
        ShowProgressWeek();
	}

    private void ShowAverageActivityOfWeek()
    {
        AverageActivityLabel.Text = $"Promedio actividad física: {CalculateAverageActivityWeek():F2}";
    }

    private void ShowAvergaEnergyOfWeek()
    {
        AverageEnergyLabel.Text = $"Promedio energía: {CalculateAverageEnergyWeek():F2}";
    }

    private void ShowProgressWeek()
    {
        float activity = CalculateAverageActivityWeek();
        float energy = CalculateAverageEnergyWeek();
        float activityPlusEnergy = activity + energy;
        GeneralProgressBar.Progress = activityPlusEnergy <= 0
            ? 0
            : (activityPlusEnergy / 2) / 10.0;
    }

    private float CalculateAverageActivityWeek()
    {
        List<Register>? registers = registerService.GetAll().Data?
            .Where(r => r.Date.IsInLastWeek()).ToList();
        float averageActivity = 0;
        if (registers != null)
        {
            registers.ForEach(r => averageActivity += r.ActivityLevel);
            averageActivity = averageActivity / registers.Count();
        }

        return averageActivity;
    }

    private float CalculateAverageEnergyWeek()
    {
        List<Register>? registers = registerService.GetAll().Data?
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