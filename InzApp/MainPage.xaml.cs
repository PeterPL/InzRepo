using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using InzApp.Resources;
using System.Windows.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.IO;


namespace InzApp
{
    public partial class MainPage : PhoneApplicationPage
    {

        Microphone microphone = Microphone.Default;
        byte[] buffer;
        MemoryStream stream = new MemoryStream();
        SoundEffect sEffect;
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            DispatcherTimer dispTimer = new DispatcherTimer();
            dispTimer.Interval = TimeSpan.FromMilliseconds(50);
            dispTimer.Tick += delegate { try { FrameworkDispatcher.Update(); } catch { } };
            dispTimer.Start();

            microphone.BufferReady += new EventHandler<EventArgs>(microphone_BufferReady);
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void microphone_BufferReady(object sender, EventArgs e)
        {
            microphone.GetData(buffer);
            stream.Write(buffer, 0, buffer.Length);
        }

        private void startRecording(object sender, RoutedEventArgs e)
        {
            microphone.BufferDuration = TimeSpan.FromMilliseconds(100);
            buffer = new byte[microphone.GetSampleSizeInBytes(microphone.BufferDuration)];
            microphone.Start();
        }

        private void stopRecording(object sender, RoutedEventArgs e)
        {
            microphone.Stop();
        }


        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}