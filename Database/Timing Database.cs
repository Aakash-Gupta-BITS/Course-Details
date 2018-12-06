using System;
using System.Collections.Generic;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Database
{
    class Timings
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
            public Day DayOfWeek;
            public DateTime Time = new DateTime();
            public string Type;
        }

        static readonly List<Timing>[] TimingList = new List<Timing>[7];

        static Timings()
        {
            TimingList[2] = new List<Timing>
            {
                new Timing
                {
                    DayOfWeek = Day.Monday,
                    Time = new DateTime(1, 1, 1, 15, 0, 0),
                    Type = "Lecture"
                },
                new Timing
                {
                    DayOfWeek = Day.Tuesday,
                    Time = new DateTime(1, 1, 1, 8, 0, 0),
                    Type = "Lab"
                },
                new Timing
                {
                    DayOfWeek = Day.Tuesday,
                    Time = new DateTime(1, 1, 1, 17, 0, 0),
                    Type = "Lecture"
                },
                new Timing
                {
                    DayOfWeek = Day.Wednesday,
                    Time = new DateTime(1, 1, 1, 15, 0, 0),
                    Type = "Lecture"
                },
                new Timing
                {
                    DayOfWeek = Day.Friday,
                    Time = new DateTime(1, 1, 1, 15, 0, 0),
                    Type = "Lecture"
                }
            };
        }

        private static Grid GetGrid(Timing t)
        {
            Grid grid = new Grid() { Margin = new Thickness(10, 10, 10, 10) };
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            TextBlock Day = new TextBlock()
            {
                Text = t.DayOfWeek.ToString("g"),
                HorizontalAlignment = HorizontalAlignment.Left
            };
            TextBlock Time = new TextBlock()
            {
                Text = (t.Time.Hour.ToString().Length == 1 ? "0" + t.Time.Hour : t.Time.Hour.ToString()) + ":" + (t.Time.Minute.ToString().Length == 1 ? "0" + t.Time.Minute : t.Time.Minute.ToString()),
                HorizontalAlignment = HorizontalAlignment.Left
            };
            TextBlock Type = new TextBlock()
            {
                Text = t.Type,
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

        public static Grid[] GetGrid(int index)
        {
            if (index < 0 || index > 6 || TimingList[index] == null)
                throw new IndexOutOfRangeException();

            Grid[] grid = new Grid[TimingList[index].Count];

            int i = 0;
            foreach (Timing t in TimingList[index])
                grid[i++] = GetGrid(t);

            return grid;
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
    }
}