using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Storage;

namespace Course_Record
{
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            if (Username.Text.ToLower() == "hello" && Password.Password.ToLower() == "world")
                this.Frame.Navigate(typeof(MainPage));
        }
    }
}