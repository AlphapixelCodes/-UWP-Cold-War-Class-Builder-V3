using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cold_War_Class_Builder_V3
{
    public static class Settings
    {
        private static Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        private static bool autosave = GetStoredData<bool>("AutoSave", true), autobackup = GetStoredData<bool>("AutoBackUp", true), fancyperkdialog = GetStoredData<bool>("FancyPerkDialog", false), fancygunselectdialog = GetStoredData<bool>("FancyGunSelectDialog", true), fancyblueprintdialog = GetStoredData<bool>("FancyBlueprintDialog", true);
        public static bool AutoSave { get { return autosave; } set { autosave = value; UpdateSettings("AutoSave", value); } }
        public static bool AutoBackup { get { return autobackup; } set { autobackup = value; UpdateSettings("AutoBackUp", value); } }
        public static bool FancyPerkDialog { get { return fancyperkdialog; } set { fancyperkdialog = value; UpdateSettings("FancyPerkDialog", value); } }
        public static bool FancyBlueprintDialog { get { return fancyblueprintdialog; } set { fancyblueprintdialog = value; UpdateSettings("FancyBlueprintDialog", value); } }
        private static GunSelectStatic.Type gunselecttype = (GunSelectStatic.Type)GetStoredData<int>("GunSelectGuiType", 0);
        public static GunSelectStatic.Type GunSelectGuiType
        {
            get { return gunselecttype; }
            set
            {
                gunselecttype = value;
                UpdateSettings("GunSelectGuiType", (int)value);
            }
        }
        private static bool showallattachments = GetStoredData<bool>("ShowAllAttachments", false);
        public static bool ShowAllAttachments { get { return showallattachments; } set { showallattachments = value; UpdateSettings("ShowAllAttachments", value); } }
    
        private static void UpdateSettings(string v, object value)
        {
            localSettings.Values[v] = value;
            Debug.WriteLine("Setting data for: " + v + "=" + value);
        }

      
        private static T GetStoredData<T>(String name,T defaultValue)
        {
            object v = localSettings.Values[name];
            if (v == null)
            {
                Debug.WriteLine("Returning default setting for: "+name);
                return defaultValue;
            }
            else
            {
                return (T)v;
            }
        }
        public static void OpenSettingsDialog(MainPage main)
        {
            new SettingsDialog(main).ShowAsync();
        }
        private static void UpdateSettings()
        {
            String data = autosave + "\n" + autobackup+"\n"+fancyperkdialog+"\n"+fancygunselectdialog+"\n"+fancyblueprintdialog;
            FileManager.SaveData("Settings.txt", data);
        }
        public async static Task<bool> LoadSettings()
        {
            String text = await FileManager.GetFileText("Settings.txt");
            if (text.Length == 0)
            {//no settings found
                return false;
            }
            else
            {
                String[] vs = text.Split("\n");
                try
                {
                    
                    bool.TryParse(vs[0], out autosave);
                    if (vs.Length > 0)
                    {
                        bool.TryParse(vs[1], out autobackup);
                        if (vs.Length > 1)
                        {
                            bool.TryParse(vs[2], out fancyperkdialog);
                            if (vs.Length > 2)
                            {
                                bool.TryParse(vs[3], out fancygunselectdialog);
                                if (vs.Length>3)
                                {
                                    bool.TryParse(vs[4], out fancyblueprintdialog);
                                }
                            }
                        }
                    }
                }
                catch { }
                return true;
            }
        }
    }
}
