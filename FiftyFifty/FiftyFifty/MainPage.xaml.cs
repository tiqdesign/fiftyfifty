using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace FiftyFifty
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
      
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public MainPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {

            base.OnAppearing();
            var allPersons = await firebaseHelper.GetAllUsers();
            lstPersons.ItemsSource = allPersons;
        }

        private void BtnUpdate_OnClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnDelete_OnClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnRetrive_OnClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private async void BtnAdd_OnClicked(object sender, EventArgs e)
        {
            
        }
    }
}
