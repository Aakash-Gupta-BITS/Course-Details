using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Course_Record.Frames
{
    public sealed partial class Teachers : Page
    {
        public Teachers()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            foreach (StackPanel sp in GetStackPanel((int)e.Parameter))
                TeacherStack.Children.Add(sp);
        }
    }
}