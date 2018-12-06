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
    class Handouts
    {
        class HandoutItem
        {
            public string Name = "";
            public uint LectureNo = 0;
            public bool DoneInClass = false;
            public bool DoneByMe = false;
        }

        static readonly List<HandoutItem>[] HandoutList = new List<HandoutItem>[7];

        static Handouts()
        {
            try
            {
                GetFromDisk();
            }
            catch
            {

            }
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

        private static Grid GetGrid(HandoutItem item)
        {
            Grid grid = new Grid() { Margin = new Thickness(10, 10, 10, 10) };
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            TextBlock name = new TextBlock()
            {
                Text = item.Name,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            TextBlock lecture = new TextBlock()
            {
                Text = item.LectureNo.ToString(),
                HorizontalAlignment = HorizontalAlignment.Left
            };
            CheckBox cbox1 = new CheckBox()
            {
                IsChecked = item.DoneInClass,
                IsEnabled = false,
                Content = "",
                HorizontalAlignment = HorizontalAlignment.Center
            };
            CheckBox cbox2 = new CheckBox()
            {
                IsChecked = item.DoneByMe,
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

        public static Grid[] GetGrid(int index)
        {
            if (index < 0 || index > 6 || HandoutList[index] == null)
                throw new IndexOutOfRangeException();

            Grid[] grid = new Grid[HandoutList[index].Count];

            int i = 0;
            foreach (HandoutItem b in HandoutList[index])
                grid[i++] = GetGrid(b);

            return grid;
        }



        private static string ListToString(List<HandoutItem> l)
        {
            string output = l.Count.ToString() + "\t";
            foreach (HandoutItem b in l)
            {
                output += b.Name + "\t";
                output += b.LectureNo + "\t";
                output += (b.DoneInClass ? "true" : "false") + "\t";
                output += (b.DoneByMe ? "true" : "false") + "\t";
            }

            return output + "\n";
        }

        private static string ListToString()
        {
            string output = "";

            for (int i = 0; i < HandoutList.Length; ++i)
                if (HandoutList[i] != null)
                    output += ListToString(HandoutList[i]);
                else
                    output += "0\n";

            return output;
        }

        private static async void WriteOnDisk()
        {
            await FileIO.WriteTextAsync(await ApplicationData.Current.LocalFolder.CreateFileAsync(@"Database\handouts", CreationCollisionOption.OpenIfExists), ListToString());
        }

        private static List<HandoutItem> Desearlize(string x)
        {
            if (x == "0") return new List<HandoutItem>();
            string[] inputs = x.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
            int a = int.Parse(inputs[0]);

            if (inputs.Length != 1 + (a * 4))
                throw new Exception();

            List<HandoutItem> output = new List<HandoutItem>();

            for (int i = 1; i < inputs.Length;)
            {
                HandoutItem b = new HandoutItem();
                b.Name = inputs[i++];
                b.LectureNo = uint.Parse(inputs[i++]);
                b.DoneInClass = (inputs[i++] == "false") ? false : true;
                b.DoneByMe = (inputs[i++] == "false") ? false : true;
                output.Add(b);
            }
            return output;
        }

        private static void GetFromDisk()
        {
            string output = Task.Run(async () => { return await FileIO.ReadTextAsync(await ApplicationData.Current.LocalFolder.GetFileAsync(@"Database\handouts")); }).Result;
            string[] lists = output.Split('\n');

            for (int i = 0; i < 7; ++i)
                HandoutList[i] = Desearlize(lists[i]);
        }
    }
}
