using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Course_Record.Frames
{
    public static class Extensions
    {
        public static string[] RemoveWhiteSpaces(this string x)
        {
            return x.Split(new char[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }

    public static class CommandListToProcess
    {
        public static void Echo(string input, TextBlock output)
        {
            string[] inp = input.Split(' ');

            if (inp.Length < 2)
                throw new Exception("Command " + input + "is not complete.\nCoreect format is : echo <string>\n");

            for (int i = 1; i < inp.Length; ++i)
                output.Text += inp[i] + " ";

            output.Text += "\n";
        }

        public static void Add(string input, TextBlock output)
        {
            throw new Exception("Command not implemented yet...\n");
        }

        public static void Remove(string input, TextBlock output)
        {
            string syntax = @"remove book <course_index> <book index>";
            string[] inp = input.RemoveWhiteSpaces();

            if (inp.Length != 4)
                throw new Exception("Input '" + input.ToLower() + "' does not match with '" + syntax + "'.\n");

            switch (inp[1])
            {
                case "book":
                    Books.Remove(int.Parse(inp[2]), int.Parse(inp[3]));
                    break;

                default:
                    throw new Exception("Not Implemented yet...\n");
            }
        }

        public static void Clear(string input, TextBlock output) => output.Text = "";
    }

    public sealed partial class Command_Prompt : Page
    {
        Queue<string> commands = new Queue<string>();
        delegate void Command(string x, TextBlock output);

        readonly string[] ValidCommands = {
            "add",
            "clear",
            "echo",
            "remove"};

        Command[] com_list = new Command[] {
            new Command(CommandListToProcess.Add),
            new Command(CommandListToProcess.Clear),
            new Command(CommandListToProcess.Echo),
            new Command(CommandListToProcess.Remove)
        };

        static bool ChangedByMe = false;

        public Command_Prompt()
        {
            this.InitializeComponent();
        }

        private string GetMainCommand(string x) => x.RemoveWhiteSpaces()[0];

        private void Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ChangedByMe)
                return;
            if (Input.Text.EndsWith("\r"))
            {
                commands.Enqueue(Input.Text);
                ChangedByMe = true;
                Input.Text = "";
                ChangedByMe = false;
                ProcessCommand();
                Scroller.ScrollToVerticalOffset(Scroller.ScrollableHeight);
            }
        }

        private void ProcessCommand()
        {
            string com = commands.Dequeue();

            try
            {
                if (com.ToLower().RemoveWhiteSpaces()[0] == "exit")
                {
                    this.Frame.Navigate(typeof(MainPage));
                    return;
                }
                for (int i = 0; i < ValidCommands.Length; ++i)
                    if (ValidCommands[i] == com.ToLower().RemoveWhiteSpaces()[0])
                    {
                        try
                        {
                            com_list[i](com, Output);
                        }
                        catch (Exception e)
                        {
                            Output.Text += e.Message;
                        }
                        return;
                    }
            }
            catch
            {
                Output.Text += "Type again\n";
                return;
            }

            Output.Text += "Command " + com.RemoveWhiteSpaces()[0] + " not found..\n";
        }
    }
}