using mim.DTO;
using Mim.Data;
using Mim.Tools;
using System.ComponentModel;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace Mim.Pages
{

	public partial class ProfilePage : ContentPage, INotifyPropertyChanged
	{
        private bool isVisibleFromCode = false;
        private bool isVisiblePassword = false;

        public DTO.Email Email { get; set; } =new DTO.Email();
        public PasswordReset PasswordReset { get; set; } = new PasswordReset();
        public UpdatePassword UpdatePassword { get; set; } = new UpdatePassword();

        public bool IsVisibleFromCode { get => isVisibleFromCode; 
            set { isVisibleFromCode = value; 
                Signal(); } }
        public bool IsVisiblePassword { get => isVisiblePassword;
            set { isVisiblePassword = value;
                Signal(); } }

        public ProfilePage()
		{
			InitializeComponent();
            IsVisibleFromCode = false;
            IsVisiblePassword = false;
            Signal(nameof(IsVisibleFromCode));
            BindingContext = this;
		}

        private async void Save(object sender, EventArgs e)
        {
            UpdatePassword.Email = Email.UserEmail;
            UpdatePassword.Code = PasswordReset.Code;
            

            var answer = await Client.HttpClient.PostAsJsonAsync("ResetPassword/PerformingReset", UpdatePassword);

            if (!answer.IsSuccessStatusCode)
            {
                await DisplayAlert("Сообщение", answer.Content.ReadAsStringAsync().Result, "Ок");
                return;
            }

            await DisplayAlert("Сообщение", "Пароль изменен", "Ок");
        }

        private async void SendCode(object sender, EventArgs e)
        {
            var answer = await Client.HttpClient.PostAsJsonAsync("ResetPassword", Email);

            if (!answer.IsSuccessStatusCode)
            {
                await DisplayAlert("Сообщение", answer.Content.ReadAsStringAsync().Result, "Ок");
                return;
            }

            IsVisibleFromCode = true; 
        }



        public event PropertyChangedEventHandler? PropertyChanged; 
        public void Signal([CallerMemberName] string prop = null)=>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        private async void AprpovalCode(object sender, EventArgs e)
        {
            PasswordReset.Email = Email.UserEmail;

            var answer = await Client.HttpClient.PostAsJsonAsync("ResetPassword/Confirmation", PasswordReset);

            if (!answer.IsSuccessStatusCode)
            {
                await DisplayAlert("Сообщение", answer.Content.ReadAsStringAsync().Result, "Ок");
                return;
            }

            IsVisiblePassword = true;
        }
    }
}