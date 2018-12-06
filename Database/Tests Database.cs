using System;
using System.Collections.Generic;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Database
{
    static class Tests
    {
        class Test
        {
            public string Name;
            public decimal MarkObtained = 0;
            public DateTime Date = new DateTime();
            public decimal OutOf = 0;
        }

        static readonly List<Test>[] TestList = new List<Test>[7];

        static Tests()
        {
            TestList[2] = new List<Test>
            {
                new Test
                {
                    Name = "MidSem",
                    MarkObtained = 25,
                    OutOf = 30
                },
                new Test
                {
                    Name = "MidSem",
                    MarkObtained = 25,
                    OutOf = 30
                }
            };
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

        private static Grid GetGrid(Test t)
        {
            Grid grid = new Grid() { Margin = new Thickness(10, 10, 10, 10) };
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            TextBlock Name = new TextBlock()
            {
                Text = t.Name,
                HorizontalAlignment = HorizontalAlignment.Left,
                FontSize = 15
            };
            TextBlock Timings = new TextBlock()
            {
                Text = t.Date.Date.ToString(),
                HorizontalAlignment = HorizontalAlignment.Left,
                FontSize = 15
            };
            TextBox Marks_Obtained = new TextBox()
            {
                Text = t.MarkObtained.ToString(),
                HorizontalAlignment = HorizontalAlignment.Left,
                FontSize = 15
            };
            TextBox Max_Marks = new TextBox()
            {
                Text = t.OutOf.ToString(),
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

        public static Grid[] GetGrid(int index)
        {
            if (index < 0 || index > 6 || TestList[index] == null)
                throw new IndexOutOfRangeException();

            Grid[] grid = new Grid[TestList[index].Count];

            int i = 0;
            foreach (Test t in TestList[index])
                grid[i++] = GetGrid(t);

            return grid;
        }

    }
}
