using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Storage;
using System.Threading.Tasks;

namespace Course_Record.Frames
{
    sealed partial class Teachers : Page
    {
        class Teacher
        {
            public static int NoOfParametres => 6;

            public string ChamberNo { get; set; }
            public string Contact { get; set; }
            public string DayofWeek { get; set; }
            public string Email { get; set; }
            public string Name { get; set; }
            public DateTime Time { get; set; }

            public Teacher()
            {

            }

            public Teacher(string x, int FromIndex)
            {
                string[] inputs = x.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                ChamberNo = inputs[FromIndex];
                Contact = inputs[FromIndex + 1];
                DayofWeek = inputs[FromIndex + 2];
                Email = inputs[FromIndex + 3];
                Name = inputs[FromIndex + 4];
                Time = new DateTime(1, 1, 1, int.Parse(inputs[FromIndex + 5].Split(' ')[0]), int.Parse(inputs[FromIndex + 5].Split(' ')[1]), 0);
            }

            public StackPanel StackItem()
            {
                string TimeItemString(int x) => (x.ToString().Length == 1 ? "0" + x : x.ToString());

                StackPanel stack = new StackPanel() { Margin = new Thickness(10, 10, 10, 10) };
                TextBlock name = new TextBlock()
                {
                    Text = Name,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    FontSize = 30
                };
                TextBlock OtherInfo = new TextBlock()
                {
                    Text = string.Format(
                        "Chamber No. : {0}\nTimings : {1}, {2}\nContact : {3}\nE-mail : {4}",
                        ChamberNo,
                        DayofWeek,
                        TimeItemString(Time.Hour) + ":" + TimeItemString(Time.Minute),
                        Contact,
                        Email
                    ),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    FontSize = 15
                };

                stack.Children.Add(name);
                stack.Children.Add(OtherInfo);

                return stack;
            }

            public override string ToString() => string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}", ChamberNo, Contact, DayofWeek, Email, Name, Time.Hour + " " + Time.Minute);
        }

        static readonly List<Teacher>[] TeachersList = new List<Teacher>[7];

        static List<StackPanel> GetStackPanel(int index)
        {
            if (TeachersList[index] == null)
                return new List<StackPanel>();

            List<StackPanel> panels = new List<StackPanel>();

            foreach (Teacher teacher in TeachersList[index])
                panels.Add(teacher.StackItem());

            return panels;
        }

        static async void WriteOnDisk()
        {
            string ListToString()
            {
                string output = "";

                for (int i = 0; i < TeachersList.Length; ++i)
                    if (TeachersList[i] != null)
                    {
                        output += TeachersList[i].Count + "\t";
                        foreach (Teacher TeacherEntry in TeachersList[i])
                            output += TeacherEntry.ToString() + "\t";
                        output += "\n";
                    }
                    else
                        output += "0\n";

                return output;
            }

            await FileIO.WriteTextAsync(await ApplicationData.Current.LocalFolder.CreateFileAsync(@"Database\Teachers", CreationCollisionOption.OpenIfExists), ListToString());
        }

        public static void GetFromDisk()
        {
            List<Teacher> Desearlize(string RawString)
            {
                string[] inputs = RawString.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);

                if (inputs.Length != 1 + (int.Parse(inputs[0]) * Teacher.NoOfParametres))
                    throw new Exception();

                List<Teacher> _output = new List<Teacher>();

                for (int CurrentIndex = 1; CurrentIndex < inputs.Length; CurrentIndex += Teacher.NoOfParametres)
                    _output.Add(new Teacher(RawString, CurrentIndex));

                return _output;
            }

            string output = Task.Run(async () =>
            {
                return await FileIO.ReadTextAsync(
                    await ApplicationData.Current.LocalFolder.GetFileAsync(
                        @"Database\Teachers"));
            }).Result;

            string[] lists = output.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < 7; ++i)
                TeachersList[i] = Desearlize(lists[i]);
        }
    }
}