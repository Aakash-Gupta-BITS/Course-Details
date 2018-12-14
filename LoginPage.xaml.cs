using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Threading.Tasks;
using Database;
using Windows.Storage;

namespace Course_Record
{
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();

            LocalFilesLocation.Text = ApplicationData.Current.LocalFolder.Path;
        }

        private void LocalFilesLocation_TextChanged(object sender, TextChangedEventArgs e)
        {
            LocalFilesLocation.Text = ApplicationData.Current.LocalFolder.Path;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Security.LoginCheck(Username.Text, Password.Password))
                this.Frame.Navigate(typeof(MainPage));
        }
    }
}