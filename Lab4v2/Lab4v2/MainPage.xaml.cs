using Lab4v2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Lab4v2
{
    public partial class MainPage : ContentPage
    {
        Cart newCart = new Cart();
        Wishlist newWishlist = new Wishlist();
        ObservableCollection<Product> productList = new ObservableCollection<Product>
        {
            new Product(){Name="Stuffed Doll",Description="Bedtime Originals Twinkle Toes Pink Panda", Price=11.77M,Category="Toys", Image="https://www.pngmart.com/files/4/Plush-Toy-PNG-Image.png"},
            new Product(){Name="Kids Ball Pit",Description="Kids Ball Pit Large Pop Up Toddler Ball Pits Tent for Toddlers Girls Boys for Indoor Outdoor Baby Playpen Zipper Storage Bag, Balls Not Included", Price=13.99M,Category="Toys", Image="https://upload.wikimedia.org/wikipedia/commons/thumb/a/a3/3Year_old_girl_ball_pit_01473.png/1200px-3Year_old_girl_ball_pit_01473.png"},
            new Product(){Name="CeraVe Moisturizing",Description="CeraVe Moisturizing Cream | Body and Face Moisturizer for Dry Skin | Body Cream with Hyaluronic Acid and Ceramides | Normal | Fragrance Free | 19 Oz", Price=20.53M,Category="Skin Care", Image="https://nuelacosmetics.com/wp-content/uploads/2020/03/CeraVe-Moisturizing-Cream-with-Pump-600x600.png"},
            new Product(){Name="Vitamin C Serum",Description="TruSkin Vitamin C Serum for Face, Anti Aging Serum with Hyaluronic Acid, Vitamin E, Organic Aloe Vera and Jojoba Oil, Hydrating & Brightening Serum for Dark Spots, Fine Lines and Wrinkles, 1 fl oz", Price=19.99M,Category="Skin Care", Image="https://cdn.shopify.com/s/files/1/0270/2612/8939/products/vitamin-c-serum_e8e18e76-2db5-47e5-a2a9-880c61bd46da.png?v=1643178788"},
            new Product(){Name="Acer Laptop",Description="Acer Predator Helios 300 PH315-54-760S Gaming Laptop | Intel i7-11800H | NVIDIA GeForce RTX 3060 Laptop GPU | 15.6 Full HD 144Hz 3ms IPS Display | 16GB DDR4 | 512GB SSD | Killer WiFi 6 | RGB Keyboard", Price=1269,Category="Electronics", Image="https://static.acer.com/up/Resource/Acer/Laptops/Predator_Helios_300/Images/20210522/Predator-Helios-300-PH-315-54-Bl-Bk-modelmain.png"},
            new Product(){Name="Headphones",Description="Anker Soundcore Life Q20 Hybrid Active Noise Cancelling Headphones, Wireless Over Ear Bluetooth Headphones, 40H Playtime, Hi-Res Audio, Deep Bass, Memory Foam Ear Cups, for Travel, Home Office", Price=54.99M,Category="Electronics", Image="https://www.pngall.com/wp-content/uploads/4/Headphone-Transparent-PNG.png"},
            new Product(){Name="Chair",Description="Macedonia Mid Century Modern Tufted Back Fabric Recliner (Light Grey Tweed)", Price=300.99M,Category="Furniture", Image="https://www.pngplay.com/wp-content/uploads/2/Modern-Chair-PNG-HD-Quality.png"},
            new Product(){Name="Bed",Description="Home Life Premiere Classics Cloth Light Grey Silver Linen 51 Tall Headboard Platform Bed with Slats Queen- Complete Bed 5 Year Warranty Included-007", Price=362.99M,Category="Furniture", Image="https://www.pngall.com/wp-content/uploads/2/Double-Bed.png"},
        };

        public MainPage()
        {
            InitializeComponent();

            LvProducts.ItemsSource = productList = GetAllProducts();
            List<Product> noDupes = productList.GroupBy(x => x.Category).Select(y => y.FirstOrDefault()).ToList();
            ObservableCollection<Product> noDupesOp = new ObservableCollection<Product>(noDupes);
            noDupesOp.Insert(0, new Product() { Name = "", Description = "", Price = 0, Category = "All", Image = "" });
            myPicker.ItemsSource = noDupesOp;
        }

        ObservableCollection<Product> GetAllProducts(string _usersearchword = null)
        {
            if (string.IsNullOrWhiteSpace(_usersearchword))
            {
                return productList;
            }
            else
            {
                return new ObservableCollection<Product>(productList.Where(prod => prod.Name.ToLower().StartsWith(_usersearchword.ToLower())).ToList());
            }
        }

        ObservableCollection<Product> GetByCategory(Product chosedCate)
        {
            if (string.IsNullOrWhiteSpace(chosedCate.Category))
            {
                return productList;
            }
            else
            {
                return new ObservableCollection<Product>(productList.Where(prod => prod.Category.ToLower().StartsWith(chosedCate.Category.ToLower())).ToList());
            }
        }

        private void searchProducts(object sender, TextChangedEventArgs e)
        {
            myPicker.SelectedIndex = 0;
            LvProducts.ItemsSource = GetAllProducts(searchword.Text);
            if (GetAllProducts(searchword.Text).Count == 0)
            {
                LvProducts.ItemsSource = new List<Product>() { new Product() { Name = "No Result Found", Image = "https://www.kanan.co/static/data_not_found-8ba65c1e24ea7bd4b50e0f69a4bef3f9.png" } };
            }
        }

        private void searchProducts(object sender, EventArgs e)
        {
            myPicker.SelectedIndex = 0;
            LvProducts.ItemsSource = GetAllProducts(searchword.Text);
            if (GetAllProducts(searchword.Text).Count == 0)
            {
                LvProducts.ItemsSource = new List<Product>() { new Product() { Name = "No Result Found", Image = "https://www.kanan.co/static/data_not_found-8ba65c1e24ea7bd4b50e0f69a4bef3f9.png" } };
            }
        }

        private void pickerControlEvent(object sender, EventArgs e)
        {
            if (myPicker.SelectedItem.ToString().ToLower() == "all")
            {
                LvProducts.ItemsSource = productList;
            }
            else
            {
                Product targetone = productList.FirstOrDefault(prod => prod.Category.ToLower() == (myPicker.SelectedItem.ToString().ToLower()));
                LvProducts.ItemsSource = GetByCategory(targetone);
            }
        }

        private void loadAllData(object sender, EventArgs e)
        {
            myPicker.SelectedIndex = 0;
            LvProducts.EndRefresh();
            LvProducts.ItemsSource = GetAllProducts(searchword.Text);
        }

        private void itemSelectedEvent(object sender, SelectedItemChangedEventArgs e)
        {
            Navigation.PushAsync(new ProductDetails(e.SelectedItem as Product));
        }

        private void likeEvent(object sender, EventArgs e)
        {
            newWishlist.Add((sender as MenuItem).CommandParameter as Product);
            DisplayAlert("Liked!", "Happy shopping", "OK");
        }

        private async void deleteEvent(object sender, EventArgs e)
        {
            bool result = await DisplayAlert("Delete Product", $"You will delete a product?", "Delete", "Cancel");
            if (result) productList.Remove((sender as MenuItem).CommandParameter as Product);
        }

        private void addToCartEvent(object sender, EventArgs e)
        {
            newCart.Add((sender as MenuItem).CommandParameter as Product);
            DisplayAlert("Added To Cart !", "Happy shopping", "OK");
        }

        private void toCartEvent(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CartPage(newCart));
        }
    }
}
