using System.Collections.Generic;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Course_Record.Frames
{
    public sealed partial class Tests : Page
    {
        public Tests()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            TestStack.Children.Add(Test.Header());
            foreach (Grid d in GetGrid((int)e.Parameter))
                TestStack.Children.Add(d);
        }
    }
}