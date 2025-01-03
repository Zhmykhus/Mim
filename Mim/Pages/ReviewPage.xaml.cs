using Mim.Data;
using Mim.DataBase;
using Mim.Tools;
using System.ComponentModel;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace Mim.Pages;
[QueryProperty(nameof(ID), "id")]
public partial class ReviewPage : ContentPage, INotifyPropertyChanged
{
    private int iD;
    private Order order;

    public int ID { get => iD; set
        { iD = value; 
            GetOrder(); } }
    public string Description { get; set; }
    public int Estimation { get; set; }
    public Order Order { get => order; 
        set { order = value; 
            Signal(); } }

    public ReviewPage()
	{
		InitializeComponent();

		BindingContext = this;
	}

    public event PropertyChangedEventHandler? PropertyChanged;
    public void Signal([CallerMemberName] string prop = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));


    private void GetOrder()
    {
        Order = DB.Instance.ListPurchases.FirstOrDefault(s => s.Id == ID);

        Signal(nameof(Order));
    }

    private async void Review(object sender, EventArgs e)
    {
        if (Order != null)
        {

            Review review = new Review
            {
                Descriptoin = Description,
                Estimation = Estimation,
                ProductId = Order.ProductId,
            };

            var answer = await Client.HttpClient.PostAsJsonAsync("Reviews", review);

            if (!answer.IsSuccessStatusCode)
            {
                await DisplayAlert("Сообщение", answer.Content.ReadAsStringAsync().Result, "Ок");
                return;
            }
            await DisplayAlert("Сообщение", "Спасибо за отзыв", "Ок");

            await Client.HttpClient.PutAsync($"Orders/{Order.Id}", null);
        }
    }
}