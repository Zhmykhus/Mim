using Mim.Data;
using Mim.DataBase;
using Mim.Tools;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Mim.Pages
{
    [QueryProperty(nameof(ID), "id")]
	public partial class ItemPage : ContentPage, INotifyPropertyChanged
	{
        private int iD;
        private Product product;
        private List<Review> reviews;

        public int ID { get => iD; 
            set
            {
                iD = value;
                GetProduct();
            }
        }
        public Product Product { get => product; 
            set { product = value; 
                Signal(); } }

        public List<Review> Reviews { get => reviews; 
            set { reviews = value;
                Signal(); } }

        public ItemPage()
		{
			InitializeComponent();

            BindingContext = this;
		}

        public event PropertyChangedEventHandler? PropertyChanged;
        public void Signal([CallerMemberName] string prop = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        private async void GetProduct()
        {
            Product = DB.Instance.ListProducts.FirstOrDefault(s => s.Id == ID);

            DB.Instance.GetListReviews(ID);
            Reviews = DB.Instance.ListReviews.ToList();


            Signal(nameof(Product));
        }

        

        private async void AddWishList(object sender, EventArgs e)
        {
           
                var answer = await Client.HttpClient.PostAsync($"Storages/{ID}?idstatus=1", null);

                if (!answer.IsSuccessStatusCode)
                {
                    await DisplayAlert("Сообщение", answer.Content.ReadAsStringAsync().Result, "Ок");
                }
           
            await DisplayAlert("Сообщение", "Товар добавлен в избранное", "Ок");
        }

        private async void AddCart(object sender, EventArgs e)
        {
            
                var answer = await Client.HttpClient.PostAsync($"Storages/{ID}?idstatus=2", null);

                if (!answer.IsSuccessStatusCode)
                {
                    await DisplayAlert("Сообщение", answer.Content.ReadAsStringAsync().Result, "Ок");
                }
            
            await DisplayAlert("Сообщение", "Товар добавлен в корзину", "Ок");
        }
    }
}