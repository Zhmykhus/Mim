using Mim.Data;
using Mim.Tools;
using System.Net.Http.Json;

namespace Mim.Pages
{

	public partial class RegistrationPage : ContentPage
	{
		public User User { get; set; } = new User();

		public RegistrationPage()
		{
			InitializeComponent();


			BindingContext = this;
		}

        private async void Save(object sender, EventArgs e)
        {
			var answer = await Client.HttpClient.PostAsJsonAsync("Authorization/Registration", User);

			if(!answer.IsSuccessStatusCode)
			{
				await DisplayAlert("Сообщение", answer.Content.ReadAsStringAsync().Result, "Ок");
				return;
			}
            DisplayAlert("Data has been saved", "Thank you", "Ok");
        }
    }
}