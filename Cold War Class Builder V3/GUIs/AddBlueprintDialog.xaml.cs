using System;
using System.Collections.Generic;
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
    public sealed partial class AddBlueprintDialog : ContentDialog
    {
        public AddBlueprintDialog()
        {
            this.InitializeComponent();
        }
        public AddBlueprintDialog(AttachmentClass Atts,IconClass Gun,String Name)
        {
            this.InitializeComponent();
            atts = Atts;
            gun = Gun;
            name = Name;
            if (gun!=null)
                GunNameTextBlock.Text = gun.Name;
            
            AttachmentText.Text = "Attachments: " + atts.GetAttachmentCount();
        }
        private AttachmentClass atts = new AttachmentClass();
        private IconClass gun = null;
        private String name = "";
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            String v = NameTextBox.Text.ToUpper();
            
            if (gun == null)
            {
                ErrorTextBlock.Text = "Must select a gun";
                args.Cancel = true;
                return;
            }
            CustomBlueprint cb1 = new CustomBlueprint(v, atts, gun);
            if (v.Length < 3 || v.Length > 27)
            {
                ErrorTextBlock.Text = "Name must be between 3 and 23 characters";
                args.Cancel = true;
            }
            else if (Regex.IsMatch(v, "[^A-Z 0-9-]+"))
            {
                ErrorTextBlock.Text = "Name can only contain A-Z 0-9 space and -";
                args.Cancel = true;
            }
            else if (CustomBlueprint.CustomBlueprints.Any(z =>z.gunIcon==cb1.gunIcon && z.Name.Equals(v)))
            {
                ErrorTextBlock.Text = "Blueprint already exists by that name for the "+cb1.gunIcon.Name;
                args.Cancel = true;
            }
            else if (CustomBlueprint.CustomBlueprints.Any(z => z.isEqual(cb1)))
            {
                ErrorTextBlock.Text = "A Blueprint with that gun and attachments already exists";
                args.Cancel = true;
            }
            else
            {
                Hide();
                new ContentDialog()
                {
                    Title = "Blueprint Added",
                    SecondaryButtonText = "Close",
                    Content = "Blueprint \"" + cb1.Name + "\" has been added"
                }.ShowAsync();
                CustomBlueprint.CustomBlueprints.Add(cb1);
                CustomBlueprint.HasUpdated = true;
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GunSelectInterface gsd = GunSelectStatic.GetGunSelectGUI(true, true);
            gsd.UpdateEvent += (a, b) => {
                gun = gsd.GetReturnValue();
                ((ContentDialog)gsd).Hide();
                new AddBlueprintDialog(atts, gun, NameTextBox.Text).ShowAsync();
            };
            this.Hide();
            ((ContentDialog)gsd).ShowAsync();
        }

        
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (gun != null)
            {
                AttachmentDialogV2 ad = new AttachmentDialogV2(atts, 20, gun);
                ad.AttachmentSetEvent += (a, b) =>
                {
                    atts = ad.returnAttachments;
                    ad.Hide();
                    new AddBlueprintDialog(atts, gun, NameTextBox.Text).ShowAsync();
                };
                Hide();
                ad.ShowAsync();
            }
            else
            {
                new MessageDialog("Must Select A Gun First").ShowAsync();
            }
        }

        private void ContentDialog_Loaded(object sender, RoutedEventArgs e)
        {
            NameTextBox.Text = name;
        }
    }
}
