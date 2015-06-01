using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;

namespace HelloService
{
    public class ToastManager
    {
        // Create and show the toast.
        // See the "Toasts" sample for more detail on what can be done with toasts
        public void SetupToast(string value)
        {

            // Get a toast XML template
            Windows.Data.Xml.Dom.XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText04);

            // Fill in the text elements
            Windows.Data.Xml.Dom.XmlNodeList stringElements = toastXml.GetElementsByTagName("text");
            for (int i = 0; i < stringElements.Length; i++)
            {
                stringElements[i].AppendChild(toastXml.CreateTextNode("Line " + i));
            }

            // Specify the absolute path to an image

            string imagePath = "file:///" + Path.GetFullPath("toastImageAndText.png");

            Windows.Data.Xml.Dom.XmlNodeList imageElements = toastXml.GetElementsByTagName("image");
            imageElements[0].Attributes.GetNamedItem("src").NodeValue = imagePath;

            // Create the toast and attach event listeners
            ToastNotification toast = new ToastNotification(toastXml);
            toast.Activated += ToastActivated;
            toast.Dismissed += ToastDismissed;
            toast.Failed += ToastFailed;

            // Show the toast. Be sure to specify the AppUserModelId on your application's shortcut!
            ToastNotificationManager.CreateToastNotifier(Constants.APP_ID).Show(toast);
        }

        private void ToastActivated(ToastNotification sender, object e)
        {
            //Dispatcher.Invoke(() =>
            // {
            //     Activate();
            //     Output.Text = "The user activated the toast.";
            // });
        }

        private void ToastDismissed(ToastNotification sender, ToastDismissedEventArgs e)
        {
            String outputText = "";
            switch (e.Reason)
            {
                case ToastDismissalReason.ApplicationHidden:
                    outputText = "The app hid the toast using ToastNotifier.Hide";
                    break;
                case ToastDismissalReason.UserCanceled:
                    outputText = "The user dismissed the toast";
                    break;
                case ToastDismissalReason.TimedOut:
                    outputText = "The toast has timed out";
                    break;
            }

            //Dispatcher.Invoke(() =>
            //{
            //    Output.Text = outputText;
            //});
        }

        private void ToastFailed(ToastNotification sender, ToastFailedEventArgs e)
        {
            //Dispatcher.Invoke(() =>
            //{
            //    Output.Text = "The toast encountered an error.";
            //});
        }
    }
}
