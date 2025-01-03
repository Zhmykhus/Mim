using Mim.Data;
using Mim.DataBase;
using Mim.Tools;
using System.ComponentModel;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace Mim.Pages
{

	public partial class BucketPage : ContentPage, INotifyPropertyChanged
	{
        private List<Product> products;
        private decimal costAll;
        private Product selectedProduct;

        public List<Product> Products { get => products;
            set { products = value;
                Signal(); } }
        public string Promocode { get; set; }
        public decimal CostAll { get => costAll;
            set { costAll = value;
                Signal(); } }
        public Product SelectedProduct { get => selectedProduct; 
            set { selectedProduct = value; 
                Signal(); } }

        public BucketPage()
		{
			InitializeComponent();


            BindingContext = this;
		}
        public event PropertyChangedEventHandler? PropertyChanged;
        public void Signal([CallerMemberName] string prop = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        private async void Order(object sender, EventArgs e)
        {
            foreach(var product in Products)
            {
                Order order = new Order()
                {
                    UserId = 0,
                    ProductId = product.Id,
                    Promocode = product.Promocode,
                    Price= 0,
                    OrderStatusId = 0
                };

                var answer = await Client.HttpClient.PostAsJsonAsync("Orders", order);


                if (!answer.IsSuccessStatusCode)
                {
                    DisplayAlert("Cообщение", answer.Content.ReadAsStringAsync().Result, "Ок");
                    break;
                }
                await Client.HttpClient.DeleteAsync($"Storages/{product.Id}?idstatus=2");
            }
            DisplayAlert("Cообщение", "Заказ принят", "Ок");

            var answer1 = Client.HttpClient.GetFromJsonAsync<List<Product>>("Storages/Cart").Result;
            Products = answer1.ToList();

            Signal(nameof(Products));
        }

        private async void AddPromo(object sender, EventArgs e)
        {
            Promocode promocode;

            if(SelectedProduct != null)
            {
                var answer = await Client.HttpClient.GetAsync($"Promocodes/{SelectedProduct.Promocode}");

                if(answer.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    promocode = answer.Content.ReadFromJsonAsync<Promocode>().Result;

                    SelectedProduct.Cost = SelectedProduct.Cost - (SelectedProduct.Cost  * promocode.Size / 100 );

                   

                    List<Product> products = [.. Products];

                    Products = new List<Product>();

                    Products = [.. products];

                    Signal(nameof(Products));
                    Signal(nameof(SelectedProduct));

                    CalcAllCost();
                }
            }
            
        }
        protected override void OnAppearing()
        {
            
            var answer = Client.HttpClient.GetFromJsonAsync<List<Product>>("Storages/Cart").Result;
            Products = answer.ToList();
            CalcAllCost();
        }
        private void CalcAllCost()
        {
            CostAll = 0;
            if(Products !=  null) 
            CostAll = Products.Sum(x => x.Cost);    
        }

        private async void Delete(object sender, EventArgs e)
        {
            if(SelectedProduct != null)
            {
                await Client.HttpClient.DeleteAsync($"Storages/{SelectedProduct.Id}?idstatus=2");
            }
            var answer1 = Client.HttpClient.GetFromJsonAsync<List<Product>>("Storages/Cart").Result;
            Products = answer1.ToList();
            CalcAllCost();
        }
    }
}