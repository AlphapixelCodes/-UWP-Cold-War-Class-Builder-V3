using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
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
    public sealed partial class CustomBlueprintsDialogV2 : ContentDialog
    {
        public CustomBlueprintsDialogV2()
        {
            this.InitializeComponent();
            Builds = CustomBlueprint.CustomBlueprints;
        }
        private List<CustomBlueprint> Builds;
        public GunControl GunClass;
        public string StartingTab="";

        public event EventHandler BlueprintSelected_Event;
        public CustomBlueprintsDialogV2(List<CustomBlueprint> builds)
        {
            Builds = builds;
            InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
        private PivotItem GetGunPivot(IconClass gun)
        {
            if (Data.AssaultRiflesList.Contains(gun))
                return AssaultRifles;
            else if (Data.SMGList.Contains(gun))
            {
                return SubMachineGuns;
            }
            else if (Data.TactRifleList.Contains(gun))
            {
                return TacticalRifles;
            }
            else if (Data.LMGList.Contains(gun))
            {
                return LightMachineGuns;
            }
            else if (Data.SniperList.Contains(gun))
            {
                return SniperRifles;
            }
            else
            {
                return Secondaries;
            }

        }
        private StackPanel GetStackFromPivot(PivotItem pivot)
        {
            return (StackPanel)((ScrollViewer)pivot.Content).Content;
        }
        private void AddBuildGroup(List<CustomBlueprint> group)
        {
            PivotItem pivot = GetGunPivot(group[0].gunIcon);
            StackPanel stack = GetStackFromPivot(pivot);
            TextBlock tb = new TextBlock();
            tb.Text = group[0].gunIcon.Name;
            tb.TextAlignment = TextAlignment.Center;
            tb.FontSize = 18;
            stack.Children.Add(tb);
            VariableSizedWrapGrid vswg = new VariableSizedWrapGrid();
            stack.Children.Add(vswg);
            foreach (var item in group)
            {
                CustomSelectControl sc = new CustomSelectControl(item.gunIcon, tb);
                sc.AttachedObject = item;
                sc.Margin = new Thickness(3, 3, 3, 3);
                sc.Tapped += SelectControl_Tapped;
                Debug.WriteLine("CustomBlueprintsDialogV2.AddBuildGroup: " + item.Name);
                vswg.Children.Add(sc);
                vswg.Orientation=Orientation.Horizontal;
                
                sc.setText(item.Name);
                MenuFlyout flyout = new MenuFlyout();
                foreach (var a in new string[] { "Delete", "Rename", "Edit Attachments",  "View" })
                {
                    MenuFlyoutItem mfi = new FlyoutMenuItemExtended(a, sc);
                    flyout.Items.Add(mfi);
                    mfi.Click += MenuFlyout_Click;
                }
                sc.RightTapped += (a, b) => flyout.ShowAt(sc);
            }
        }
        private void SelectControl_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (BlueprintSelected_Event == null)
            {
                SelectControl sc = (SelectControl)sender;
                ViewGunDialog vgd = new ViewGunDialog((CustomBlueprint)sc.AttachedObject);
                vgd.CloseEvent += (c, d) => { vgd.Hide(); ReLaunchThisGUI(); };
                vgd.Closed += SecondaryGuiClosing_Event;
                Hide();
                vgd.ShowAsync();
            }
            else
            {
                BlueprintSelected_Event?.Invoke(sender, new EventArgs());
            }
        }
        private void MenuFlyout_Click(object sender, RoutedEventArgs e)
        {
            FlyoutMenuItemExtended fmie = (FlyoutMenuItemExtended)sender;
            CustomSelectControl sc = (CustomSelectControl)fmie.AttachedObject;
            CustomBlueprint blue = (CustomBlueprint)sc.AttachedObject;
            //Debug.WriteLine(fmie.Text);
            switch (fmie.Text)
            {
                case "View":
                    ViewGunDialog vgd = new ViewGunDialog(blue);
                    vgd.CloseEvent += (a, b) => { vgd.Hide(); ReLaunchThisGUI(); };
                    vgd.Closed += SecondaryGuiClosing_Event;
                    Hide();
                    vgd.ShowAsync();
                    break;
                case "Rename":
                    NameDialog nd = new NameDialog();
                    nd.Closed += SecondaryGuiClosing_Event;
                    nd.Confirmation_Event += (a, b) => {
                        string v = nd.ReturnValue.ToUpper();
                        if (v.Length < 3 || v.Length > 27)
                            nd.ErrorTextToWrite = "Name must be between 3 and 23 characters";
                        else if (Regex.IsMatch(v, "[^A-Z 0-9-]+"))
                        {
                            nd.ErrorTextToWrite = "Name can only contain A-Z 0-9 space and -";
                        }
                        else if (CustomBlueprint.CustomBlueprints.Any(z => z.Name.Equals(v)))
                        {
                            nd.ErrorTextToWrite = "Blueprint already exists by that name";
                        }
                        else
                        {
                            nd.Hide();
                            blue.Name = v;
                            CustomBlueprint.HasUpdated = true;
                            ReLaunchThisGUI();
                        }
                    };
                    Hide();
                    nd.ShowAsync();
                    break;
                case "Delete":
                    CustomBlueprint.CustomBlueprints.Remove(blue);
                    sc.DeleteControl();
                    CustomBlueprint.HasUpdated = true;
                    break;
                case "Edit Attachments":
                    AttachmentDialogV2 ad = new AttachmentDialogV2(blue.Attachments, 20,blue.gunIcon);
                    ad.Closed += SecondaryGuiClosing_Event;
                    ad.AttachmentSetEvent += (a, b) => {
                        blue.Attachments = ((AttachmentDialogV2)a).returnAttachments;
                        CustomBlueprint.HasUpdated = true;
                        ad.Hide();
                        ReLaunchThisGUI();
                    };
                    this.Hide();
                    ad.ShowAsync();
                    break;
            }
        }
        private void SecondaryGuiClosing_Event(ContentDialog sender, ContentDialogClosedEventArgs args)
        {
            ReLaunchThisGUI();
        }

        private void ReLaunchThisGUI()
        {
            try
            {
                CustomBlueprintsDialogV2 cbd = new CustomBlueprintsDialogV2(Builds);
                cbd.GunClass = GunClass ?? null;
                cbd.StartingTab = ((PivotItem)Pivot.SelectedItem).Name;
                cbd.BlueprintSelected_Event = this.BlueprintSelected_Event;
                cbd.ShowAsync();
            }
            catch {
                Debug.WriteLine("EXECPTION IN CustomBlueprintsDialogV2.ReLaunchThisGui()");
            }
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            List<List<CustomBlueprint>> gunsGrouped = new List<List<CustomBlueprint>>();
            String currentGun = "";
            foreach(var b in Builds.OrderBy(a => a.gunIcon.Name))
            {
                if (b.gunIcon.Name != currentGun)
                {
                    gunsGrouped.Add(new List<CustomBlueprint>());
                    currentGun = b.gunIcon.Name;
                }
                gunsGrouped.Last().Add(b);
            }
            gunsGrouped.ForEach(z => AddBuildGroup(z));
            RemoveEmptyPivots();
            //set starting tab
            if (StartingTab != "")
            {
                object v = Pivot.FindName(StartingTab);
                if (v != null)
                {
                    //Debug.WriteLine("What"+v.GetType());
                    Pivot.SelectedItem = ((FrameworkElement)v);
                }
            }
        }
        private void RemoveEmptyPivots()
        {
            if (GetStackFromPivot(AssaultRifles).Children.Count == 0)
                ((Pivot)AssaultRifles.Parent).Items.Remove(AssaultRifles);
            if (GetStackFromPivot(SubMachineGuns).Children.Count == 0)
                ((Pivot)SubMachineGuns.Parent).Items.Remove(SubMachineGuns);
            if (GetStackFromPivot(TacticalRifles).Children.Count == 0)
                ((Pivot)TacticalRifles.Parent).Items.Remove(TacticalRifles);
            if (GetStackFromPivot(LightMachineGuns).Children.Count == 0)
                ((Pivot)LightMachineGuns.Parent).Items.Remove(LightMachineGuns);
            if (GetStackFromPivot(SniperRifles).Children.Count == 0)
                ((Pivot)SniperRifles.Parent).Items.Remove(SniperRifles);
            if (GetStackFromPivot(Secondaries).Children.Count == 0)
                ((Pivot)Secondaries.Parent).Items.Remove(Secondaries);

        }
        private class CustomSelectControl : SelectControl
        {
            public void DeleteControl()
            {
                VariableSizedWrapGrid parent = (VariableSizedWrapGrid)this.Parent;
                parent.Children.Remove(this);
                if (parent.Children.Count == 0)
                {
                    StackPanel stack = ((StackPanel)parent.Parent);
                    stack.Children.Remove(parent);
                    stack.Children.Remove(Label);
                }

            }
            private TextBlock Label;
            public CustomSelectControl(IconClass gun, TextBlock label) : base(gun)
            {
                Label = label;
            }
        }
    }
}
