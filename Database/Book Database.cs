using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Media;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Storage;
using System.Threading.Tasks;

namespace Course_Record.Frames
{
    public sealed partial class Books : Page
    {
        class Book
        {
            public const int NoOfParametres = 3;

            public string Author { get; set; }
            public bool HavePDF { get; set; }
            public string Name { get; set; }

            public Book() { }

            public Book(string x, int FromIndex)
            {
                string[] input = x.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                Author = input[FromIndex++];
                HavePDF = (input[FromIndex++] == "True");
                Name = input[FromIndex];
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

            public Grid GridItem()
            {
                Grid grid = new Grid() { Margin = new Thickness(10, 10, 10, 10) };
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

                TextBlock name = new TextBlock()
                {
                    Text = Name,
                    HorizontalAlignment = HorizontalAlignment.Left
                };
                TextBlock author = new TextBlock()
                {
                    Text = Author,
                    HorizontalAlignment = HorizontalAlignment.Left
                };
                CheckBox cbox = new CheckBox()
                {
                    IsChecked = HavePDF,
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

            public override string ToString() =>
                string.Format(
                    "{0}\t{1}\t{2}",
                    Author,
                    HavePDF,
                    Name);
        }

        static readonly List<Book>[] BookList = new List<Book>[7];

        static List<Grid> GetGrid(int index)
        {
            if (BookList[index] == null)
                return new List<Grid>();

            List<Grid> grids = new List<Grid>();

            foreach (Book b in BookList[index])
                grids.Add(b.GridItem());

            return grids;
        }

        static async void WriteOnDisk()
        {
            string ListToString()
            {
                string output = "";

                for (int i = 0; i < BookList.Length; ++i)
                    if (BookList[i] != null)
                    {
                        output += BookList[i].Count + "\t";
                        foreach (Book book in BookList[i])
                            output += book.ToString() + "\t";
                        output += "\n";
                    }
                    else
                        output += "0\n";

                return output;
            }

            await FileIO.WriteTextAsync(await ApplicationData.Current.LocalFolder.CreateFileAsync(@"Database\Books", CreationCollisionOption.OpenIfExists), ListToString());
        }
        
        public static void Remove(int index_course, int index_book)
        {
            if (index_course < 0 || index_course > 6)
                throw new Exception("Course at index " + index_course + " not found.\n");
            if (BookList[index_course].Count < index_book || index_book < 0)
                throw new Exception("Given index is not found at specified course. Index range is from 0 to " + (BookList[index_course].Count - 1) + "\n");

            BookList[index_course].Remove(BookList[index_course][index_book]);
        }

        public static void GetFromDisk()
        {
            List<Book> Deserialize(string RawString)
            {
                string[] inputs = RawString.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);

                if (inputs.Length != 1 + (int.Parse(inputs[0]) * Book.NoOfParametres))
                    throw new Exception();

                List<Book> _output = new List<Book>();

                for (int CurrentIndex = 1; CurrentIndex < inputs.Length; CurrentIndex += Book.NoOfParametres)
                    _output.Add(new Book(RawString, CurrentIndex));

                return _output;
            }

            string output = Task.Run(async () =>
            {
                return await FileIO.ReadTextAsync(
                    await ApplicationData.Current.LocalFolder.GetFileAsync(
                        @"Database\Books"));
            }).Result;

            string[] lists = output.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < 7; ++i)
                BookList[i] = Deserialize(lists[i]);
        }
    }
}