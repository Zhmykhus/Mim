using Mim.Data;
using Mim.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Mim.DataBase
{
    public class DB
    {
        private static DB instance;

        public static DB Instance { get { 
            if(instance == null)    
                    instance = new DB();
            return instance;
            
            } } 

        public List<Category> ListCategories { get; set; }
        public List<Product> ListProducts { get; set; }
        public List<Order> ListOrders { get; set; }
        public List<Order> LisAllOrders { get; set; }
        public List<Order> ListPurchases { get; set; }
        public List<Provider> ListProviders { get; set; }
        public List<OrderStatus> ListOrderStatuses { get; set; }
        public List<Promocode> ListPromocodes { get; set; }
        public List<Review> ListReviews { get; set; }
        public List<Data.Size> ListSize { get; set; }
        public List<User> ListUsers { get; set; }
       
        public void GetListCategories()
        {
            var answer =  Client.HttpClient.GetFromJsonAsync<List<Category>>("Categories").Result;

            ListCategories = answer.ToList();
        }
        public void GetListUsers()
        {
            var answer = Client.HttpClient.GetFromJsonAsync<List<User>>("Uses").Result;

            ListUsers = answer.ToList();
        }

        public void GetListProducts()
        {
            var answer =  Client.HttpClient.GetFromJsonAsync<List<Product>>("Products").Result;

            ListProducts = answer.ToList();
        }
        public void GetListOrders()
        {
            var answer =  Client.HttpClient.GetFromJsonAsync<List<Order>>("Orders").Result;

            ListOrders = answer.ToList();
        }
        public void GetListAllOrders()
        {
            var answer = Client.HttpClient.GetFromJsonAsync<List<Order>>("Orders/GetAll").Result;

            LisAllOrders = answer.ToList();
        }
        public void GetListProviders()
        {
            var answer =  Client.HttpClient.GetFromJsonAsync<List<Provider>>("Providers").Result;

            ListProviders = answer.ToList();
        }
        public void GetListOrderStatuses()
        {
            var answer =  Client.HttpClient.GetFromJsonAsync<List<OrderStatus>>("OrderStatuses").Result;

            ListOrderStatuses = answer.ToList();
        }
        public void GetListPromocodes()
        {
            var answer =  Client.HttpClient.GetFromJsonAsync<List<Promocode>>("Promocodes").Result;

            ListPromocodes = answer.ToList();
        }public void GetListReviews(int id)
        {
            var answer =  Client.HttpClient.GetFromJsonAsync<List<Review>>($"Reviews/{id}").Result;

            ListReviews = answer.ToList();
        }
        public void GetListSize()
        {
            var answer =  Client.HttpClient.GetFromJsonAsync<List<Data.Size>>("Sizes").Result;

            ListSize = answer.ToList();
        }
        public void GetListPurchases()
        {
            var answer = Client.HttpClient.GetFromJsonAsync<List<Order>>("Orders/Purchases").Result;

            ListPurchases = answer.ToList();
        }



    }
}
