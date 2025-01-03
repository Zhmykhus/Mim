using Mim.Data;
using Mim.DataBase;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Mim.Pages;

public partial class AllOrdersPage : ContentPage, INotifyPropertyChanged
{
    private List<Order> orders;

    public List<Order> Orders
    {
        get => orders;
        set
        {
            orders = value;
            Signal();
        }
    }
    public Order SelectedOrder { get; set; }

    public AllOrdersPage()
	{
		InitializeComponent();

		BindingContext = this;
	}
    public event PropertyChangedEventHandler? PropertyChanged;
    public void Signal([CallerMemberName] string prop = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

    private async void Update(object sender, EventArgs e)
    {
        if (SelectedOrder != null)
            await Shell.Current.GoToAsync($"EditOrderPage?id={SelectedOrder.Id}");
    }
    protected override void OnAppearing()
    {
        DB.Instance.GetListOrderStatuses();
            
        DB.Instance.GetListAllOrders();
        Orders = DB.Instance.LisAllOrders;
    }
}