using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;



namespace Course_Record.Frames
{
    public sealed partial class Books : Page
    {
        int index = 0;

        public Books()
        {
            this.InitializeComponent();
            BookNamesStack.Children.Add(Database.Books.Header());
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            index = (int)e.Parameter;
            try
            {
                foreach (Grid d in Database.Books.GetGrid(index))
                    BookNamesStack.Children.Add(d);
            }
            catch
            {

            }
        }
    }
}