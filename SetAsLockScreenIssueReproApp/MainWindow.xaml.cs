using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System.UserProfile;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SetAsLockScreenIssueReproApp
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private async void myButton_Click(object sender, RoutedEventArgs e)
        {
            bool success = false;
            if (UserProfilePersonalizationSettings.IsSupported())
            {
                var storageFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("LockScreenRepoAppLockScreen", CreationCollisionOption.OpenIfExists);

                StorageFile file = await StorageFile.GetFileFromPathAsync(@"INSERT FILE PATH HERE");
                if (file == null)
                {
                    myTextBlock.Text = "Invalid file path";
                    return;
                }

                var copy = await file.CopyAsync(storageFolder, file.Name, NameCollisionOption.ReplaceExisting);

                UserProfilePersonalizationSettings profileSettings = UserProfilePersonalizationSettings.Current;
                success = await profileSettings.TrySetLockScreenImageAsync(copy);
            }
            myTextBlock.Text = success ? "Success" : "Failure";
        }
    }
}
