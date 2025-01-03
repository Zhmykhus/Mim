using Microsoft.Maui.Controls;
using Mim.DTO;
using Mim.Tools;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection.Metadata;

namespace Mim.Pages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {

        public string Login { get; set; }   
        public string Password { get; set; }   

        public LoginPage()
        {
            InitializeComponent();


            BindingContext = this;  
        }

        private async void Entry_Button(object sender, EventArgs e)
        {
            AuthDTO authDTO = new AuthDTO() { 
                
                Password = Password,
                Login = Login
            };

           var answer = await Client.HttpClient.PostAsJsonAsync("Authorization", authDTO);

            if(!answer.IsSuccessStatusCode)
            {
                await DisplayAlert("Сообщение", answer.Content.ReadAsStringAsync().Result, "Ок");
                return;
            }

            UserData userData = await answer.Content.ReadFromJsonAsync<UserData>();
            ClientData.Role = userData.Role;
            ClientData.UserId = userData.Id;
            Client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userData.Token);

            Shell.Current.GoToAsync("//CatalogPage");
        }

        private void Registr(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("RegistrationPage");
        }

        private void Recover(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("ProfilePage");
        }


       
    }
}