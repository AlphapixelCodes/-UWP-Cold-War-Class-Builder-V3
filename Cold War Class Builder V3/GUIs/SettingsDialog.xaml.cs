using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Cold_War_Class_Builder_V3
{
    public sealed partial class SettingsDialog : ContentDialog
    {
        public SettingsDialog()
        {
            this.InitializeComponent();
        }
        private MainPage Home;
        public SettingsDialog(MainPage home)
        {
            Home = home;
            InitializeComponent();

        }
        public string GetAppVersion()
        {
            Package package = Package.Current;
            PackageId packageId = package.Id;
            PackageVersion version = packageId.Version;

            return string.Format("Version {0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);
        }
        private void ContentDialog_Loaded(object sender, RoutedEventArgs e)
        {
            Autosave.IsOn = Settings.AutoSave;
            Autosave.Toggled += (a, b) => Settings.AutoSave = Autosave.IsOn;
            
            Autobackup.IsOn = Settings.AutoBackup;
            Autobackup.Toggled += (a, b) => Settings.AutoBackup = Autobackup.IsOn;

            FancyPerkDialog.IsOn = Settings.FancyPerkDialog;
            FancyPerkDialog.Toggled += (a, b) => Settings.FancyPerkDialog = FancyPerkDialog.IsOn;

         //   FancyGunSelectDialog.IsOn = Settings.FancyGunSelectDialog;
         //   FancyGunSelectDialog.Toggled += (a, b) => Settings.FancyGunSelectDialog = FancyGunSelectDialog.IsOn;

            FancyBlueprintDialog.IsOn = Settings.FancyBlueprintDialog;
            FancyBlueprintDialog.Toggled+= (a, b) =>  Settings.FancyBlueprintDialog = FancyBlueprintDialog.IsOn;

            foreach (var item in Enum.GetNames(GunSelectStatic.Type.Fancy.GetType())){
                GunSelectCombo.Items.Add(item + " Dialog");
            }
            GunSelectCombo.SelectedIndex = (int)Settings.GunSelectGuiType;
            GunSelectCombo.SelectionChanged += (a, b) => Settings.GunSelectGuiType=(GunSelectStatic.Type)GunSelectCombo.SelectedIndex;

            ShowAllAttachments.IsOn = Settings.ShowAllAttachments;
            ShowAllAttachments.Toggled += (a, b) => Settings.ShowAllAttachments = ShowAllAttachments.IsOn;
            Version.Text = GetAppVersion();

        }

        private async void ClearAllDataButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ContentDialog cd = new ContentDialog() { 
            Title="Clear All Data",
            Content="Are you sure you want to clear all data?",
            PrimaryButtonText="Clear Data",
            SecondaryButtonText="Cancel"
            };
            this.Hide();
            ContentDialogResult result = await cd.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                AttachmentClass.CustomAttachments.Clear();
                CustomBlueprint.CustomBlueprints.Clear();
                ClassBuild.Builds.Clear();
                ClassBuild.addNewDefault();
                    Home.RefreshBuildList();
                    Home.classControl.LoadClassBuild(ClassBuild.CurrentBuild);
                //new SettingsDialog(Home).ShowAsync();
            }

        }
    }
}
