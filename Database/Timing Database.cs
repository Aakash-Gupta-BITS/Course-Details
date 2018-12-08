using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Storage;

namespace Course_Record.Frames
{
    public sealed partial class Timings : Page
    {
        enum Day
        {
            Sunday,
            Monday,
            Tuesday,
            Wednesday,
            Thrusday,
            Friday,
            Saturday
        }

        class Timing
        {
            public const int NoOfPaarametres = 3;

            public Day DayOfWeek { get; set; }
            //Hour:Minute
            public DateTime Time { get; set; }
            public string Type { get; set; }

            public Timing() { }

            public Timing(string x, int FromIndex)
            {
                string[] input = x.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                DayOfWeek = (Day)int.Parse(input[FromIndex++]);
                Time = new DateTime(
                    1,
                    1,
                    1,
                    int.Parse(input[FromIndex].Split(' ')[0]),
                    int.Parse(input[FromIndex].Split(' ')[1]),
                    0);
                Type = input[++FromIndex];
            }

            public static Grid Header()
            {
                Grid grid = new Grid() { Margin = new Thickness(10, 10, 10, 10) };
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

                TextBlock name = new TextBlock()
                {
                    Text = "Day",
                    FontSize = 20,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    FontWeight = FontWeights.Bold
                };
                TextBlock author = new TextBlock()
                {
                    Text = "Time",
                    FontSize = 20,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    FontWeight = FontWeights.Bold
                };
                TextBlock HavePdf = new TextBlock()
                {
                    Text = "Type",
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
                string TimeItemString(int x) => (x.ToString().Length == 1 ? "0" + x : x.ToString());

                Grid grid = new Grid() { Margin = new Thickness(10, 10, 10, 10) };
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

                TextBlock Day = new TextBlock()
                {
                    Text = DayOfWeek.ToString("g"),
                    HorizontalAlignment = HorizontalAlignment.Left
                };
                TextBlock Time = new TextBlock()
                {
                    Text = TimeItemString(this.Time.Hour) + ":" + TimeItemString(this.Time.Minute),
                    HorizontalAlignment = HorizontalAlignment.Left
                };
                TextBlock Type = new TextBlock()
                {
                    Text = this.Type,
                    HorizontalAlignment = HorizontalAlignment.Left
                };

                Grid.SetColumn(Day, 0);
                Grid.SetColumn(Time, 1);
                Grid.SetColumn(Type, 2);

                grid.Children.Add(Day);
                grid.Children.Add(Time);
                grid.Children.Add(Type);

                return grid;
            }

            public override string ToString() => string.Format(
                "{0}\t{1} {2}\t{3}",
                (int)DayOfWeek,
                Time.Hour,
                Time.Minute,
                Type);

        }

        static readonly List<Timing>[] TimingList = new List<Timing>[7];

        static List<Grid> GetGrid(int index)
        {
            if (TimingList[index] == null)
                return new List<Grid>();

            List<Grid> grids = new List<Grid>();

            foreach (Timing t in TimingList[index])
                grids.Add(t.GridItem());

            return grids;
        }

        static async void WriteOnDisk()
        {
            string ListToString()
            {
                string output = "";

                for (int i = 0; i < TimingList.Length; ++i)
                    if (TimingList[i] != null)
                    {
                        output += TimingList[i].Count;
                        foreach (Timing t in TimingList[i])
                            output += t.ToString() + "\t";
                        output += "\n";
                    }
                    else
                        output += "0\n";

                return output;
            }
            await FileIO.WriteTextAsync(await ApplicationData.Current.LocalFolder.CreateFileAsync(@"Database\Timings", CreationCollisionOption.OpenIfExists), ListToString());
        }

        public static void GetFromDisk()
        {
            List<Timing> Desearlize(string RawString)
            {
                string[] inputs = RawString.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);

                if (inputs.Length != 1 + (int.Parse(inputs[0]) * Timing.NoOfPaarametres))
                    throw new Exception();

                List<Timing> _output = new List<Timing>();

                for (int CurrentIndex = 1; CurrentIndex < inputs.Length; CurrentIndex += Timing.NoOfPaarametres)
                    _output.Add(new Timing(RawString, CurrentIndex));

                return _output;
            }
            string output = Task.Run(async () =>
            {
                return await FileIO.ReadTextAsync(
                    await ApplicationData.Current.LocalFolder.GetFileAsync(
                        @"Database\Timings"));
            }).Result;

            string[] lists = output.Split('\n');

            for (int i = 0; i < 7; ++i)
                TimingList[i] = Desearlize(lists[i]);
        }
    }
}