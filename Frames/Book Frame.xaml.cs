using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media;
using Windows.UI;

namespace Course_Record.Frames
{
    public sealed partial class Books : Page
    {
        int index = 0;
        public Books()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            index = (int)e.Parameter;
            BookNamesStack.Children.Add(Book.Header());
            foreach (Grid d in GetGrid(index))
                BookNamesStack.Children.Add(d);
        }

        private void Add_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (Add_Name.Text == "")
            {
                Add_Name.BorderBrush = new SolidColorBrush(Colors.Red);
                return;
            }
            if (Add_Author.Text == "")
            {
                Add_Author.BorderBrush = new SolidColorBrush(Colors.Red);
                return;
            }

            BookList[index].Add(new Book { Name = Add_Name.Text, Author = Add_Author.Text, HavePDF = (Add_HavePDF.IsChecked == true) });

            new Task(WriteOnDisk).Start();

            Add_Name.BorderBrush = new SolidColorBrush(Colors.Black);
            Add_Author.BorderBrush = new SolidColorBrush(Colors.Black);

            ViewFrame.Visibility = Windows.UI.Xaml.Visibility.Visible;
            AddFrame.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            BookNamesStack.Children.Clear();
            BookNamesStack.Children.Add(Book.Header());
            foreach (Grid d in GetGrid(index))
                BookNamesStack.Children.Add(d);
        }

        private void Modify_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (Modify_Name.Text == "")
            {
                Modify_Name.BorderBrush = new SolidColorBrush(Colors.Red);
                return;
            }

            if (Modify_Author.Text == "")
            {
                Modify_Author.BorderBrush = new SolidColorBrush(Colors.Red);
                return;
            }

            BookList[index][Modify_ItemsList.SelectedIndex] = new Book { Author = Modify_Author.Text, Name = Modify_Name.Text, HavePDF = (Modify_HavePDF.IsChecked == true) };

            new Task(WriteOnDisk).Start();

            Modify_Name.BorderBrush = new SolidColorBrush(Colors.Black);
            Modify_Author.BorderBrush = new SolidColorBrush(Colors.Black);

            Modify_Name.Text = "";
            Modify_Author.Text = "";
            Modify_HavePDF.IsChecked = false;

            ViewFrame.Visibility = Windows.UI.Xaml.Visibility.Visible;
            ModifyFrame.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            BookNamesStack.Children.Clear();
            BookNamesStack.Children.Add(Book.Header());
            foreach (Grid d in GetGrid(index))
                BookNamesStack.Children.Add(d);
        }

        private void Delete_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (Delete_ItemsList.SelectedIndex < 0)
                return;
            BookList[index].Remove(BookList[index][Delete_ItemsList.SelectedIndex]);
            new Task(WriteOnDisk).Start();

            ViewFrame.Visibility = Windows.UI.Xaml.Visibility.Visible;
            DeleteFrame.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            BookNamesStack.Children.Clear();
            BookNamesStack.Children.Add(Book.Header());
            foreach (Grid d in GetGrid(index))
                BookNamesStack.Children.Add(d);
        }

        private void CommandClicked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewFrame.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            AddFrame.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            ModifyFrame.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            DeleteFrame.Visibility = Windows.UI.Xaml.Visibility.Collapsed;


            switch (((AppBarButton)sender).Content)
            {
                case "View":
                    ViewFrame.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    return;

                case "Add":
                    AddFrame.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    return;

                case "Modify":
                    Modify_ItemsList.Items.Clear();

                    foreach (Book b in BookList[index])
                        Modify_ItemsList.Items.Add(b.Name);

                    ModifyFrame.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    return;

                case "Delete":
                    Delete_ItemsList.Items.Clear();

                    foreach (Book b in BookList[index])
                        Delete_ItemsList.Items.Add(b.Name);

                    DeleteFrame.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    return;
            }
        }

        private void Modify_ItemsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int sec_index = Modify_ItemsList.SelectedIndex;
            if (sec_index < 0)
                return;
            Modify_Author.Text = BookList[index][sec_index].Author;
            Modify_Name.Text = BookList[index][sec_index].Name;
            Modify_HavePDF.IsChecked = BookList[index][sec_index].HavePDF;
        }
    }
}