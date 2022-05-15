using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class ComboBoxGunSelectDialog : ContentDialog,GunSelectInterface
    {
        public ComboBoxGunSelectDialog()
        {
            this.InitializeComponent();
            AddPrimaryGuns();
        }
        public event EventHandler UpdateEvent;
        public IconClass returnValue;
        public GunControl gunControl;
        private Dictionary<String, List<IconClass>> guns = new Dictionary<string, List<IconClass>>();
        private void AddPrimaryGuns()
        {
            guns.Add("Assault Rifles", Data.AssaultRiflesList);
            guns.Add("Sub Machine Guns", Data.SMGList);
            guns.Add("Tactical Rifles", Data.TactRifleList);
            guns.Add("Light Machine Guns", Data.LMGList);
            guns.Add("Snipers", Data.SniperList);
        }
        public ComboBoxGunSelectDialog(bool startingtabAR, bool gunfighter)
        {
            this.InitializeComponent();
            if (gunfighter)
            {
                AddPrimaryGuns();
                guns.Add("Secondaries", Data.SecondaryList);
            } else
            {
                if (startingtabAR)
                {
                    AddPrimaryGuns();
                }
                else
                    guns.Add("Secondaries", Data.SecondaryList);
                }
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            returnValue = (IconClass)((ComboItemExtended)GunCombo.SelectedItem).AttachedObject;
            UpdateEvent?.Invoke(this, new EventArgs());
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
        private void LoadGunCombo(List<IconClass> icons)
        {
            GunCombo.Items.Clear();
            icons.ForEach(e => {
                GunCombo.Items.Add(new ComboItemExtended(e, e.Name));
            });
            GunCombo.SelectedIndex = 0;
        }
        private void ContentDialog_Loaded(object sender, RoutedEventArgs e)
        {
            foreach(var a in guns.Keys.ToList())
            {
                typeCombo.Items.Add(new ComboItemExtended(guns[a], a));
            }
            typeCombo.SelectedIndex = 0;
        }

        public IconClass GetReturnValue()
        {
            return returnValue;
        }
        public GunControl GetGunControl()
        {
            return gunControl;
        }

        private class ComboItemExtended : ComboBoxItem
        {
            public ComboItemExtended(object attachedObject,string name)
            {
                AttachedObject = attachedObject;
                this.Content = name;
            }

            public object AttachedObject { get; }
        }



        private void typeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadGunCombo(((List<IconClass>)((ComboItemExtended)typeCombo.SelectedItem).AttachedObject));
        }

        public void SetGunControl(GunControl x)
        {
            gunControl = x;
        }
    }
}
