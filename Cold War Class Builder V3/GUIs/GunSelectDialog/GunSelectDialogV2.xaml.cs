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
    public sealed partial class GunSelectDialogV2 : ContentDialog, GunSelectInterface
    {
        public GunSelectDialogV2()
        {
            this.InitializeComponent();
        }
        public GunSelectDialogV2(bool startingtabAR, bool gunfighter)
        {
            InitializeComponent();
            if (gunfighter)
            {
                if (startingtabAR)
                    Pivot.SelectedItem = AssaultRifles;
                else
                    Pivot.SelectedItem = Secondaries;
                LoadPivotGuns(AssaultRifles, Data.AssaultRiflesList);
                LoadPivotGuns(SubMachineGuns, Data.SMGList);
                LoadPivotGuns(TacticalRifles, Data.TactRifleList);
                LoadPivotGuns(LightMachineGuns, Data.LMGList);
                LoadPivotGuns(SniperRifles, Data.SniperList);
                LoadPivotGuns(Secondaries, Data.SecondaryList);
            }
            else
            {
                if (startingtabAR)
                {
                    Pivot.Items.Remove(Secondaries);
                    LoadPivotGuns(AssaultRifles, Data.AssaultRiflesList);
                    LoadPivotGuns(SubMachineGuns, Data.SMGList);
                    LoadPivotGuns(TacticalRifles, Data.TactRifleList);
                    LoadPivotGuns(LightMachineGuns, Data.LMGList);
                    LoadPivotGuns(SniperRifles, Data.SniperList);

                }
                else
                {
                    Pivot.Items.Remove(AssaultRifles);
                    Pivot.Items.Remove(SubMachineGuns);
                    Pivot.Items.Remove(TacticalRifles);
                    Pivot.Items.Remove(LightMachineGuns);
                    Pivot.Items.Remove(SniperRifles);
                    Pivot.SelectedItem = Secondaries;
                    LoadPivotGuns(Secondaries, Data.SecondaryList);
                }

            }

        }
        private void LoadPivotGuns(PivotItem p,List<IconClass> guns)
        {
            StackPanel stack = ((StackPanel)((ScrollViewer)p.Content).Content);
            guns.ForEach(e => {
                GunControl gc = new GunControl(e);
                gc.Margin = new Thickness(0, 5, 0, 5);
                gc.ShowHideAllBoxes(true);
                gc.Tapped += GunControl_Tapped;
                stack.Children.Add(gc);
            });
        }

        private IconClass returnValue;
        private GunControl gunControl;
        public event EventHandler UpdateEvent;
        private void GunControl_Tapped(object sender, TappedRoutedEventArgs e)
        {
            returnValue = ((GunControl)sender).iconClass;

            UpdateEvent?.Invoke(this, new EventArgs());
            this.Hide();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
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
