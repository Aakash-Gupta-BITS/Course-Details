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

            new Task(Frames.Books.GetFromDisk).Start();
            new Task(Frames.Teachers.GetFromDisk).Start();
            new Task(Frames.Handout.GetFromDisk).Start();
            new Task(Frames.Tests.GetFromDisk).Start();
            new Task(Frames.Timings.GetFromDisk).Start();
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            if (Security.LoginCheck(Username.Text, Password.Password))
                this.Frame.Navigate(typeof(Changes.ChangeBookItems));
        }

        private void LocalFilesLocation_TextChanged(object sender, TextChangedEventArgs e)
        {
            LocalFilesLocation.Text = ApplicationData.Current.LocalFolder.Path;
        }
    }
}