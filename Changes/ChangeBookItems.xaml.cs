using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Course_Record.Changes
{
    public sealed partial class ChangeBookItems : Page
    {
        public ChangeBookItems()
        {
            this.InitializeComponent();
            CourseList.Items.Add("Biology Laboratory");
            CourseList.Items.Add("Electronic Science");
            CourseList.Items.Add("General Biology");
            CourseList.Items.Add("Mathemaics II");
            CourseList.Items.Add("Probability and Stats");
            CourseList.Items.Add("Tech Report Writing");
            CourseList.Items.Add("Workshop");
        }
    }
}
