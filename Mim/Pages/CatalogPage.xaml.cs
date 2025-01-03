using Mim.Data;
using Mim.DataBase;
using Mim.Tools;
using System.ComponentModel;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace Mim.Pages
{

    public partial class CatalogPage : ContentPage, INotifyPropertyChanged
    {
        private List<Product> products;
        private Provider selectedProvider;
        private Category selectedCategory;
        private string selectedSortCostType;
        private Data.Size selelctedSize;
        private List<Provider> providers;
        private List<Category> categories;
        private List<string> sortCostType;
        private List<Data.Size> sizes;
        private string searchText = "";
        private bool buttonVisibility = false;

        public List<Product> Products { get => products;
            set { products = value;
                Signal(); } }

        public List<Provider> Providers { get => providers; 
            set { providers = value; 
                Signal(); } }
        public List<Category> Categories { get => categories;
            set { categories = value;
                Signal(); } }
        public List<string> SortCostType { get => sortCostType; 
            set { sortCostType = value; 
                Signal(); } }
        public List<Data.Size> Sizes { get => sizes;
            set { sizes = value; 
                Signal(); } }

        public Provider SelectedProvider { get => selectedProvider; 
            set { selectedProvider = value;
                Search();
                Signal();
            } }

        public string SearchText { get => searchText; 
            set { searchText = value; 
                Signal();
                Search();
            }
        }

        public Category SelectedCategory { get => selectedCategory;
            set {selectedCategory = value;
                Search();
                Signal();
            } }

        public string SelectedSortCostType { get => selectedSortCostType;
            set { selectedSortCostType = value; 
                Search();
                Signal();
            } }

        
        public Data.Size SelectedSize { get => selelctedSize; 
            set { selelctedSize = value;
                Search();
                Signal();
            } }

        public bool ButtonVisibility { get => buttonVisibility;
            set { buttonVisibility = value; 
                Signal(); } }

        public CatalogPage()
		{
			InitializeComponent();

			
            BindingContext = this;
		}

        private void GetListProduct()
        {
            DB.Instance.GetListProducts();
            Products = DB.Instance.ListProducts;
        }

        public Product SelectedProduct { get; set; }


        public  void GetLists()
		{
             DB.Instance.GetListProducts();
             DB.Instance.GetListCategories();
             DB.Instance.GetListProviders();
             DB.Instance.GetListSize();

            Providers = [new Provider { Name = "Все производители" }, .. DB.Instance.ListProviders];
            Categories = [new Category { Title = "Все категории" }, .. DB.Instance.ListCategories];
            SortCostType = new List<string>() { "По возрастанию", "По убыванию" };
            Sizes = [new Data.Size { Title = "Все размеры" }, .. DB.Instance.ListSize];
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void Signal([CallerMemberName] string prop = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));


        private async void AddCart(object sender, EventArgs e)
        {
            if(SelectedProduct != null)
            {
                var answer = await Client.HttpClient.PostAsync($"Storages/{SelectedProduct.Id}?idstatus=2", null);

                if (!answer.IsSuccessStatusCode)
                {
                    await DisplayAlert("Сообщение", answer.Content.ReadAsStringAsync().Result, "Ок");
                }
            }
            await DisplayAlert("Сообщение", "Товар добавлен в корзину", "Ок");

        }

        private async void AddWishList(object sender, EventArgs e)
        {
            if (SelectedProduct != null)
            {
                var answer = await Client.HttpClient.PostAsync($"Storages/{SelectedProduct.Id}?idstatus=1", null);

                if (!answer.IsSuccessStatusCode)
                {
                    await DisplayAlert("Сообщение", answer.Content.ReadAsStringAsync().Result, "Ок");
                }
            }
            await DisplayAlert("Сообщение", "Товар добавлен в избранное", "Ок");
        }

        private void Create(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("EditProductPage?id=0");
        }

        private void Update(object sender, EventArgs e)
        {
            if(SelectedProduct != null)
                Shell.Current.GoToAsync($"EditProductPage?id={SelectedProduct.Id}");
        }

        private void Delete(object sender, EventArgs e)
        {
            if (SelectedProduct != null)
            {
                var answer = Client.HttpClient.DeleteAsync($"Products?id={SelectedProduct.Id}").Result;
                if (!answer.IsSuccessStatusCode)
                {
                    DisplayAlert("Сообщение", answer.Content.ReadAsStringAsync().Result, "Ок");
                    return;
                }
                GetListProduct();
            }

        }
        

        private void Search()
        {
            GetListProduct();



            if (SelectedCategory != null && SelectedSize != null && SelectedProvider != null && SelectedSortCostType != null)
            {

                if (SelectedSize.Title != "Все размеры")
                    Products = Products.Where(s => s.SizeId == SelectedSize.Id).ToList();

                if (SelectedCategory.Title != "Все категории")
                    Products = Products.Where(s => s.CategoryId == SelectedCategory.Id).ToList();

                if (SelectedSortCostType == "По возрастанию")
                    Products = Products.OrderBy(s => s.Cost).ToList();
                else
                {
                    Products = Products.OrderByDescending(s => s.Cost).ToList();
                }
                if (SelectedProvider.Name != "Все производители")
                    Products = Products.Where(s => s.ProviderId == SelectedProvider.Id).ToList();
            }
            if (!string.IsNullOrWhiteSpace(SearchText))
                Products = Products.Where(s => s.Title.Contains(SearchText)).ToList();



        }

        protected override void OnAppearing()
        {
            GetLists();

            SelectedCategory = Categories.First();
            SelectedProvider = Providers.First();
            SelectedSortCostType = SortCostType.First();
            SelectedSize = Sizes.First();

            if(ClientData.Role == "admin")
            {
                ButtonVisibility = true;
            }
            else
            {
                ButtonVisibility = false;
            }

        }

        private void About(object sender, EventArgs e)
        {
            if(SelectedProduct != null)
                Shell.Current.GoToAsync($"ItemPage?id={SelectedProduct.Id}");
        }
    }
}