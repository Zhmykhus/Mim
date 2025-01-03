
using Mim.Data;
using Mim.DataBase;
using Mim.Tools;
using System.ComponentModel;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace Mim.Pages;
[QueryProperty(nameof(ID), "id")]
public partial class EditProductPage : ContentPage, INotifyPropertyChanged
{
    private string photoPath;
    private int iD;
    private Product product = new Product();
    private List<Data.Size> sisez;
    private List<Category> ctegories;
    private List<Provider> providers;

    public Product Product { get => product; 
        set { product = value; 
            Signal(); } }
    public int ID { get => iD;
        set { iD = value;
            GetProduct(); } }

    public string PhotoPath { get => photoPath; set { photoPath = value; Signal(); } }
    public List<Data.Size> Sises { get => sisez;
        set { sisez = value;
            Signal(); } }
    public List<Category> Categories { get => ctegories;
        set { ctegories = value; 
            Signal(); } }
    public List<Provider> Providers { get => providers;
        set { providers = value; 
            Signal(); } }


    public Provider SelectedProvider { get; set; } = new Provider();
    public Category SelectedCategory { get; set; } = new Category();
    public Data.Size SelectedSize { get; set; } = new Data.Size();

    public EditProductPage()
	{
		InitializeComponent();

        Categories = DB.Instance.ListCategories;
        Providers = DB.Instance.ListProviders;
        Sises = DB.Instance.ListSize;

		BindingContext = this;
	}
	public event PropertyChangedEventHandler? PropertyChanged; 
	public void Signal([CallerMemberName] string prop = null)=>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

    private void GetProduct()
    {
        if (ID == 0)
            Product = new Product();

        else
        {
            Product = DB.Instance.ListProducts.FirstOrDefault(s => s.Id == ID);
        }
    }

    private  void Save(object sender, EventArgs e)
    {

        Product.ProviderId = SelectedProvider.Id;
        Product.CategoryId = SelectedCategory.Id;
        Product.SizeId = SelectedSize.Id;
        if (Product.Id == 0)
        {
            var answer = Client.HttpClient.PostAsJsonAsync("Products", Product).Result;
            
            if (!answer.IsSuccessStatusCode)
            {
                DisplayAlert("Сообщение", answer.Content.ReadAsStringAsync().Result, "Ок");
                return;
            }
            DisplayAlert("Сообщение", "Товар добавлен", "Ок");
        }
        else
        {
            var answer = Client.HttpClient.PutAsJsonAsync("Products", Product).Result;
            if (!answer.IsSuccessStatusCode)
            {
                DisplayAlert("Сообщение", answer.Content.ReadAsStringAsync().Result, "Ок");
                return;
            }
            DisplayAlert("Сообщение", "Товар обновлен", "Ок");
        }

        
    }
    async Task TakePhotoAsync()
    {
        try
        {
            var photo = await MediaPicker.PickPhotoAsync();
            await LoadPhotoAsync(photo);
            Console.WriteLine($"CapturePhotoAsync COMPLETED: {photoPath}");
        }
        catch (FeatureNotSupportedException fnsEx)
        {
            // Feature is not supported on the device
        }
        catch (PermissionException pEx)
        {
            // Permissions not granted
        }
        catch (Exception ex)
        {
            Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
        }
    }
    async Task LoadPhotoAsync(FileResult photo)
    {
        // canceled
        if (photo == null)
        {
            PhotoPath = null;
            return;
        }
        // save the file into local storage
        var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
        using (var stream = await photo.OpenReadAsync())
        using (var br = new BinaryReader(stream))
        {
            Product.Image = br.ReadBytes((int)stream.Length);
        }

        Signal(nameof(Product));
    }

    private void GetPhoto(object sender, EventArgs e)
    {
        TakePhotoAsync();
    }

    private void Back(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//CatalogPage");
    }
}