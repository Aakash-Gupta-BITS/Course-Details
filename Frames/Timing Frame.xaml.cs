using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;



namespace Course_Record.Frames
{
    public sealed partial class Timings : Page
    {
        public Timings()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            TimingStack.Children.Add(Timing.Header());
            foreach (Grid d in GetGrid((int)e.Parameter))
                TimingStack.Children.Add(d);
        }
    }
}