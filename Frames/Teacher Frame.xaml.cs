using System.Collections.Generic;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System;

namespace Course_Record.Frames
{

    public sealed partial class Teachers_List : Page
    {
        int index = 0;

        public Teachers_List()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            index = (int)e.Parameter;
            try
            {
                foreach (StackPanel sp in Database.Teachers.GetStackPanel(index))
                    TeacherStack.Children.Add(sp);
            }
            catch
            {

            }
        }
    }
}
