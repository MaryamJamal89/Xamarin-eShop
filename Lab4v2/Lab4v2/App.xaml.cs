using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lab4v2
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
            var targetPage = new MainPage();
            //var titleView = new SearchBar { HeightRequest = 44, WidthRequest = 300 };
            //NavigationPage.SetTitleView(targetPage, titleView);
            NavigationPage.SetHasNavigationBar(targetPage, false);
            MainPage = new NavigationPage(targetPage);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
