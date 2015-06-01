using DesktopToastsSample.ShellHelpers;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using MS.WindowsAPICodePack.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace HelloService
{
    class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            var program = new Program();
            var service = new Service1();
            
            if (!Environment.UserInteractive)
            {
                var servicesToRun = new ServiceBase[] { service };
                ServiceBase.Run(servicesToRun);
                return;
            }

            bool keepRunning = true;
            while (keepRunning)
            {
                Console.WriteLine("Running as a Console Application");
                Console.WriteLine(" 1. Run Service");
                Console.WriteLine(" 2. Stop Service");
                Console.WriteLine(" 3. Create Shortcut");
                Console.WriteLine(" 4. Remove Shortcut");
                Console.WriteLine(" 5. Exit");
                Console.Write("Enter Option: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.WriteLine("Starting Service");
                        service.Interactive = true;
                        service.Start(args);
                        Console.WriteLine("Service Start Returned - Press Enter To Exit");
                        Console.ReadLine();
                        break;
                    case "2":
                        Console.WriteLine("Stopping service...");
                        service.Stop();
                        Console.WriteLine("Stopped service");
                        break;  
                    case "3":
                        program.TryCreateShortcut();
                        break;
                    case "4":
                        program.TryRemoveShortcut();
                        break;
                    case "5":
                        keepRunning = false;
                        break;
                }
            }

            Console.WriteLine("Closing");
            Console.WriteLine("Press <Enter> to exit...");
            Console.ReadLine();
        }

        // In order to display toasts, a desktop application must have a shortcut on the Start menu.
        // Also, an AppUserModelID must be set on that shortcut.
        // The shortcut should be created as part of the installer. The following code shows how to create
        // a shortcut and assign an AppUserModelID using Windows APIs. You must download and include the 
        // Windows API Code Pack for Microsoft .NET Framework for this code to function
        //
        // Included in this project is a wxs file that be used with the WiX toolkit
        // to make an installer that creates the necessary shortcut. One or the other should be used.
        private bool TryCreateShortcut()
        {
            string shortcutPath = Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData) + Constants.APP_SHORTCUT;
            
            if (!File.Exists(shortcutPath))
            {
                InstallShortcut(shortcutPath);
                return true;
            }
            Console.WriteLine("Shortcut exists...");
            Console.WriteLine("Couldn't create shortcut");
            return false;
        }

        private bool TryRemoveShortcut()
        {
            string shortcutPath = Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData) + Constants.APP_SHORTCUT;

            if (File.Exists(shortcutPath))
            {
                try
                {
                    File.Delete(shortcutPath);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Can't delete shortcut - {0}", shortcutPath);
                    Console.WriteLine("Couldn't delete with {0} - {1}", ex.Message, ex.StackTrace);
                }
            }

            Console.WriteLine("Shortcut doesn't exist");
            return false;
        }


        private void InstallShortcut(String shortcutPath)
        {
            // Find the path to the current executable
            string exePath = Process.GetCurrentProcess().MainModule.FileName;
            IShellLinkW newShortcut = (IShellLinkW)new CShellLink();

            // Create a shortcut to the exe
            DesktopToastsSample.ShellHelpers.ErrorHelper.VerifySucceeded(newShortcut.SetPath(exePath));
            DesktopToastsSample.ShellHelpers.ErrorHelper.VerifySucceeded(newShortcut.SetArguments(""));

            // Open the shortcut property store, set the AppUserModelId property
            IPropertyStore newShortcutProperties = (IPropertyStore)newShortcut;

            using (PropVariant appId = new PropVariant(Constants.APP_ID))
            {
                DesktopToastsSample.ShellHelpers.ErrorHelper.VerifySucceeded(
                    newShortcutProperties.SetValue(SystemProperties.System.AppUserModel.ID, appId));

                DesktopToastsSample.ShellHelpers.ErrorHelper.VerifySucceeded(newShortcutProperties.Commit());
            }

            // Commit the shortcut to disk
            IPersistFile newShortcutSave = (IPersistFile)newShortcut;

            DesktopToastsSample.ShellHelpers.ErrorHelper.VerifySucceeded(newShortcutSave.Save(shortcutPath, true));
        }


    }
}
