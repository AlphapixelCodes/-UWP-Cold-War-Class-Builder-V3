using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class CustomBlueprintsDialog : ContentDialog
    {
        public CustomBlueprintsDialog()
        {
            this.InitializeComponent();
            Builds = CustomBlueprint.CustomBlueprints;
        }
        private List<CustomBlueprint> Builds;
        public CustomBlueprintsDialog(List<CustomBlueprint> builds)
        {
            Builds = builds;
            InitializeComponent();
        }
        public GunControl GunClass;
        public event EventHandler BlueprintSelected_Event;
        private void addGunType(String name, List<CustomBlueprint> icons)
        {

            TextBlock txt = new TextBlock();
            txt.Width = Stack.ActualWidth;
            txt.TextAlignment = TextAlignment.Center;
            txt.FontFamily = new FontFamily("Stencil");
            txt.Text = name;
            Stack.Children.Add(txt);
            VariableSizedWrapGrid vswg = new VariableSizedWrapGrid();

            vswg.Width = 470;
            vswg.Orientation = Orientation.Horizontal;

            Stack.Children.Add(vswg);
            icons.ForEach(e => {
                CustomSelectControl sc = new CustomSelectControl(e.gunIcon,txt);
                sc.AttachedObject = e;
                sc.Margin = new Thickness(3, 3, 3, 3);
                sc.Tapped += SelectControl_Tapped;
                sc.setText(e.Name);
                vswg.Children.Add(sc);
               
                MenuFlyout flyout = new MenuFlyout();
                foreach (var a in new string[] { "Delete", "Rename", "Edit Attachments", "View" })
                {
                    MenuFlyoutItem mfi = new FlyoutMenuItemExtended(a, sc);
                    flyout.Items.Add(mfi);
                    mfi.Click += MenuFlyout_Click;
                }
                sc.RightTapped += (a, b) => flyout.ShowAt(sc);
            });
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
                    nd.Closed+= SecondaryGuiClosing_Event;
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
                    AttachmentDialogV2 ad = new AttachmentDialogV2(blue.Attachments,20,blue.gunIcon);
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
                CustomBlueprintsDialog cbd = new CustomBlueprintsDialog(Builds);
                cbd.GunClass = GunClass ?? null;
                cbd.BlueprintSelected_Event = this.BlueprintSelected_Event;
                cbd.ShowAsync();
            }catch { }
        }

        
        private void SelectControl_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (BlueprintSelected_Event == null)
            {
                SelectControl sc = (SelectControl) sender;
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

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_Loaded(object sender, RoutedEventArgs e)
        {
            Builds.Sort((a, b) => String.Compare(a.gunIcon.Name, b.gunIcon.Name));
            String curgun = "";
            List<CustomBlueprint> bs = new List<CustomBlueprint>();
            foreach (var item in Builds)
            {
                if (curgun != item.gunIcon.Name)
                {
                    if (curgun != "")
                    {
                        addGunType(curgun, bs);
                        bs = new List<CustomBlueprint>();
                    }
                    curgun = item.gunIcon.Name;
                    
                }
                bs.Add(item);

            }
            if (curgun != "")
                addGunType(curgun, bs);
            else {
                TextBlock t = new TextBlock();
                t.Text = "None";
                Stack.Children.Add(t);
            }
        }

        //for deleting custombuilds
        private class CustomSelectControl: SelectControl
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
            public CustomSelectControl(IconClass gun,TextBlock label) :base(gun)
            {
                Label = label;
            }
        }
    }
}
