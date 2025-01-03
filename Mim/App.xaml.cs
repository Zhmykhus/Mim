using Mim.Pages;

namespace Mim
{

    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
