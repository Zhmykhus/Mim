using Mim.Data;
using Mim.DataBase;
using Mim.Tools;
using System.ComponentModel;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace Mim.Pages
{

    public partial class SettingsPage : ContentPage, INotifyPropertyChanged
    {
        private User user;

        public User User { get => user;
            set { user = value; 
                Signal(); } }

        public SettingsPage()
        {
            InitializeComponent();

            BindingContext = this;
        }

        private async void Save(object sender, EventArgs e)
        {
            var answer = await Client.HttpClient.PostAsJsonAsync("Uses", user);

            if(!answer.IsSuccessStatusCode)
            {
                await DisplayAlert("Сообщение", answer.Content.ReadAsStringAsync().Result, "Ок");
                return;
            }

            DisplayAlert("Data has been saved", "Thank you", "Ok");
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void Signal([CallerMemberName] string prop = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        protected override void OnAppearing()
        {
            DB.Instance.GetListUsers();

            User = DB.Instance.ListUsers.FirstOrDefault(s => s.Id == ClientData.UserId);

            Signal(nameof(User));
        }
    }
}