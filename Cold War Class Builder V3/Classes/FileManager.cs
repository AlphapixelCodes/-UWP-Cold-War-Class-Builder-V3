using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Core.Preview;
using Windows.UI.Xaml.Controls;

namespace Cold_War_Class_Builder_V3
{
    public static class FileManager
    {
        private static string DataFileName="Data.txt",BackUpFileName="Backup.txt";
     //   private static bool autosave = true,autobackup=true;
      //  public static bool AutoSave { get { return autosave; }set { autosave = value; UpdateSettings(); }}
        //public static bool AutoBackup { get { return autobackup; } set { autobackup = value; UpdateSettings(); } }
        private async static Task<StorageFile> GetFile(String filename)
        {
            var item = await ApplicationData.Current.LocalFolder.GetFileAsync(filename);
            return item;
        }
        public async static void SaveData(String filename,String Data)
        {
            StorageFolder storageFolder =
            ApplicationData.Current.LocalFolder;
            StorageFile sampleFile =await storageFolder.CreateFileAsync(filename+".temp",CreationCollisionOption.ReplaceExisting);
            try
            {
                await FileIO.WriteTextAsync(sampleFile, Data);
                await sampleFile.RenameAsync(filename, NameCollisionOption.ReplaceExisting);
            }
            catch { }
        }
        public async static Task<String> GetFileText(String filename)
        {
            try
            {
                StorageFile storageItem = await GetFile(filename);
                if (storageItem == null)
                    return "";
                return await FileIO.ReadTextAsync(storageItem);
            }
            catch
            {
                return "";
            }
        }
        public static string GetSaveString()
        {
            return ClassBuild.GetSaveString() + "\n" + CustomBlueprint.GetSaveString()+"\n"+ AttachmentClass.GetCustomSaveString();
        }
        public static void LoadSaveString(String s)
        {
            string[] vs = s.Split("\n");
            ClassBuild.LoadBuilds(vs[0]);
            CustomBlueprint.LoadBlueprints(vs[1]);
            AttachmentClass.LoadCustomFromString(vs[2]);
        }

        public async static Task<string> LoadData()
        {
            IStorageItem backup = await ApplicationData.Current.LocalFolder.TryGetItemAsync(BackUpFileName);
            IStorageItem data = await ApplicationData.Current.LocalFolder.TryGetItemAsync(DataFileName);
            Debug.WriteLine("Backup.txt date: " + backup);
            Debug.WriteLine("Data.txt date: " + data);
            if (backup != null)
            {
                //Debug.WriteLine("Backup: " + backup.DateCreated);
                if (data != null)
                {
                    //  Debug.WriteLine("Data: " + data.DateCreated);
                    if (data.DateCreated.CompareTo(backup.DateCreated) < 1)
                    {
                        ContentDialog diag = new ContentDialog
                        {
                            Title = "Unsaved Backup Detected",
                            Content = "Would you like to load the backup?",
                            SecondaryButtonText = "Delete",
                            PrimaryButtonText = "Recover"
                        };
                        ContentDialogResult result = await diag.ShowAsync();
                        if (result.Equals(ContentDialogResult.Primary))
                        {
                            //StorageFile storageFile = await ApplicationData.Current.LocalFolder.GetFileAsync(BackUpFileName);
                            //string v = await FileIO.ReadTextAsync(storageFile);
                            LoadSaveString(await GetFileText(BackUpFileName));
                            return "Backup 1";
                        }else if (result.Equals(ContentDialogResult.Secondary))
                        {
                            await (await GetFile(BackUpFileName)).DeleteAsync();
                            LoadSaveString(await GetFileText(DataFileName));
                            return "Data4-deleting backup";
                        }
                        else
                        {//data and backup, but dont load backup
                            LoadSaveString(await GetFileText(DataFileName));
                            return "Data1";
                        }
                    }
                    else
                    {
                        LoadSaveString(await GetFileText(DataFileName));
                        return "Data3";
                    }
                }
                else
                {//backup but no save
                    LoadSaveString(await GetFileText(BackUpFileName));
                    return "Backup 2";
                }
            }
            else if (data != null)
            {//save, no backup
                LoadSaveString(await GetFileText(DataFileName));
                return "Data2";
            }
            else
            {
                ClassBuild.addNewDefault();
                return "Default";
            }
        }

        public async static Task<String> OpenFileDialog()
        {
                var picker = new Windows.Storage.Pickers.FileOpenPicker();
                picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
                picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;
                picker.FileTypeFilter.Add(".txt");
                StorageFile file = await picker.PickSingleFileAsync();
                if (file != null)
                {                
                    string v = await FileIO.ReadTextAsync(file);
                    return v;
                }
                else
                {
                    return "";
                }
        }
        public async static void ExportFileDialog()
        {
            var savePicker = new Windows.Storage.Pickers.FileSavePicker();
            savePicker.SuggestedStartLocation =
                Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            savePicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;
            savePicker.FileTypeChoices.Add("Plain Text", new List<string>() { ".txt" });
            savePicker.SuggestedFileName = "Data"; 
            StorageFile sf = await savePicker.PickSaveFileAsync();
            if(sf!=null)
                FileIO.WriteTextAsync(sf,GetSaveString());
        }

       
        public static void PerformAutoSave()
        {
            if (Settings.AutoSave)
            {
                SaveData(DataFileName,GetSaveString());
            }
            else if(Settings.AutoBackup)
            {
                SaveData(BackUpFileName, GetSaveString());
            }
        }

     
    }
}

