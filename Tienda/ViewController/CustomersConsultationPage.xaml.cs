using System.Collections.ObjectModel;
using Tienda.DAO;
using Tienda.Service;
using Tienda.Model;
using Tienda.Interfaces;
namespace Tienda.ViewController;

public partial class CustomersConsultationPage : ContentPage
{
    private static readonly IGenericService<Customer, string> CustomerService =
        new CustomerService(new CustomersTextFileDao());
    private ObservableCollection<Customer> allCustomers = new();
    private bool? IsVipSelected = null;
	public CustomersConsultationPage()
	{
		InitializeComponent();
        InitializeData();
	}

    /// <summary>
    /// Initialize the customers data to show it and the filters.
    /// </summary>
    private void InitializeData()
    {
        allCustomers =
            new ObservableCollection<Customer>(CustomerService.GetAll().Result.Data ??
                                               new ObservableCollection<Customer>());
        clvCustomers.ItemsSource = allCustomers;

        var cities = allCustomers
            .Select(c => c.City)
            .Distinct()
            .OrderBy(c => c)
            .ToList();

        cities.Insert(0, "Todas");
        pkrCity.ItemsSource = cities;

        string[] vipOptions = { "Todos", "VIP", "No VIP" };
        pkrVip.ItemsSource = vipOptions.ToList();

        pkrCity.SelectedIndexChanged += (s, e) => ApplyFilters();
        pkrVip.SelectedIndexChanged += (s, e) => ApplyFilters();
    }

    /// <summary>
    /// Apply filters to the data.
    /// </summary>
    private void ApplyFilters()
    {
        if (allCustomers == null || allCustomers.Count == 0)
            return;

        string selectedCity = pkrCity.SelectedItem as string;
        string selectedVip = pkrVip.SelectedItem as string;

        IEnumerable<Customer> filtered = allCustomers;

        if (!string.IsNullOrEmpty(selectedCity) && selectedCity != "Todas")
            filtered = filtered.Where(c => c.City == selectedCity);
        

        if (!string.IsNullOrEmpty(selectedVip) && selectedVip != "Todos")
        {
            bool isVip = selectedVip == "VIP";
            filtered = filtered.Where(c => c.IsVip == isVip);
        }

        clvCustomers.ItemsSource = new ObservableCollection<Customer>(filtered);
    }

}