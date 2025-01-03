using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mim.Tools
{
    public class Client
    {
        private static HttpClient httpClient;

       

        public static HttpClient HttpClient { get { 
            
            
            if(httpClient == null)
                {
                    httpClient = new HttpClient();
                    httpClient.BaseAddress = new Uri("http://192.168.130.188:8080");
                }
            return httpClient;  
            
            } }
    }
}
