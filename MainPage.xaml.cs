using Windows.UI.Xaml.Controls;


namespace Course_Record
{
    public sealed partial class MainPage : Page
    {
        int lastSelectedIndex = 1;
        bool ChangedByProgram = true;

        public MainPage()
        {
            this.InitializeComponent();
            UpdatePivots();
        }

        private void HamburgerMenuChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (HamburgerMenuPane.SelectedIndex)
            {
                case 0:
                    HamburgerMenu.IsPaneOpen = !HamburgerMenu.IsPaneOpen;
                    ChangedByProgram = true;
                    HamburgerMenuPane.SelectedIndex = lastSelectedIndex;
                    break;

                default:
                    if (ChangedByProgram)
                    {
                        ChangedByProgram = false;
                        return;
                    }
                    lastSelectedIndex = HamburgerMenuPane.SelectedIndex;
                    HamburgerMenu.IsPaneOpen = false;
                    UpdatePivots();
                    break;
            }
        }

        private void UpdatePivots()
        {
                Books.Navigate(typeof(Frames.Books), lastSelectedIndex - 1);
                Handouts.Navigate(typeof(Frames.Handout), lastSelectedIndex - 1);
                TeacherList.Navigate(typeof(Frames.Teachers_List), lastSelectedIndex - 1);
                TestList.Navigate(typeof(Frames.Test_Frame), lastSelectedIndex - 1);
                Timings.Navigate(typeof(Frames.Timing_Frame), lastSelectedIndex - 1);
        }
    }
}