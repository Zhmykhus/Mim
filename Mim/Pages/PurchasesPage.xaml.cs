using Mim.Data;
using Mim.DataBase;
using Mim.Tools;
using System.ComponentModel;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace Mim.Pages;

public partial class PurchasesPage : ContentPage, INotifyPropertyChanged
{
    private List<Order> orders;

    public List<Order> Orders { get => orders;
        set { orders = value;
            Signal();
        } }

    public Order SelectedOrder { get; set; }
   

    public PurchasesPage()
	{
		InitializeComponent();

		BindingContext = this;
	}

    protected override void OnAppearing()
    {
        DB.Instance.GetListOrderStatuses();
        DB.Instance.GetListPurchases();

        Orders = DB.Instance.ListPurchases;

        Signal(nameof(Orders));
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public void Signal([CallerMemberName] string prop = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

    private async void Review(object sender, EventArgs e)
    {
        if (SelectedOrder != null)
        {

           await  Shell.Current.GoToAsync($"ReviewPage?id={SelectedOrder.Id}");  
        }
        else
            await DisplayAlert("Сообщение", "Необходимо выбрать заказ", "Ок");
    }
}