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
    public sealed partial class AddCustomAttachmentDialog : ContentDialog
    {
        public AddCustomAttachmentDialog()
        {
            this.InitializeComponent();
        }
        public List<object> AttachedObjects = new List<object>();
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            
            if(!ValidateInput())
                args.Cancel = true;
        }
        private bool ValidateInput()
        {
            int type = TypeCombo.SelectedIndex;
            //   Debug.WriteLine(type);
            string v = NameTextBox.Text.ToUpper();
            if (v.Length < 3 || v.Length > 27)
            {
                ErrorText.Text = "Name must be between 3 and 27 characters";
                //  args.Cancel = true;
                return false;
            }
            else if (Regex.IsMatch(v, "[^A-Z 0-9&/-]+"))
            {
                ErrorText.Text = "Name can only contain A-Z 0-9 space, /, & and -";
                //args.Cancel = true;
                return false;
            }
            else if (AttachmentClass.CustomAttachments.Any(a => a.Name == v && type == a.Type) || Data.AllAttachments[type].Contains(v))
            {
                ErrorText.Text = "This attachment already exists";
                //     args.Cancel = true;
                return false;
            }
            else
            {
                AttachmentClass.CustomAttachments.Add(new AttachmentClass.CustomAttachment(type, v));
                AttachmentClass.HasUpdated = true;
                Callback?.Invoke("", new EventArgs());
                //this.Hide();
                return true;
                
            }
        }
        public event EventHandler Callback;
        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Callback?.Invoke("", new EventArgs());
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            TypeCombo.Items.Add("Optic");
            TypeCombo.Items.Add("Muzzle");
            TypeCombo.Items.Add("Barrel");
            TypeCombo.Items.Add("Body");
            TypeCombo.Items.Add("Underbarrel");
            TypeCombo.Items.Add("Magazine");
            TypeCombo.Items.Add("GunHandle");
            TypeCombo.Items.Add("Stock");
            TypeCombo.SelectedItem = "Optic";

        }

        private void NameTextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                ValidateInput();
                //Debug.WriteLine("Enter");

            }
        }
    }
}
