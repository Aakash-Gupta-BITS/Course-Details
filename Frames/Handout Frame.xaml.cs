using System.Collections.Generic;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;


namespace Course_Record.Frames
{
    public sealed partial class Handout : Page
    {
        public Handout()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            HandoutTopicNamesStack.Children.Add(HandoutItem.Header());
            foreach (Grid d in GetGrid((int)e.Parameter))
                HandoutTopicNamesStack.Children.Add(d);
        }
    }
}