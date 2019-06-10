using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiftyFifty.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FiftyFifty.Auth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUp : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        public SignUp()
        {
            InitializeComponent();
           
            tb_mail.FadeTo(1, 500, Easing.Linear);
            tb_password.FadeTo(1, 500, Easing.Linear);
            tb_fullname.FadeTo(1, 500, Easing.Linear);
            btn_signUp.FadeTo(1, 500, Easing.Linear);
           
        }

        private async void Btn_signUp_OnClicked(object sender, EventArgs e)
        {
            //Boş mu diye kontrol ediyor sadece.
            if (!string.IsNullOrWhiteSpace(tb_mail.Text) || !string.IsNullOrWhiteSpace(tb_password.Text)|| !string.IsNullOrWhiteSpace(tb_fullname.Text))
            {
                //İçinde başka kontroller de yapılabilir güvenlik için.
                await firebaseHelper.AddUser(tb_mail.Text, tb_password.Text,tb_fullname.Text);
                tb_mail.Text = string.Empty;
                tb_password.Text = string.Empty;
                await DisplayAlert("Tebrikler!", "Kayıt işlemini tamamladın. Aramıza Hoşgeldin.", "Tamam");
                await Navigation.PushAsync(new Home(), true);
            }
            else
            {
                DisplayAlert("Hata", "Gerekli Alanları Doldurunuz!", "Geri");
            }
        }
    }
}