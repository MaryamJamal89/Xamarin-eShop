using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lab4v2.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lab4v2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage : ContentPage
    {
        List<Product> tempCart = new List<Product>();
        ObservableCollection<Product> cartToShow;
        public CartPage(Cart newCart)
        {
            InitializeComponent();
            tempCart.AddRange(newCart);
            cartToShow = new ObservableCollection<Product>(tempCart);
            LvCart.ItemsSource = cartToShow = GetAllProducts();
        }

        ObservableCollection<Product> GetAllProducts(string _usersearchword = null)
        {
            if (string.IsNullOrWhiteSpace(_usersearchword))
            {
                return cartToShow;
            }
            else
            {
                return new ObservableCollection<Product>(cartToShow.Where(prod => prod.Name.ToLower().StartsWith(_usersearchword.ToLower())).ToList());
            }
        }

        private void searchProducts(object sender, TextChangedEventArgs e)
        {
            LvCart.ItemsSource = GetAllProducts(searchword.Text);
            if (GetAllProducts(searchword.Text).Count == 0)
            {
                LvCart.ItemsSource = new List<Product>() { new Product() { Name = "No Result Found", Image = "https://www.kanan.co/static/data_not_found-8ba65c1e24ea7bd4b50e0f69a4bef3f9.png" } };
            }
        }

        private void loadAllData(object sender, EventArgs e)
        {
            LvCart.EndRefresh();
            LvCart.ItemsSource = cartToShow = GetAllProducts();
        }

        private void deleteEvent(object sender, EventArgs e)
        {
            cartToShow.Remove((sender as MenuItem).CommandParameter as Product);
        }

        private void toHomeEvent(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void itemSelectedEvent(object sender, SelectedItemChangedEventArgs e)
        {
            Navigation.PushAsync(new ProductDetails(e.SelectedItem as Product));
        }
    }
}