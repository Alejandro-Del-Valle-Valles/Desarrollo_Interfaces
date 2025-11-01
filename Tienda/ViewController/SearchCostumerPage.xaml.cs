using System.Collections.ObjectModel;
using Tienda.DAO;
using Tienda.Interfaces;
using Tienda.Model;
using Tienda.Service;

namespace Tienda.ViewController;

public partial class SearchCostumerPage : ContentPage
{
    private static readonly IGenericService<Customer, string> CustomerService = new CustomerService(new CustomersTextFileDao());
    private ObservableCollection<Customer> allCustomers = new();
    public SearchCostumerPage()
    {
        InitializeComponent();
        InitializeCustomers();
    }

    /// <summary>
    /// Initialize the customers data to show it.
    /// </summary>
    private void InitializeCustomers()
    {
        allCustomers = new ObservableCollection<Customer>(CustomerService.GetAll().Result.Data ??
                                                          new ObservableCollection<Customer>());
        clvCustomers.ItemsSource = allCustomers.Order();
        etyFilter.TextChanged += (sender, e) => OnFilterEntered();
        clvCustomers.SelectionChanged += (s, e) => UpdateNavigationButtons();

        UpdateNavigationButtons();
    }

    /// <summary>
    /// Apply text filter to all attributes to search info every time the entry text is changed.
    /// </summary>
    private void OnFilterEntered()
    {
        string text = etyFilter.Text.Trim();
        clvCustomers.ItemsSource = allCustomers
            .Where(c => c.Name.Contains(text, StringComparison.OrdinalIgnoreCase)
                || c.Surname.Contains(text, StringComparison.OrdinalIgnoreCase)
                || c.City.Contains(text, StringComparison.OrdinalIgnoreCase)
                || c.Email.Contains(text, StringComparison.OrdinalIgnoreCase))
            .Order();

        if (text.Equals("Vip", StringComparison.OrdinalIgnoreCase))
            clvCustomers.ItemsSource = allCustomers
                .Where(c => c.IsVip)
                .Order();
        else if (text.Equals("No Vip", StringComparison.OrdinalIgnoreCase))
            clvCustomers.ItemsSource = allCustomers
                .Where(c => !c.IsVip)
                .Order();

        UpdateNavigationButtons();
    }

    /// <summary>
    /// When the previous or next button is clicked, go to the next or the previous customer.
    /// </summary>
    /// <param name="sender">The button that was clicked (btnPrevious or btnNext)</param>
    /// <param name="e">Event arguments</param>
    private void OnPreviousOrNextClicked(object sender, EventArgs e)
    {
        var button = sender as Button;

        var items = clvCustomers.ItemsSource as IEnumerable<Customer>;
        if (items == null || !items.Any())
        {
            return;
        }

        var list = items.ToList();
        var selected = clvCustomers.SelectedItem as Customer;

        int currentIndex = (selected == null) ? -1 : list.IndexOf(selected);

        Customer newItemToSelect = null;

        if (button == btnNext)
        {
            int nextIndex = currentIndex + 1;
            if (nextIndex < list.Count)
            {
                newItemToSelect = list[nextIndex];
            }
        }
        else if (button == btnPrevious)
        {
            int prevIndex = currentIndex - 1;
            if (prevIndex >= 0)
            {
                newItemToSelect = list[prevIndex];
            }
        }

        if (newItemToSelect != null)
        {
            clvCustomers.SelectedItem = newItemToSelect;
            clvCustomers.ScrollTo(newItemToSelect, position: ScrollToPosition.Center, animate: true);
        }
    }

    /// <summary>
    /// Helper method to enable/disable navigation buttons
    /// based on the current selection and list state.
    /// </summary>
    private void UpdateNavigationButtons()
    {
        var items = clvCustomers.ItemsSource as IEnumerable<Customer>;

        if (items == null || !items.Any())
        {
            btnPrevious.IsEnabled = false;
            btnNext.IsEnabled = false;
            return;
        }

        var list = items.ToList();
        var selected = clvCustomers.SelectedItem as Customer;
        int totalCount = list.Count;

        if (selected == null)
        {
            btnPrevious.IsEnabled = false;
            btnNext.IsEnabled = totalCount > 0;
        }
        else
        {
            int currentIndex = list.IndexOf(selected);
            btnPrevious.IsEnabled = currentIndex > 0;
            btnNext.IsEnabled = currentIndex < (totalCount - 1);
        }
    }
}