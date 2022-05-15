using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public sealed partial class GunSelectDialog : ContentDialog, GunSelectInterface
    {
        public GunSelectDialog()
        {
            this.InitializeComponent();
            //his.Width=Window.Current.Bounds.Width;
            this.Title = "Select Gun";
            Combo.Items.Add("ASSAULT RIFLES");
            Combo.Items.Add("SUB MACHINE GUNS");
            Combo.Items.Add("TACTICAL RIFLES");
            Combo.Items.Add("LIGHT MACHINE GUNS");
            Combo.Items.Add("SNIPER RIFLES");
            Combo.Items.Add("SECONDARIES");
            Combo.SelectedItem = "ASSAULT RIFLES";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startingtab">is starting tab Assault rifles</param>
        /// <param name="gunfighter">if starting tab is secondary and showextras</param>
        public GunSelectDialog(bool startingtabAR,bool gunfighter)
        {
            InitializeComponent();
            if (gunfighter)
            {
                Combo.Items.Add("ASSAULT RIFLES");
                Combo.Items.Add("SUB MACHINE GUNS");
                Combo.Items.Add("TACTICAL RIFLES");
                Combo.Items.Add("LIGHT MACHINE GUNS");
                Combo.Items.Add("SNIPER RIFLES");
                Combo.Items.Add("SECONDARIES");
                if (startingtabAR)
                    Combo.SelectedItem = "ASSAULT RIFLES";
                else
                    Combo.SelectedItem = "SECONDARIES";
            }
            else
            {
                if (startingtabAR)
                {
                    Combo.Items.Add("ASSAULT RIFLES");
                    Combo.Items.Add("SUB MACHINE GUNS");
                    Combo.Items.Add("TACTICAL RIFLES");
                    Combo.Items.Add("LIGHT MACHINE GUNS");
                    Combo.Items.Add("SNIPER RIFLES");
                    Combo.SelectedItem = "ASSAULT RIFLES";
                }
                else
                {
                    Combo.Items.Add("SECONDARIES");
                    Combo.SelectedItem = "SECONDARIES";
                }

            }

        }
        public event EventHandler UpdateEvent;
        private IconClass returnValue;
        private GunControl gunControl;
        private void LoadGuns(List<IconClass> gs)
        {
            StackControl.Children.Clear();
            gs.ForEach(e=>{
                GunControl gc = new GunControl(e);
                gc.Margin = new Thickness(0, 5, 0, 5);
                gc.ShowHideAllBoxes(true);
                gc.Tapped += GunControl_Tapped;
                StackControl.Children.Add(gc);
            });
        }
        
        private void GunControl_Tapped(object sender, TappedRoutedEventArgs e)
        {
            returnValue = ((GunControl)sender).iconClass;
            UpdateEvent?.Invoke(this, new EventArgs());
            this.Hide();
        }

        private void StackControl_Loaded(object sender, RoutedEventArgs e)
        {
            Combo_SelectionChanged("", new SelectionChangedEventArgs(new List<object>(),new List<Object>()));
        }

        private void Combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (Combo.SelectedItem)
            {
                case "ASSAULT RIFLES":
                    LoadGuns(Data.AssaultRiflesList);
                    break;
                case "SUB MACHINE GUNS":
                    LoadGuns(Data.SMGList);
                    break;
                case "TACTICAL RIFLES":
                    LoadGuns(Data.TactRifleList);
                    break;
                case "LIGHT MACHINE GUNS":
                    LoadGuns(Data.LMGList);
                    break;
                case "SNIPER RIFLES":
                    LoadGuns(Data.SniperList);
                    break;
                case "SECONDARIES":
                    LoadGuns(Data.SecondaryList);
                    break;
            }
        }

        public IconClass GetReturnValue()
        {
            return returnValue;
        }

        public GunControl GetGunControl()
        {
            return gunControl;
        }

        public void SetGunControl(GunControl x)
        {
            gunControl = x;
        }
    }
}
