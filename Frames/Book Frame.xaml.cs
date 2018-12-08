
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Course_Record.Frames
{
    public sealed partial class Books : Page
    {
        public Books()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            BookNamesStack.Children.Add(Book.Header());
            foreach (Grid d in GetGrid((int)e.Parameter))
                BookNamesStack.Children.Add(d);
        }
    }
}