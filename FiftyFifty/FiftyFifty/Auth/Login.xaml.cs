using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiftyFifty.Models;
using FiftyFifty.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FiftyFifty.Auth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public Login()
        {
            InitializeComponent();
        }
        private async void Btn_login_Clicked(object sender, EventArgs e)
        {
            btn_login.IsEnabled = false;
            if (!string.IsNullOrWhiteSpace(tb_mail.Text) && !string.IsNullOrWhiteSpace(tb_password.Text))
            {
                var user = await firebaseHelper.GetUser(tb_mail.Text);
                if (user == null)
                {
                    await DisplayAlert("Hata", "Belirtilen Mail Adresi Sistemimizde Kayıtlı Bulunmamaktadır!", "Tamam");
                    tb_mail.Text = "";
                    tb_password.Text = "";
                    btn_login.IsEnabled = true;
                }
                else
                {
                    if (user.Password == tb_password.Text)
                    {
                        await Navigation.PushAsync(new Home(), true);
                    }
                    else
                    {
                        await DisplayAlert("Hata", "Şifre Eşleşmiyor! Tekrar Deneyin.", "Tamam");
                        tb_password.Text = "";
                        btn_login.IsEnabled = true;
                    }
                }

            }
            else
            {
                DisplayAlert("Hata", "Gerekli Alanları Doldurunuz!", "Tamam");
                btn_login.IsEnabled = true;
            }
        }
        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            return true;
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            //Animasyonlardan sonra giriş yapmak için gerekli kısımlar gelicek.

            lb_go.FadeTo(0, 500, Easing.Linear);
            lb_go.IsVisible = false;
            tb_mail.FadeTo(1, 500, Easing.BounceOut);
            tb_password.FadeTo(1, 500, Easing.BounceOut);
            btn_login.FadeTo(1, 500, Easing.BounceOut);

        }

        private async void TapGestureRecognizer_OnTapped2(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUp(), true);

        }
    }
}