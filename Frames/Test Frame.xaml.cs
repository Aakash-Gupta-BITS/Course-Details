using System.Collections.Generic;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Course_Record.Frames
{
    public sealed partial class Test_Frame : Page
    {
        int index = 0;
        public Test_Frame()
        {
            this.InitializeComponent();
            TestStack.Children.Add(Database.Tests.Header());
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            index = (int)e.Parameter;
            try
            {
                foreach (Grid d in Database.Tests.GetGrid(index))
                    TestStack.Children.Add(d);
            }
            catch
            {

            }
        }
    }
}