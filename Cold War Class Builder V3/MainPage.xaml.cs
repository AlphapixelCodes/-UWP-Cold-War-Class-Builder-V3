using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Core.Preview;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Cold_War_Class_Builder_V3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public enum SearchSettings { Name, Favorite, None };
        private SearchSettings search = SearchSettings.None;
        private string SearchNameString = "";
        public ClassControl classControl;
        
        public MainPage()
        {
            this.InitializeComponent();
        }
        public void RefreshBuildList()
        {
            bool CurrentBuildIsInList = false;
            ClassBuildsStack.Children.Clear();
            List<ClassBuild> builds = new List<ClassBuild>();
            switch (search)
            {
                case SearchSettings.None:
                    builds = ClassBuild.Builds;
                    break;
                case SearchSettings.Favorite:
                    builds = ClassBuild.Builds.FindAll(e => e.isFavorite);
                    break;
                case SearchSettings.Name:
                    builds = ClassBuild.Builds.FindAll(e => e.Name.Contains(SearchNameString.ToUpper()));
                    break;
            }
            builds.ForEach(e => {
                ClassTitleControl ctc = new ClassTitleControl(e);
                ctc.Tapped += ClassTitleControl_Tapped;
                ctc.FlyoutMenuItem_Tapped += ClassTitleControl_FlyoutMenuItem_Tapped;
                if (e.Equals(ClassBuild.CurrentBuild))
                {
                    ctc.Selected = true;
                    CurrentBuildIsInList = true;
                }
                ClassBuildsStack.Children.Add(ctc);
            });
            if (!CurrentBuildIsInList)
            {
                ClassTitleControl uIElement = (ClassTitleControl)ClassBuildsStack.Children.FirstOrDefault();
                if (uIElement != null)
                {
                    uIElement.Selected = true;
                    gc.LoadClassBuild(uIElement.build);
                    ClassBuild.CurrentBuild = uIElement.build;
                }
            }
        }

        private void ClassTitleControl_FlyoutMenuItem_Tapped(object sender, EventArgs e)
        {
            FlyoutMenuItemExtended fmie = (FlyoutMenuItemExtended) sender;
            ClassTitleControl ctc = (ClassTitleControl)fmie.AttachedObject;
            switch (fmie.Text)
            {
                case "Delete":
                    ClassBuild.CurrentBuild = ClassBuild.Builds.FirstOrDefault();
                    if (ClassBuild.CurrentBuild != null)
                    {
                        gc.LoadClassBuild(ClassBuild.CurrentBuild);
                    }
                    ClassBuild.Builds.Remove(ctc.build);
                    if (ClassBuild.Builds.Count == 0)
                    {
                        gc.IsEnabled = false;
                        gc.SetBuildName( "None selected");
                    }
                    RefreshBuildList();
                    break;
                case "Toggle Favorite":
                    ctc.build.isFavorite = !ctc.build.isFavorite;
                    ctc.isFavorite = ctc.build.isFavorite;
                    ClassBuild.HasUpdated = true;
                    break;
                case "Rename":
                    NameDialog nd = new NameDialog();
                    nd.SetTextBoxText(ctc.Title);
                    nd.Confirmation_Event += (dialog, l) =>
                    {
                        string v = nd.ReturnValue.ToUpper();
                        if (v.Length < 3 || v.Length>23)
                            nd.ErrorTextToWrite = "Name must be between 3 and 23 characters";
                        else if (Regex.IsMatch(v, "[^A-Z 0-9-]+"))
                        {
                            nd.ErrorTextToWrite = "Name can only contain A-Z 0-9 space and -";
                        } else if (ClassBuild.Builds.Any(z=>z.Name.Equals(v)))
                        {
                            nd.ErrorTextToWrite = "Class name already exists";
                        }
                        else
                        {
                            nd.Hide();
                            ctc.build.Name = v;
                            if(ctc.build.Equals(ClassBuild.CurrentBuild))
                                gc.SetBuildName(v);
                            ctc.setText(v);
                            ClassBuild.HasUpdated = true;
                        }
                    };
                    nd.ShowAsync();
                    break;
                case "Duplicate":
                    ClassBuild b = ctc.build.Clone();
                    ClassBuild.Builds.Add(b);
                    ClassBuild.CurrentBuild = b;
                    gc.LoadClassBuild(b);
                    RefreshBuildList();
                   ClassBuild.HasUpdated = true;
                    break;       
            }
            //ClassBuild.HasUpdated = true;
        }
        private void ClassTitleControl_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ClassTitleControl ctc = ((ClassTitleControl)sender);
            ClassBuildsStack.Children.OfType<ClassTitleControl>().ToList().ForEach(a => a.Selected = false);
            ctc.Selected = true;

            ClassBuild.CurrentBuild = ctc.build;
            gc.LoadClassBuild(ClassBuild.CurrentBuild);

        }

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Data.Load();
           // await new MessageDialog("Remove AttachmentDialog not v2, start porgram, add new class, set primary gun to a new gun and then save and exit, reopen program and the gun choice isnt saved fix this").ShowAsync();
            RemoveCustomAttachmentsMenuItem.Click += (a,b)=>new RemoveAttachmentDialog().ShowAsync();
            classControl = gc;

            gc.IsEnabled = false;
            
            String s = await FileManager.LoadData();
           // await Settings.LoadSettings();
            //auto save stuff start//
            SettingsMenuItem.Click += (a, b) => Settings.OpenSettingsDialog(this);
           
            
            ClassBuild.CurrentBuild = ClassBuild.Builds[0];
           // Debug.WriteLine(ClassBuild.CurrentBuild.Name);
            gc.LoadClassBuild(ClassBuild.CurrentBuild);
            RefreshBuildList();
            gc.IsEnabled = true;
        }

        

        private void MenuItem_AddClass_Click(object sender, RoutedEventArgs e)
        {
            gc.LoadClassBuild(ClassBuild.addNewDefault());
            RefreshBuildList();
            gc.IsEnabled = true;
        }

        private void SearchMenuItem_Click(object sender, RoutedEventArgs e)
        {

            ToggleMenuFlyoutItem s = (ToggleMenuFlyoutItem) sender;
           
          
            bool haschanged = false;
            Debug.WriteLine(s.Text);
            switch (s.Text)
            {
                case "Favorites":
                    search = SearchSettings.Favorite;
                    haschanged = true;
                    break;
                case "None":
                    search = SearchSettings.None;
                    haschanged = true;
                    break;
                case "Name":
                    NameDialog nameDialog = new NameDialog();
                    nameDialog.Title = "Search Name";
                    nameDialog.Confirmation_Event += (a, b) =>
                    {
                        NameDialog diag = (NameDialog)a;
                        if (diag.ReturnValue=="") {
                            diag.ErrorTextToWrite = "Search can't be empty";
                        }
                        else
                        {
                            haschanged = true;
                            SearchNameString =diag.ReturnValue.ToUpper();
                            search = SearchSettings.Name;
                            diag.Hide();
                            RefreshBuildList();
                        }
                    };
                    nameDialog.ShowAsync();
                    break;
            }
            if (haschanged)
            {
                foreach (ToggleMenuFlyoutItem child in SearchMenuItem.Items.OfType<ToggleMenuFlyoutItem>())
                {
                    child.IsChecked = false;
                }
                switch (search)
                {
                    case SearchSettings.Favorite:
                        SearchMenuFlyoutFavorite.IsChecked = true;
                        break;
                    case SearchSettings.Name:
                        SearchMenuFlyoutName.IsChecked = true;
                        break;
                    case SearchSettings.None:
                        SearchMenuFlyoutNone.IsChecked = true;
                        break;
                }
            }
            RefreshBuildList();
        }

        private async void ViewCustomBuildsMenuFlyout_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Settings.FancyBlueprintDialog)
                    new CustomBlueprintsDialogV2().ShowAsync();
                else
                    new CustomBlueprintsDialog().ShowAsync();
            }
            catch { }
        }



        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            //Debug.WriteLine(CustomBlueprint.GetSaveString());
            FileManager.SaveData("Data.txt",FileManager.GetSaveString());
            
            ContentDialog cd = new ContentDialog();
            cd.Title = "Save";
            cd.Content = "Saved Sucessfully";
            cd.PrimaryButtonText = "";
            cd.SecondaryButtonText = "Close";
            cd.ShowAsync();
        }

        private async void OpenFile_Click(object sender, RoutedEventArgs e)
        {
             string v = await FileManager.OpenFileDialog();
             if (v == "")
                 return;
             FileManager.LoadSaveString(v);
            RefreshBuildList();

        }

        private void ExportFile_Click(object sender, RoutedEventArgs e)
        {
            FileManager.ExportFileDialog();
        }
       
        private async void MenuItemAddBlueprint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                gc.IsEnabled = false;
                await new AddBlueprintDialog().ShowAsync();
                gc.IsEnabled = true;
            }
            catch
            {  }
        }

     

        private void MenuResourcesUsedFlyout_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                new ResourcesDialog().ShowAsync();
            }
            catch { }
        }

        private void YoutubeChancel_Clicked(object sender, RoutedEventArgs e)
        {
            Links.OpenLink(Links.YoutubeChannel);
        }
    }
}
