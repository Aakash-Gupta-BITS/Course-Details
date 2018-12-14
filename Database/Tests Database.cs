using System;
using System.Collections.Generic;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Storage;
using System.Threading.Tasks;

namespace Course_Record.Frames
{
    public sealed partial class Tests : Page
    {
        class Test
        {
            public const int NoOfParametres = 4;

            //Hour:Minute Day MonthName
            public DateTime Date { get; set; }
            public decimal MarkObtained { get; set; }
            public string Name { get; set; }
            public decimal OutOf { get; set; }

            public Test() { }

            public Test(string x, int FromIndex)
            {
                string[] input = x.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                Date = new DateTime(
                    1,
                    int.Parse(input[FromIndex].Split(' ')[3]),
                    int.Parse(input[FromIndex].Split(' ')[2]),
                    int.Parse(input[FromIndex].Split(' ')[0]),
                    int.Parse(input[FromIndex].Split(' ')[1]),
                    0);
                MarkObtained = int.Parse(input[++FromIndex]);
                Name = input[++FromIndex];
                OutOf = decimal.Parse(input[++FromIndex]);
            }

            public static Grid Header()
            {
                Grid grid = new Grid() { Margin = new Thickness(10, 10, 10, 10) };
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

                TextBlock Name = new TextBlock()
                {
                    Text = "Name",
                    FontSize = 20,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    FontWeight = FontWeights.Bold
                };
                TextBlock Date = new TextBlock()
                {
                    Text = "Timings",
                    FontSize = 20,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    FontWeight = FontWeights.Bold
                };
                TextBlock Marks_Obtained = new TextBlock()
                {
                    Text = "Marks Obtained",
                    FontSize = 20,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    FontWeight = FontWeights.Bold
                };
                TextBlock Max_Marks = new TextBlock()
                {
                    Text = "Max Marks",
                    FontSize = 20,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    FontWeight = FontWeights.Bold
                };

                Grid.SetColumn(Name, 0);
                Grid.SetColumn(Date, 1);
                Grid.SetColumn(Marks_Obtained, 2);
                Grid.SetColumn(Max_Marks, 3);

                grid.Children.Add(Name);
                grid.Children.Add(Date);
                grid.Children.Add(Marks_Obtained);
                grid.Children.Add(Max_Marks);

                return grid;
            }

            public Grid GridItem()
            {
                string TimeItemString(int x) => (x.ToString().Length == 1 ? "0" + x : x.ToString());
                string[] MonthName = new string[] {"January",
                "February",
                "March",
                "April",
                "May",
                "June",
                "July",
                "August",
                "September",
                "October",
                "November",
                "December" };

                Grid grid = new Grid() { Margin = new Thickness(10, 10, 10, 10) };
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

                TextBlock Name = new TextBlock()
                {
                    Text = this.Name,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    FontSize = 15
                };
                TextBlock Timings = new TextBlock()
                {
                    Text = TimeItemString(Date.Hour) + ":" + TimeItemString(Date.Minute) + " " + Date.Day + " " + MonthName[Date.Month - 1],
                    HorizontalAlignment = HorizontalAlignment.Left,
                    FontSize = 15
                };
                TextBox Marks_Obtained = new TextBox()
                {
                    Text = MarkObtained.ToString(),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    FontSize = 15
                };
                TextBox Max_Marks = new TextBox()
                {
                    Text = OutOf.ToString(),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    FontSize = 15
                };

                Grid.SetColumn(Name, 0);
                Grid.SetColumn(Timings, 1);
                Grid.SetColumn(Marks_Obtained, 2);
                Grid.SetColumn(Max_Marks, 3);

                grid.Children.Add(Name);
                grid.Children.Add(Timings);
                grid.Children.Add(Marks_Obtained);
                grid.Children.Add(Max_Marks);

                return grid;
            }

            public override string ToString() => string.Format(
                "{0} {1} {2} {3}\t{4}\t{5}\t{6}",
                Date.Hour,
                Date.Minute,
                Date.Day,
                Date.Month,
                MarkObtained,
                Name,
                OutOf);
        }

        static readonly List<Test>[] TestList = new List<Test>[7];

        public static List<Grid> GetGrid(int index)
        {
            if (TestList[index] == null)
                return new List<Grid>();

            List<Grid> grids = new List<Grid>();

            foreach (Test t in TestList[index])
                grids.Add(t.GridItem());

            return grids;
        }

        private static async void WriteOnDisk()
        {
            string ListToString()
            {
                string output = "";

                for (int i = 0; i < TestList.Length; ++i)
                    if (TestList[i] != null)
                    {
                        output += TestList[i].Count + "\t";
                        foreach (Test t in TestList[i])
                            output += t.ToString() + "\t";
                        output += "\n";
                    }
                    else
                        output += "0\n";

                return output;
            }

            await FileIO.WriteTextAsync(await ApplicationData.Current.LocalFolder.CreateFileAsync(@"Database\Tests", CreationCollisionOption.OpenIfExists), ListToString());
        }

        public static void GetFromDisk()
        {

            List<Test> Desearlize(string RawString)
            {
                string[] inputs = RawString.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);

                if (inputs.Length != 1 + (int.Parse(inputs[0]) * Test.NoOfParametres))
                    throw new Exception();

                List<Test> _output = new List<Test>();

                for (int CurrentIndex = 1; CurrentIndex < inputs.Length; CurrentIndex += Test.NoOfParametres)
                    _output.Add(new Test(RawString, CurrentIndex));

                return _output;
            }

            string output = Task.Run(async () =>
            {
                return await FileIO.ReadTextAsync(
                    await ApplicationData.Current.LocalFolder.GetFileAsync(
                        @"Database\Tests"));
            }).Result;

            string[] lists = output.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < 7; ++i)
                TestList[i] = Desearlize(lists[i]);
        }
    }
}