using CommunityToolkit.Maui.Views;
using Mim.Data;
using Mim.DataBase;
using Mim.Tools;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Mim.Pages;

public partial class OrderPage : ContentPage, INotifyPropertyChanged
{
    private List<Order> orders;

    public List<Order> Orders { get => orders;
        set { orders = value;
            Signal(); } }
    public Order SelectedOrder { get; set; }

    public OrderPage()
	{
		InitializeComponent();

        BindingContext = this;
	}
    protected override void OnAppearing()
    {
        DB.Instance.GetListOrderStatuses();
        DB.Instance.GetListOrders();

        Orders = DB.Instance.ListOrders.OrderBy(s => s.OrderStatusId).ToList();
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public void Signal([CallerMemberName] string prop = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

    private void Review(object sender, EventArgs e)
    {
        if(SelectedOrder != null)
        {
            if(SelectedOrder.OrderStatusId != 7)
            {
                DisplayAlert("Сообщение", "Невозможно оценить недоставленый товар", "Ок");
            }
            else
            {
                Shell.Current.GoToAsync($"ReviewPage?id={SelectedOrder.Id}");
            }
        }
    }

   
}