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
    public sealed partial class ViewGunDialog : ContentDialog
    {
        public ViewGunDialog()
        {
            this.InitializeComponent();
        }
        private string gunName,title;
        
        private AttachmentClass attachment;
        public ViewGunDialog(String gunname, AttachmentClass attachments)
        {
            title = gunname;
            gunName = gunname;
            attachment = attachments;
            this.InitializeComponent();
            
        }
        public ViewGunDialog(CustomBlueprint cb)
        {
            title = cb.Name;
            gunName = cb.gunIcon.Name;
            attachment = cb.Attachments;
            this.InitializeComponent();
        }
        public event EventHandler CloseEvent;
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            CloseEvent?.Invoke(sender, new EventArgs());
        }

        private void ContentDialog_Loaded(object sender, RoutedEventArgs e)
        {
            this.Title = title;
            Optic.Text = attachment.Optic;
            Muzzle.Text = attachment.Muzzle;
            Barrel.Text = attachment.Barrel;
            Body.Text = attachment.Body;
            Underbarrel.Text = attachment.Underbarrel;
            Magazine.Text = attachment.Magazine;
            Handle.Text = attachment.GunHandle;
            Stock.Text = attachment.Stock;
            GunName.Text = gunName;
        }
    }
}
