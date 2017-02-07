using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystemWatcher
{
    class Program
    {

        [STAThread]
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Write 'this' to use the folder where this programm is located.");
            Console.WriteLine("Write 'select' to open the 'select folder' menu.");

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select the folder or hit cancel to select the folder where this file is located.";

            String cmd = Console.ReadLine();

            if (cmd == "this")
            {
                fbd.SelectedPath = Path.GetDirectoryName(Application.ExecutablePath);
            }
            else if (cmd == "select")
            {
                if (fbd.ShowDialog() != DialogResult.OK)
                {
                    fbd.SelectedPath = Path.GetDirectoryName(Application.ExecutablePath);
                }
            }

            Console.ForegroundColor = ConsoleColor.DarkCyan;

            Console.WriteLine("Selected path: " + fbd.SelectedPath);
            Console.WriteLine("");
            Console.ResetColor();

            FileSystemWatcher watcher = new FileSystemWatcher();

            watcher.Path = fbd.SelectedPath;
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;

            watcher.Changed += Watcher_Changed;
            watcher.Deleted += Watcher_Changed;
            watcher.Renamed += Watcher_Changed;
            watcher.Created += Watcher_Changed;

            new System.Threading.AutoResetEvent(false).WaitOne();

        }

        private static void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            String type = e.ChangeType.ToString();
            
            //Insert simple switch
            switch (type)
            {
                case "Created":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "Deleted":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "Changed":
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case "Renamed":
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    break;
                default:
                    break;

            }

            Console.WriteLine(e.ChangeType + " | " + e.FullPath);
        }
    }
}
