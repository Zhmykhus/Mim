using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Mim.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mim.ViewModels
{
   public partial class LoginPageViewModel : BaseViewModel
   {
        [ObservableProperty]
        private string _userName;

        [ObservableProperty]
        private string _password;



        [ICommand]
        public void Login()
        {
           
        }
   }
}
