using Mim.Pages;
using Mim.Tools;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace Mim
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell, INotifyPropertyChanged
    {
        

        public bool TabAllVisibility { get; set; }


        public bool OtherTabVisibiliy { get; set; }



        public AppShell()
        {

            InitializeComponent();
            Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
            Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(CatalogPage), typeof(CatalogPage));
            Routing.RegisterRoute(nameof(EditProductPage), typeof(EditProductPage));
            Routing.RegisterRoute(nameof(ItemPage), typeof(ItemPage));
            Routing.RegisterRoute(nameof(ReviewPage), typeof(ReviewPage));
            Routing.RegisterRoute(nameof(PurchasesPage), typeof(PurchasesPage));
            Routing.RegisterRoute(nameof(EditOrderPage), typeof(EditOrderPage));
            Routing.RegisterRoute(nameof(AllOrdersPage), typeof(AllOrdersPage));

            ClientData.EventHandler += TabVisibilityChanged;

            BindingContext = this;
        }

        private void TabVisibilityChanged(object? sender, string e)
        {
            TabAllVisibility = ClientData.Role == "admin";
            OtherTabVisibiliy = ClientData.Role != "admin";
            Signal(nameof(TabAllVisibility));
            Signal(nameof(OtherTabVisibiliy));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void Signal([CallerMemberName] string prop = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
