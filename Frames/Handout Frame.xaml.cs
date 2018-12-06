using System.Collections.Generic;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;


namespace Course_Record.Frames
{
    public sealed partial class Handout : Page
    {
        int index = 0;

        public Handout()
        {
            this.InitializeComponent();
            HandoutTopicNamesStack.Children.Add(Database.Handouts.Header());
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            index = (int)e.Parameter;
            try
            {
                foreach (Grid d in Database.Handouts.GetGrid(index))
                    HandoutTopicNamesStack.Children.Add(d);
            }
            catch
            {

            }
        }
    }
}