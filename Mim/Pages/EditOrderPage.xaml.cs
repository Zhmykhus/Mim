
using Mim.Data;
using Mim.DataBase;
using Mim.Tools;
using System.ComponentModel;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace Mim.Pages;

[QueryProperty(nameof(ID), "id")]
public partial class EditOrderPage : ContentPage, INotifyPropertyChanged
{
    private int iD;
    private List<OrderStatus> orderStatuses;
    private Order order;
    private OrderStatus selectedOrderStatus;

    public int ID { get => iD;
        set { iD = value; 
            GetOrder(); } }

    public Order Order { get => order;
        set { order = value; 
            Signal(); } }

    public List<OrderStatus> OrderStatuses { get => orderStatuses; 
        set { orderStatuses = value; 
            Signal(); } }

    public OrderStatus SelectedOrderStatus { get => selectedOrderStatus; 
        set { selectedOrderStatus = value;
            Signal(); } }

    public EditOrderPage()
	{
		InitializeComponent();

		BindingContext = this;
	}


    private void GetOrder()
    {
        DB.Instance.GetListOrderStatuses();

        Order = DB.Instance.LisAllOrders.FirstOrDefault(s => s.Id == ID);

       
        OrderStatuses = DB.Instance.ListOrderStatuses.ToList();

        SelectedOrderStatus = OrderStatuses.FirstOrDefault(s => s.Id == Order.OrderStatusId);

        Signal(nameof(Order));
        Signal(nameof(OrderStatuses));
        Signal(nameof(SelectedOrderStatus));
    }
    public event PropertyChangedEventHandler? PropertyChanged;
    public void Signal([CallerMemberName] string prop = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

    private async void Save(object sender, EventArgs e)
    {
        if(SelectedOrderStatus != null)
        {
            var answer = await Client.HttpClient.PostAsJsonAsync($"Orders/{Order.Id}/UpdateStatus?idstatus={SelectedOrderStatus.Id}", SelectedOrderStatus.Id);

            if (!answer.IsSuccessStatusCode)
            {
                await DisplayAlert("Сообщение", answer.Content.ReadAsStringAsync().Result, "Ок");
                return;
            }
            await DisplayAlert("Сообщение", "Статус изменен", "Ок");
        }
    }
}