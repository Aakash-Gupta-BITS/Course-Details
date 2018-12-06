using System;
using System.Collections.Generic;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Storage;
using System.Threading.Tasks;
using System.IO;

namespace Database
{
    public static class Books
    {
        class Book
        {
            public string Name;
            public string Author;
            public bool HavePDF;
        }

        static readonly List<Book>[] BookList = new List<Book>[7];

        static Books()
        {
            try
            {
                GetFromDisk();
            }
            catch
            {

            }
        }

        private static Grid GetGrid(Book b)
        {
            Grid grid = new Grid() { Margin = new Thickness(10, 10, 10, 10) };
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            TextBlock name = new TextBlock()
            {
                Text = b.Name,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            TextBlock author = new TextBlock()
            {
                Text = b.Author,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            CheckBox cbox = new CheckBox()
            {
                IsChecked = b.HavePDF,
                IsEnabled = false,
                Content = "",
                HorizontalAlignment = HorizontalAlignment.Center
            };


            Grid.SetColumn(name, 0);
            Grid.SetColumn(author, 1);
            Grid.SetColumn(cbox, 2);

            grid.Children.Add(name);
            grid.Children.Add(author);
            grid.Children.Add(cbox);

            return grid;
        }

        public static Grid[] GetGrid(int index)
        {
            if (index < 0 || index > 6 || BookList[index] == null)
                throw new IndexOutOfRangeException();

            Grid[] grid = new Grid[BookList[index].Count];

            int i = 0;
            foreach (Book b in BookList[index])
                grid[i++] = GetGrid(b);

            return grid;
        }

        public static Grid Header()
        {
            Grid grid = new Grid() { Margin = new Thickness(10, 10, 10, 10) };
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            TextBlock name = new TextBlock()
            {
                Text = "Name",
                FontSize = 20,
                HorizontalAlignment = HorizontalAlignment.Left,
                FontWeight = FontWeights.Bold
            };
            TextBlock author = new TextBlock()
            {
                Text = "Author",
                FontSize = 20,
                HorizontalAlignment = HorizontalAlignment.Left,
                FontWeight = FontWeights.Bold
            };
            TextBlock HavePdf = new TextBlock()
            {
                Text = "Have PDF",
                FontSize = 20,
                HorizontalAlignment = HorizontalAlignment.Left,
                FontWeight = FontWeights.Bold
            };

            Grid.SetColumn(name, 0);
            Grid.SetColumn(author, 1);
            Grid.SetColumn(HavePdf, 2);

            grid.Children.Add(name);
            grid.Children.Add(author);
            grid.Children.Add(HavePdf);

            return grid;
        }


        private static string ListToString(List<Book> l)
        {
            string output = l.Count.ToString() + "\t";
            foreach (Book b in l)
            {
                output += b.Name + "\t";
                output += b.Author + "\t";
                output += (b.HavePDF ? "true" : "false") + "\t";
            }

            return output + "\n";
        }

        private static string ListToString()
        {
            string output = "";

            for (int i = 0; i < BookList.Length; ++i)
                if (BookList[i] != null)
                    output += ListToString(BookList[i]);
                else
                    output += "0\n";

            return output;
        }

        private static async void WriteOnDisk()
        {
            await FileIO.WriteTextAsync(await ApplicationData.Current.LocalFolder.CreateFileAsync(@"Database\books", CreationCollisionOption.OpenIfExists), ListToString());
        }

        private static List<Book> Desearlize(string x)
        {
            if (x == "0") return new List<Book>();
            string[] inputs = x.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
            int a = int.Parse(inputs[0]);

            if (inputs.Length != 1 + (a * 3))
                throw new Exception();

            List<Book> output = new List<Book>();

            for (int i = 1; i < inputs.Length; )
            {
                Book b = new Book();
                b.Name = inputs[i++];
                b.Author = inputs[i++];
                b.HavePDF = (inputs[i++] == "false") ? false : true;
                output.Add(b);
            }
            return output;
        }

        private static void GetFromDisk()
        {
            string output = Task.Run(async () => { return await FileIO.ReadTextAsync(await ApplicationData.Current.LocalFolder.GetFileAsync(@"Database\books")); }).Result;
            string[] lists = output.Split('\n');

            for (int i = 0; i < 7; ++i)
                BookList[i] = Desearlize(lists[i]);
        }
    }
}