﻿using System;
using System.Diagnostics;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.ApplicationSettings;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.WindowsAzure.MobileServices;

namespace MobileServicesExample
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        // TODO - Update the values below with your Mobile Services URL and Application Key values
        // http://go.microsoft.com/fwlink/?LinkId=290986&clcid=0x409
        public static MobileServiceClient WinRTByExampleBookClient =
            new MobileServiceClient(
                "https://MOBILESERVICENAME.azure-mobile.net/",
                "APPLICATION KEY");

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();
            Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {

#if DEBUG
            if (Debugger.IsAttached)
            {
                //this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            var rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // Check to be sure a "real" Mobile Service URI has been provided
                var mobileServicesClientUri = WinRTByExampleBookClient.ApplicationUri;
                if ("MOBILESERVICENAME.azure-mobile.net".Equals(mobileServicesClientUri.Host, StringComparison.OrdinalIgnoreCase))
                {
                    const String message = "This sample app will not run until it actual Mobile Services URL and App Key values are provided.  "
                                           + "Please update the WinRTByExampleBookClient value in the app.xaml.cs file, then rebuild and rerun the app.";
                    var dialog = new MessageDialog(message, "Launch Error");
                    dialog.ShowAsync();
                }
                else
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    if (!rootFrame.Navigate(typeof (SubscribersPage), e.Arguments))
                    {
                        throw new Exception("Failed to create initial page");
                    }
                }
            }
            // Ensure the current window is active
            Window.Current.Activate();
            //// http://go.microsoft.com/fwlink/?LinkId=290986&clcid=0x409
            //WAMSExample.WinRTByExampleBookPush.UploadChannel();
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }

        protected override void OnWindowCreated(WindowCreatedEventArgs args)
        {
            base.OnWindowCreated(args);
            SettingsPane.GetForCurrentView().CommandsRequested += (sender, eventArgs) =>
            {
                var credentialsSetting = new SettingsCommand("AppUpdateCredentials", "Credentials",
                        handler =>
                        {
                            var flyout = new CredentialsFlyout();
                            flyout.Show();
                        });
                eventArgs.Request.ApplicationCommands.Add(credentialsSetting);
            };
        }
    }
}
