using System;
using System.Collections.Generic;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Database
{
    class Teachers
    {
        class Teacher
        {
            public string Name;
            public string ChamberNo;
            public string DayofWeek;
            public DateTime Time;
            public string Contact;
            public string Email;
        }

        static readonly List<Teacher>[] TeachersList = new List<Teacher>[7];

        static Teachers()
        {
            TeachersList[2] = new List<Teacher>
            {
                new Teacher
                {
                    Name = "Rajdeep Chowdhury",
                    ChamberNo = "3222 - S",
                    DayofWeek = "Monday",
                    Time = new DateTime(2000, 1, 1, 15, 0, 0),
                    Contact = "+91 159651-5608",
                    Email = "rajdeep.chowdhury@pilani.bits-pilani.ac.in",

                }
            };
        }

        private static StackPanel GetStackPanel(Teacher teacher)
        {
            StackPanel stack = new StackPanel() { Margin = new Thickness(10, 10, 10, 10) };
            TextBlock name = new TextBlock()
            {
                Text = teacher.Name,
                HorizontalAlignment = HorizontalAlignment.Left,
                FontSize = 30
            };
            TextBlock OtherInfo = new TextBlock()
            {
                Text = string.Format("Chamber No. : {0}\nTimings : {1}, {2}\nContact : {3}\nE-mail : {4}", teacher.ChamberNo, teacher.DayofWeek, teacher.Time.TimeOfDay, teacher.Contact, teacher.Email),
                HorizontalAlignment = HorizontalAlignment.Left,
                FontSize = 15
            };

            stack.Children.Add(name);
            stack.Children.Add(OtherInfo);

            return stack;
        }

        public static StackPanel[] GetStackPanel(int index)
        {
            if (index < 0 || index > 6 || TeachersList[index] == null)
                throw new IndexOutOfRangeException();

            StackPanel[] panels = new StackPanel[TeachersList[index].Count];

            int i = 0;
            foreach (Teacher t in TeachersList[index])
                panels[i++] = GetStackPanel(t);

            return panels;
        }
    }
}
