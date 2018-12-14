using System;
using System.Collections.Generic;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Storage;
using System.Threading.Tasks;

namespace Course_Record.Frames
{
    public sealed partial class Handout : Page
    {
        class HandoutItem
        {
            public const int NoOfParametres = 4;

            public bool DoneByMe { get; set; }
            public bool DoneInClass { get; set; }
            public uint LectureNo { get; set; }
            public string Name { get; set; }

            public HandoutItem() { }

            public HandoutItem(string x, int FromIndex)
            {
                string[] input = x.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);

                DoneByMe = input[FromIndex++] == "true";
                DoneInClass = input[FromIndex++] == "true";
                LectureNo = uint.Parse(input[FromIndex++]);
                Name = input[FromIndex].Replace('\r', '\n');
            }

            public static Grid Header()
            {
                Grid grid = new Grid() { Margin = new Thickness(10, 10, 10, 10) };
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

                TextBlock Name = new TextBlock()
                {
                    Text = "Name",
                    FontSize = 20,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    FontWeight = FontWeights.Bold
                };
                TextBlock LectureNo = new TextBlock()
                {
                    Text = "Lecture No",
                    FontSize = 20,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    FontWeight = FontWeights.Bold
                };
                TextBlock DoneinClass = new TextBlock()
                {
                    Text = "Done in Class",
                    FontSize = 20,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    FontWeight = FontWeights.Bold
                };
                TextBlock DonebyMe = new TextBlock()
                {
                    Text = "Done by Me",
                    FontSize = 20,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    FontWeight = FontWeights.Bold
                };

                Grid.SetColumn(Name, 0);
                Grid.SetColumn(LectureNo, 1);
                Grid.SetColumn(DoneinClass, 2);
                Grid.SetColumn(DonebyMe, 3);

                grid.Children.Add(Name);
                grid.Children.Add(LectureNo);
                grid.Children.Add(DoneinClass);
                grid.Children.Add(DonebyMe);

                return grid;
            }

            public Grid GridItem()
            {
                Grid grid = new Grid() { Margin = new Thickness(10, 10, 10, 10) };
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

                TextBlock name = new TextBlock()
                {
                    Text = Name,
                    HorizontalAlignment = HorizontalAlignment.Left
                };
                TextBlock lecture = new TextBlock()
                {
                    Text = LectureNo.ToString(),
                    HorizontalAlignment = HorizontalAlignment.Left
                };
                CheckBox cbox1 = new CheckBox()
                {
                    IsChecked = DoneInClass,
                    IsEnabled = false,
                    Content = "",
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                CheckBox cbox2 = new CheckBox()
                {
                    IsChecked = DoneByMe,
                    Content = "",
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                Grid.SetColumn(name, 0);
                Grid.SetColumn(lecture, 1);
                Grid.SetColumn(cbox1, 2);
                Grid.SetColumn(cbox2, 3);

                grid.Children.Add(name);
                grid.Children.Add(lecture);
                grid.Children.Add(cbox1);
                grid.Children.Add(cbox2);

                return grid;
            }

            public override string ToString() =>
                string.Format("{0}\t{1}\t{2}\t{3}",
                    DoneByMe,
                    DoneInClass,
                    LectureNo,
                    Name.Replace('\n', '\r'));
        }

        static readonly List<HandoutItem>[] HandoutList = new List<HandoutItem>[7];
        
        public static List<Grid> GetGrid(int index)
        {
            if (HandoutList[index] == null)
                return new List<Grid>();

            List<Grid> grids = new List<Grid>();
            
            foreach (HandoutItem b in HandoutList[index])
                grids.Add(b.GridItem());

            return grids;
        }

        static async void WriteOnDisk()
        {
            string ListToString()
            {
                string output = "";

                for (int i = 0; i < HandoutList.Length; ++i)
                    if (HandoutList[i] != null)
                    {
                        output += HandoutList[i].Count;
                        foreach (HandoutItem hi in HandoutList[i])
                            output += hi.ToString() + "\t";
                        output += "\n";
                    }
                    else
                        output += "0\n";

                return output;
            }

            await FileIO.WriteTextAsync(await ApplicationData.Current.LocalFolder.CreateFileAsync(@"Database\Handouts", CreationCollisionOption.OpenIfExists), ListToString());
        }

        public static void GetFromDisk()
        {
            List<HandoutItem> Desearlize(string RawString)
            {
                string[] inputs = RawString.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);

                if (inputs.Length != 1 + (int.Parse(inputs[0]) * HandoutItem.NoOfParametres))
                    throw new Exception();

                List<HandoutItem> _output = new List<HandoutItem>();

                for (int CurrentIndex = 1; CurrentIndex < inputs.Length; CurrentIndex += HandoutItem.NoOfParametres)
                    _output.Add(new HandoutItem(RawString, CurrentIndex));

                return _output;
            }

            string output = Task.Run(async () => 
            {
                return await FileIO.ReadTextAsync(
                    await ApplicationData.Current.LocalFolder.GetFileAsync(
                        @"Database\Handouts")
                        );
            }).Result;

            string[] lists = output.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < 7; ++i)
                HandoutList[i] = Desearlize(lists[i]);
        }
    }
}