using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lab4v2.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lab4v2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetails : ContentPage
    {
        public ProductDetails( Product _prod )
        {
            InitializeComponent();

            productDetails.BindingContext = _prod;
        }
    }
}