using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mim.Tools
{
    public class ClientData
    {
        public static string Role
        {
            get => role; set
            {
                role = value;
                EventHandler?.Invoke(Role, null);
            }
        }
        public static int UserId { get; set; }

        public static EventHandler<string> EventHandler;


        private static string role;
    }
}
