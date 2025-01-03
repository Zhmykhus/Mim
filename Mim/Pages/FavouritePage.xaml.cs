using Mim.Data;
using Mim.Tools;
using System.ComponentModel;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace Mim.Pages
{

	public partial class FavouritePage : ContentPage, INotifyPropertyChanged
	{
        private List<Product> products;

        public List<Product> Products { get => products;
            set { products = value; 
                Signal(); } }
        public Product SelectedProduct { get; set; } 

		public FavouritePage()
		{
			InitializeComponent();

            BindingContext = this;
		}
        public event PropertyChangedEventHandler? PropertyChanged;
        public void Signal([CallerMemberName] string prop = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        protected override void OnAppearing()
        {
            var answer = Client.HttpClient.GetFromJsonAsync<List<Product>>("Storages/WishList").Result;
            Products = answer.ToList();
        }
        private async void Delete(object sender, EventArgs e)
        {
            if (SelectedProduct != null)
            {
                await Client.HttpClient.DeleteAsync($"Storages/{SelectedProduct.Id}?idstatus=1");
            }
            var answer1 = Client.HttpClient.GetFromJsonAsync<List<Product>>("Storages/WishList").Result;
            Products = answer1.ToList();
        }
    }
}