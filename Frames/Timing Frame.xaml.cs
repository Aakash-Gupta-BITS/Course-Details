using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;



namespace Course_Record.Frames
{
    public sealed partial class Timing_Frame : Page
    {
        int index = 0;
        public Timing_Frame()
        {
            this.InitializeComponent();
            TimingStack.Children.Add(Database.Timings.Header());
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            index = (int)e.Parameter;
            try
            {
                foreach (Grid d in Database.Timings.GetGrid(index))
                    TimingStack.Children.Add(d);
            }
            catch
            {

            }
        }
    }
}
